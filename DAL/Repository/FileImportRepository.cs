using ImportExcel.DAL;
using ImportExcel.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImportExcel.Services
{
    public class FileImportRepository : IFileImportRepository
    {
        private readonly AppDbContext _context;

        public FileImportRepository(AppDbContext db)
        {
            _context = db;
        }

        public int UploadFile(IFormFile file, string filePath)
        {
            try
            {
                List<Order> orders = new List<Order>();
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var order = new Order
                            {
                                Id = worksheet.Cells[row, 1].Value.ToString(),
                                Image = worksheet.Cells[row, 2].Value.ToString(),
                                Name = worksheet.Cells[row, 3].Value.ToString(),
                                OrderDate = GetDateString(worksheet, row),
                                Price = worksheet.Cells[row, 5].Value.ToString(),
                                Discount = worksheet.Cells[row, 6].Value.ToString(),
                            };
                            orders.Add(order);
                        }
                        _context.Orders.AddRange(orders);
                        _context.SaveChanges();
                    }
                }
                SaveDataToJson(filePath, orders);
                return orders.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetDateString(ExcelWorksheet worksheet, int row)
        {
            double d = double.Parse(worksheet.Cells[row, 4].Value.ToString());
            DateTime conv = DateTime.FromOADate(d);                                                               
            return conv.ToString("yyyy/MM/dd");
        }

        private void SaveDataToJson(string filePath, List<Order> orders)
        {
            string json = JsonConvert.SerializeObject(orders);
            File.WriteAllText(filePath, json);
        }
    }
}