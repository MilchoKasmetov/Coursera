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
        private readonly IExporter csvExporter;

        public HomeController(IStudentService studentService,CSVExporter csvExporter)
        {
            this.studentService = studentService;
            this.csvExporter = csvExporter;
        }

        public IActionResult Index()
        {
            var model = new SearchStudentInputModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchStudentInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var model = await this.studentService.ShowAllStudents(input);

            if (input.OutputFormat == OutputFormat.Csv)
            {
                this.csvExporter.Export(model, input.DirectoryPath);
            }
            else if (input.OutputFormat == OutputFormat.Html)
            {

            }
            else
            {

            }



             return this.RedirectToAction("Index", "Home");
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
