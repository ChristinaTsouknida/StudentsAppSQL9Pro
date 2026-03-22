namespace StudentsAppSqlDB9Pro.Exceptions
{
    public class StudentNotFoundException : Exception
    {

        public StudentNotFoundException() : base("Student not found.")
        {

        }


        public StudentNotFoundException(string message) : base(message)
        {

        }

        public StudentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
