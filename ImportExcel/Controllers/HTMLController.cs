using DAL.Enums;
using DAL.Factory;
using DAL.Interface;
using ImportExcel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml.Style;
using System.Dynamic;

namespace ImportExcel.Controllers
{
    public class HTMLController : Controller
    {
        private readonly IExportFactory _exportFactory;
        public HTMLController(IExportFactory exportFactory)
        {
            _exportFactory = exportFactory;
        }
        public static List<SelectListItem> items;
        public IActionResult Index()
        {
            items = new List<SelectListItem>();
            SelectListItem selectListItem = new SelectListItem();
            selectListItem.Text = "Id";
            selectListItem.Value = "Id";
            items.Add(selectListItem);
            selectListItem = new SelectListItem();
            selectListItem.Text = "Image";
            selectListItem.Value = "Image";
            items.Add(selectListItem);
            selectListItem = new SelectListItem();
            selectListItem.Text = "Name";
            selectListItem.Value = "Name";
            items.Add(selectListItem);
            selectListItem = new SelectListItem();
            selectListItem.Text = "OrderDate";
            selectListItem.Value = "OrderDate";
            items.Add(selectListItem);
            selectListItem = new SelectListItem();
            selectListItem.Text = "Price";
            selectListItem.Value = "Price";
            items.Add(selectListItem);
            selectListItem = new SelectListItem();
            selectListItem.Text = "Discount";
            selectListItem.Value = "Discount";
            items.Add(selectListItem);
            return View();
        }

        [HttpGet]
        public IActionResult DesiredTable(HTMLModel model)
        {
            IFileExportRepository fileExport = _exportFactory.GetDataExporter(ExporterEnum.Database);
            model.Orders = fileExport.ExportData();
            List<IDictionary<string, object>> filteredOrders = model.Orders.Select(order =>
            {
                var filteredOrder = new ExpandoObject() as IDictionary<string, object>;

                foreach (var column in model.Columns)
                {
                    var property = typeof(OrderDTO).GetProperty(column);
                    if (property != null)
                    {
                        filteredOrder.Add(column, property.GetValue(order));
                    }
                }

                return filteredOrder;
            }).ToList();
            return View(filteredOrders);
        }
    }
}