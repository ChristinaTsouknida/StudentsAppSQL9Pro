using AutoMapper;
using StudentsAppSqlDB9Pro.DAO;
using StudentsAppSqlDB9Pro.DTO;
using StudentsAppSqlDB9Pro.Exceptions;
using StudentsAppSqlDB9Pro.Models;
using System.Transactions;

namespace StudentsAppSqlDB9Pro.Services
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly IStudentDAO _studentDAO;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentServiceImpl> _logger;

        public StudentServiceImpl(IStudentDAO studentDAO, IMapper mapper, ILogger<StudentServiceImpl> logger)
        {
            _studentDAO = studentDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public StudentReadOnlyDTO? InsertStudent(StudentInsertDTO studentInsertDTO)
        {
            StudentReadOnlyDTO? studentReadOnlyDTO;

            try
            {
                using TransactionScope scope = new TransactionScope();
                Student student = _mapper.Map<Student>(studentInsertDTO);
                Student? insertedStudent = _studentDAO.Insert(student);
                studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(student);
                _logger.LogInformation("Student {Firstname} {Lastname} inserted successfully.",
                    studentReadOnlyDTO.Firstname, studentReadOnlyDTO.Lastname);

                scope.Complete();
                return studentReadOnlyDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError("Student insertion failed for {Firstname} {Lastname}. {Errormessage}",
                    studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
                throw;
            }
        }

        public StudentReadOnlyDTO GetStudent(int id)
        {
            StudentReadOnlyDTO studentReadOnlyDTO;
            Student student;

            try
            {
                student = _studentDAO.GetById(id) ?? throw new StudentNotFoundException($"Student with id {id} not found.");
                studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(student);
                _logger.LogInformation("Student with id={Id} fetched successfully", id);
                return studentReadOnlyDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching student with id ={Id} {ErrorMessage}", id, ex.Message);
                throw;
            }
        }

        public void UpdateStudent(StudentUpdateDTO studentUpdateDTO)
        {
            try 
            {
                using TransactionScope scope = new TransactionScope();
                if (_studentDAO.GetById(studentUpdateDTO.Id) is null)
                {
                    throw new StudentNotFoundException($"Student with id {studentUpdateDTO.Id} not found.");
                }
                Student student = _mapper.Map<Student>(studentUpdateDTO);
                _studentDAO.Update(student);
                _logger.LogInformation("Student {Firstname} {Lastname} updated successfully.",
                   studentUpdateDTO.Firstname, studentUpdateDTO.Lastname);

                scope.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError("Student update failed for {Firstname} {Lastname}. {Errormessage}",
                    studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
                throw;
            }
        }

        public void DeleteStudent(int id)
        {
            try
            {
                using TransactionScope scope = new TransactionScope();

                if (_studentDAO.GetById(id) is null)
                {
                    throw new StudentNotFoundException($"Student with id {id} not found.");
                }
                _studentDAO.Delete(id);
                _logger.LogInformation("Student with id={id} deleted successfully.", id);

                scope.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError("Student with id={id} failed to delete. {ErrorMessage}",
                    id, ex.Message);
                throw;
            }
        }

        public List<StudentReadOnlyDTO> GetAllStudents()
        {
            List<StudentReadOnlyDTO> studentsReadOnlyDTOs = [];
            StudentReadOnlyDTO studentReadOnlyDTO;

            List<Student> students;

            try
            {
                students = _studentDAO.GetAll();
                foreach (Student student in students)
                {
                    studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(student);
                    studentsReadOnlyDTOs.Add(studentReadOnlyDTO);
                }
                _logger.LogInformation("All students fetched successfully");
                return studentsReadOnlyDTOs;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching all students. {ErrorMessage}", ex.Message);
                throw;
            }
        }

       

        

        
    }
}
