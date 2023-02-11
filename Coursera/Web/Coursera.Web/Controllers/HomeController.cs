namespace Coursera.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Coursera.Services.Data;
    using Coursera.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IStudentService studentService;

        public HomeController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.studentService.ShowAllStudents();

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
