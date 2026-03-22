using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsAppSqlDB9Pro.Models;
using StudentsAppSqlDB9Pro.Services;

namespace StudentsAppSQL9Dev.Pages.Students
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService studentService;

        public DeleteModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                studentService.DeleteStudent(id);
                TempData["StudentName"] = $"Student with id {id} was successfully deleted.";


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
