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
        public static List<Order> orders; 
        public OrderController(IFileImportRepository file, IWebHostEnvironment webHostEnvironment, IExportFactory exportFactory)
        {
            _fileRepository = file;
            _webHostEnvironment = webHostEnvironment;
            _exportFactory = exportFactory;
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
                if (inputFile == null || inputFile.Length == 0)
                {
                    return BadRequest("File is empty");
                }
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Json/orders.json");

                int count = _fileRepository.UploadFile(inputFile, filePath);
                TempData["TotalRecords"] = count + " Record Inserted";

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
            IFileExportRepository dataExporter = _exportFactory.GetDataExporter(exporterType);
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Json/orders.json");
            orders = dataExporter.ExportData(filePath);
            return Json("");
        }

        [HttpGet]
        public IActionResult OrderList()
        {
            return View(orders);
        }

    }
}