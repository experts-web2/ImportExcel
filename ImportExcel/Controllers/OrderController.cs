using DAL.Enums;
using DAL.Factory;
using DAL.Interface;
using ImportExcel.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Style;

namespace ImportExcel.Controllers
{
    public class OrderController : Controller
    {
        private readonly IFileImportRepository _fileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IExportFactory _exportFactory;
        private readonly ILogger<OrderController> _logger;
        public static List<OrderDTO> orders; 
        public OrderController(IFileImportRepository file, IWebHostEnvironment webHostEnvironment, IExportFactory exportFactory, ILogger<OrderController> logger)
        {
            _fileRepository = file;
            _webHostEnvironment = webHostEnvironment;
            _exportFactory = exportFactory;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult ImportData(List<OrderDTO> orders = null)
        {
            if (orders is null)
                orders = new List<OrderDTO>();
            return View(orders);
        }

        [HttpPost]
        public IActionResult UploadExcel(IFormFile inputFile)
        {
            try
            {
                _logger.LogInformation("Uploading file starting");
                if (inputFile == null || inputFile.Length == 0)
                {
                    return BadRequest("File is empty");
                }
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Json/orders.json");

                int count = _fileRepository.UploadFile(inputFile, filePath);
                TempData["TotalRecords"] = count + " Record Inserted";
                _logger.LogInformation(count + " Record Inserted");
                _logger.LogInformation("Uploading file end");
                return RedirectToAction(nameof(ImportData));
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpGet]
        public IActionResult ExportData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ExportData(ExporterEnum exporterType)
        {
            _logger.LogInformation("Export data starting");
            IFileExportRepository dataExporter = _exportFactory.GetDataExporter(exporterType);
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Json/orders.json");
            orders = dataExporter.ExportData(filePath);
            _logger.LogInformation("Export data ending");
            return Json("");
        }

        [HttpGet]
        public IActionResult OrderList()
        {
            _logger.LogInformation("Reading data starting");
            return View(orders);
        }

    }
}