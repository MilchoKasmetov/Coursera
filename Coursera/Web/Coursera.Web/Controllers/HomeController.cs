namespace Coursera.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Coursera.Services.Data;
    using Coursera.Web.ViewModels;
    using Coursera.Web.ViewModels.Students;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IStudentService studentService;

        public HomeController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public IActionResult Index()
        {
            var model = new SearchStudentInputModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchStudentInputModel inputModel)
        {
            var model = await this.studentService.ShowAllStudents();

            return this.View(model);
        }

        //public  IActionResult Index()
        //{

        //    return this.View();
        //}

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
