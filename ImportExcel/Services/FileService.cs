using ImportExcel.DAL;
using ImportExcel.Interface;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;

namespace ImportExcel.Services
{
    public class FileService : IFileInterface
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FileService> _logger;

        public FileService(AppDbContext db, ILogger<FileService> logger)
        {
            _context = db;
            _logger = logger;

        }

        public void UploadFile(IFormFile file)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        int recordsAdded = 0;
                        int recordsUpdated = 0;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            var order = new Order
                            {
                                Id = worksheet.Cells[row, 1].Value?.ToString(),
                                Image = worksheet.Cells[row, 2].Value?.ToString(),
                                Name = worksheet.Cells[row, 3].Value?.ToString(),
                                OrderDate = worksheet.Cells[row, 4].Value?.ToString(),
                                Price = worksheet.Cells[row, 5].Value?.ToString(),
                                Discount = worksheet.Cells[row, 6].Value?.ToString(),
                            };

                            var existingOrder = _context.Orders.Find(order.Id);

                            if (existingOrder == null)
                            {
                                _context.Orders.Add(order);
                                recordsAdded++;
                            }
                            else
                            {
                                // Update existing order properties
                                existingOrder.Image = order.Image;
                                existingOrder.Name = order.Name;
                                existingOrder.OrderDate = order.OrderDate;
                                existingOrder.Price = order.Price;
                                existingOrder.Discount = order.Discount;

                                recordsUpdated++;
                            }
                        }

                        _context.SaveChanges();

                        _logger.LogInformation($"Records Added: {recordsAdded}, Records Updated: {recordsUpdated}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the Excel file.");
                // Handle the exception as needed
            }
        }

        public List<Order> GetData()
        {
            List<Order> orders = _context.Orders.ToList();
            return orders;
        }
    }
}
