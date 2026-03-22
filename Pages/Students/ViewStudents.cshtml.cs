using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsAppSqlDB9Pro.DTO;
using StudentsAppSqlDB9Pro.Models;
using StudentsAppSqlDB9Pro.Services;

namespace StudentsAppSQL9Dev.Pages.Students
{
    public class ViewStudentsModel : PageModel
    {
        public Error ErrorObj { get; set; } = new();

        private readonly IStudentService studentService;
        public List<StudentReadOnlyDTO> StudentsReadOnlyDTO { get; set; } = [];

        public ViewStudentsModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public IActionResult OnGet()
        {
            try
            {
                StudentsReadOnlyDTO = studentService.GetAllStudents();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ErrorObj = new Error { Message = ex.Message };

            }
            return Page();
        }
    }
}
