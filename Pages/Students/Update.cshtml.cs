using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsAppSqlDB9Pro.DTO;
using StudentsAppSqlDB9Pro.Models;
using StudentsAppSqlDB9Pro.Services;

namespace StudentsAppSQL9Dev.Pages.Students
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public StudentUpdateDTO? StudentUpdateDTO { get; set; } 
        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService _studentService;

        public UpdateModel(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = _studentService.GetStudent(id);
                StudentUpdateDTO = new StudentUpdateDTO
                {
                    Id = studentReadOnlyDTO.Id,
                    Firstname = studentReadOnlyDTO.Firstname,
                    Lastname = studentReadOnlyDTO.Lastname,
                };

            }
            catch (Exception ex)
            {
                ErrorArray.Add(new Error { Message = ex.Message });
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return Page();
            }
            try
            {
                
                _studentService.UpdateStudent(StudentUpdateDTO!);

                TempData["StudentName"] = $"Student with id {StudentUpdateDTO!.Id} was successfully updated.";


                // PRG pattern Post-Request-Get
                return RedirectToPage("/Students/Success");
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
