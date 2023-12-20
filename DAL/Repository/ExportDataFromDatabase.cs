using DAL.Interface;
using ImportExcel;
using ImportExcel.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    internal class ExportDataFromDatabase : IFileExportRepository
    {
        private readonly AppDbContext _context;
        public ExportDataFromDatabase(AppDbContext db)
        {
            _context = db;
        }
        public List<OrderDTO> ExportData(string filePath = null)
        {
            List<Order> orders = _context.Orders.ToList();
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
