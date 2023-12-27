using DAL.Enums;
using DAL.Factory;
using DAL.Interface;
using ImportExcel.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ImportExcel.Controllers
{
    public class OrderController : Controller
    {
        private readonly IFileImportDBRepository _fileDBRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExportFactory _exportFactory;
        private readonly IFileImportJsonRepository _fileJsonRepository;
        private readonly IUtility _utility;
        private readonly ILogger<OrderController> _logger;


        public static List<OrderDTO> orders;
        public OrderController(IFileImportDBRepository file, IWebHostEnvironment webHostEnvironment, IExportFactory exportFactory, ILogger<OrderController> logger, IFileImportJsonRepository fileImportJson, IUtility utility)
        {
            _fileDBRepository = file;
            _webHostEnvironment = webHostEnvironment;
            _exportFactory = exportFactory;
            _logger = logger;
            _fileJsonRepository = fileImportJson;
            _utility = utility;
        }

        [HttpGet]
        public IActionResult ImportData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadExcel(IFormFile inputFile)
        {
            _logger.LogInformation("Uploading file starting");
            if (inputFile == null || inputFile.Length == 0)
            {
                return BadRequest("File is empty");
            }
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Json/orders.json");

            var orders = _utility.GetDataFromExcel(inputFile);
            _fileDBRepository.ImportToDatabase(orders);
            var count = _fileJsonRepository.ImportToJson(filePath, orders);
            TempData["TotalRecords"] = count + " Record Inserted";
            _logger.LogInformation(count + " Record Inserted");
            _logger.LogInformation("Uploading file end");
            return RedirectToAction(nameof(ImportData));
        }

        [HttpGet]
        public IActionResult ExportData()
        {
            return View();
        }

        [HttpPost]
        public void ExportData(ExporterEnum exporterType)
        {
            _logger.LogInformation("Export data starting");
            IFileExportRepository dataExporter = _exportFactory.GetDataExporter(exporterType);
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Json/orders.json");
            orders = dataExporter.ExportData(filePath);
            _logger.LogInformation("Export data ending");
        }

        [HttpGet]
        public IActionResult OrderList()
        {
            _logger.LogInformation("Reading data starting");
            return View(orders);
        }

        [HttpGet]
        public void SearchData(string data)
        {
            orders = _fileDBRepository.SearchFromDatabase(data);
        }

        [HttpGet]
        public  IActionResult FilterData(DateTime startDate, DateTime endDate)
        {
            orders =  _fileDBRepository.FilterFromDatabase(startDate, endDate);
            return RedirectToAction(nameof(OrderList));
        }

    }
}