using System.ComponentModel.DataAnnotations;

namespace StudentsAppSqlDB9Pro.DTO
{
    public record StudentInsertDTO(

        [property: Required(ErrorMessage = "{0} is required.")]
        [property: MinLength(1, ErrorMessage = "{0} must no be empty")]
        string? Firstname,

        [property: Required(ErrorMessage = "{0} is required.")]
        [property: MinLength(1, ErrorMessage = "{0} must no be empty")]
        string? Lastname
        
    ) 
    {

        public StudentInsertDTO() : this(string.Empty, string.Empty) { }
    }
}
