using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsAppSqlDB9Pro.DTO;
using StudentsAppSqlDB9Pro.Models;
using StudentsAppSqlDB9Pro.Services;

namespace StudentsAppSqlDB9Pro.Pages.Students
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public StudentInsertDTO StudentInsertDTO { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = [];
        
        private readonly IStudentService studentService;

        public CreateModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public void OnGet()
        {
            // return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try 
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = studentService.InsertStudent(StudentInsertDTO);
                
                TempData["StudentName"] = $"{studentReadOnlyDTO?.Firstname}, {studentReadOnlyDTO?.Lastname}" + " was successfully created.";

                // PRG pattern Post-Request-Get
                return RedirectToPage("/Students/Success");


                //Response.Redirect("/Students/getall");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ErrorArray.Add(new Error { Message = ex.Message });
                return Page();
            }
        }   
    }
}
