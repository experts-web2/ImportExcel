using DAL.Interface;
using ImportExcel;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UtitlityRepository : IUtility
    {
        public List<OrderDTO> GetDataFromExcel(IFormFile file)
        {
            try
            {
                List<OrderDTO> orders = new List<OrderDTO>();
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            var order = new OrderDTO
                            {
                                Id = worksheet.Cells[row, 1]?.Value.ToString(),
                                Image = worksheet.Cells[row, 2]?.Value.ToString(),
                                Name = worksheet.Cells[row, 3]?.Value.ToString(),
                                OrderDate = GetDateString(worksheet, row),
                                Price = worksheet.Cells[row, 5]?.Value.ToString(),
                                Discount = worksheet.Cells[row, 6]?.Value.ToString(),
                            };
                            orders.Add(order);
                        }
                    }
                    return orders;
                }
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
    }
}