using AutoMapper;
using StudentsAppSqlDB9Pro.DTO;
using StudentsAppSqlDB9Pro.Models;

namespace StudentsAppSqlDB9Pro.Configuration
{
    public class MapperConfig : Profile
    {

        public MapperConfig() 
        {
            CreateMap<StudentInsertDTO, Student>().ReverseMap();
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();
            CreateMap<StudentReadOnlyDTO, Student>().ReverseMap();
        }
    }
}
