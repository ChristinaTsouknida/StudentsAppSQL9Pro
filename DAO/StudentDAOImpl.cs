using Microsoft.Data.SqlClient;
using StudentsAppSqlDB9Pro.Core;
using StudentsAppSqlDB9Pro.Models;

namespace StudentsAppSqlDB9Pro.DAO
{
    public class StudentDAOImpl : IStudentDAO
    {
        private readonly DBHelper _db;

        public StudentDAOImpl(DBHelper db)
        {
            _db = db;
        }

        public Student? Insert(Student student)
        {
            int insertedId = 0;

            Student? studentToReturn = null;

            string sql = "INSERT INTO Students (Firstname, Lastname) VALUES (@firstname, @lastname); " +
                "SELECT  SCOPE_IDENTITY();";

            using SqlConnection connection = _db.GetConnection();
            connection.Open();

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@firstname", student.Firstname);
            command.Parameters.AddWithValue("@lastname", student.Lastname);

            var insertedObject = command.ExecuteScalar();

            if (insertedObject != null)
            {
                if (!int.TryParse(insertedObject.ToString(), out insertedId))
                {
                    throw new Exception("Error in inserted id");
                }
            }

            string sql2 = "SELECT * FROM Students WHERE Id = @studentId";
            using SqlCommand command2 = new(sql2, connection);
            command2.Parameters.AddWithValue("@studentId", insertedId);

            using SqlDataReader reader = command2.ExecuteReader();

            if (reader.Read())
            {
                studentToReturn = new Student()
                {
                    Id = (int)reader["Id"],
                    Firstname = reader["Firstname"] as string ?? "",
                    Lastname = reader["Lastname"] as string ?? ""
                };
            }

            return studentToReturn;
        }

        public void Update(Student student)
        {
            string sql = "UPDATE Students SET Firstname = @firstname, Lastname = @lastname WHERE Id = @id";

            using SqlConnection connection = _db.GetConnection();
            connection.Open();

            using SqlCommand sqlCommand = new(sql, connection);
            sqlCommand.Parameters.AddWithValue("@firstname", student.Firstname);
            sqlCommand.Parameters.AddWithValue("@lastname", student.Lastname);
            sqlCommand.Parameters.AddWithValue("@id", student.Id);

            sqlCommand.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            string sql = "DELETE FROM Students WHERE Id = @id";

            using SqlConnection connection = _db.GetConnection();
            connection.Open();

            using SqlCommand sqlCommand = new(sql, connection);

            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.ExecuteNonQuery();
        }

        public Student? GetById(int id)
        {
            Student studentToReturn = null;
            string sql = "SELECT * FROM Students WHERE Id = @id";

            using SqlConnection connection = _db.GetConnection();
            connection.Open();

            using SqlCommand sqlCommand = new(sql, connection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                studentToReturn = new Student()
                {
                    Id = (int)reader["Id"],
                    Firstname = reader["Firstname"] as string ?? "",
                    Lastname = reader["Lastname"] as string ?? ""
                };
            }

            return studentToReturn;
        }


        public List<Student> GetAll()
        {
            List<Student> students = [];
            string sql = "SELECT * FROM Students";


            using SqlConnection connection = _db.GetConnection();
            connection.Open();

            using SqlCommand sqlCommand = new(sql, connection);
            
            using SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student()
                {
                    Id = (int)reader["Id"],
                    Firstname = reader["Firstname"] as string ?? "",
                    Lastname = reader["Lastname"] as string ?? ""
                };

                students.Add(student);
            }

            return students;
        }

       
       

        
    }
}
