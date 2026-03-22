using AutoMapper;
using StudentsAppSqlDB9Pro.DAO;
using StudentsAppSqlDB9Pro.DTO;
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

        public StudentReadOnlyDTO? InsertStudent(StudentReadOnlyDTO studentInsertDTO)
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

        public void DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }

        public List<StudentReadOnlyDTO> GetAllStudents()
        {
            throw new NotImplementedException();
        }

        public StudentReadOnlyDTO GetStudent(int id)
        {
            throw new NotImplementedException();
        }

        

        public void UpdateStudent(StudentUpdateDTO studentUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
