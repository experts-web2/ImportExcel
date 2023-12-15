using ImportExcel.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ImportExcel.Controllers
{
    public class OrderController : Controller
	{
		private readonly IFileInterface _fileService;
		public OrderController(IFileInterface fileInterface)
		{
			_fileService = fileInterface;
		}
		[HttpGet]
		public IActionResult GetData(List<Order>? orders=null)
		{
			if(orders is null)
				orders = new List<Order>();
			return View(orders);
		}
        [HttpGet]
        public IActionResult ImportExcel(List<Order>? orders = null)
        {
            if (orders is null)
                orders = new List<Order>();
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
                _fileService.UploadFile(inputFile);

				return View(nameof(GetData),new List<Order>());
				return View();
			}
			catch (Exception)
			{
				return GetData();
			}
		}

	}
}