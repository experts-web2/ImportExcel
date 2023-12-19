using DAL.Interface;
using ImportExcel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    internal class ExportDataFromJson : IFileExportRepository
    {
        public List<OrderDTO> ExportData(string filePath = null)
        {
            List<Order> orders = new List<Order>();
            string json = File.ReadAllText(filePath);
            orders = JsonConvert.DeserializeObject<List<Order>>(json);
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            if (orders != null && orders.Any())
            {
                foreach (var order in orders)
                {
                    orderDTOs.Add(new OrderDTO { Discount = order.Discount, Name = order.Name, OrderDate = order.OrderDate, Price = order.Price, Image = order.Image, Id = order.Id });
                }
            }
            return orderDTOs;
        }
    }
}
