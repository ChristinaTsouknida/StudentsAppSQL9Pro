using System.ComponentModel.DataAnnotations;

namespace StudentsAppSqlDB9Pro.DTO
{
    public abstract record BaseDTO(

        [property: Required(ErrorMessage = "The {0} is required.")]
        int Id
    )
    {
        public BaseDTO() : this(0) { }
    }
}
