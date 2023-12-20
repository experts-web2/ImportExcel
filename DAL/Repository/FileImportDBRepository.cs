using ImportExcel.DAL;
using ImportExcel.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImportExcel.Services
{
    public class FileImportDBRepository : IFileImportDBRepository
    {
        private readonly AppDbContext _context;

        public FileImportDBRepository(AppDbContext db)
        {
            _context = db;
        }

        public void ImportToDatabase(List<OrderDTO> orderDTOs)
        {
            try
            {
                List<Order> orders = new List<Order>();
                foreach (OrderDTO orderDTO in orderDTOs)
                {
                    orders.Add(new Order { Discount = orderDTO.Discount, Name = orderDTO.Name, OrderDate = orderDTO.OrderDate, Price = orderDTO.Price, Image = orderDTO.Image, Id = orderDTO.Id });
                }
                _context.Orders.AddRange(orders);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<OrderDTO> SearchFromDatabase(string searchData)
        {
            try
            {
                List<Order> orders = _context.Orders.Where(x => x.Name.Contains(searchData) || x.Id.Contains(searchData)).ToList();
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
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OrderDTO>> FilterFromDatabase(DateTime startDate, DateTime endDate)
        {
            try
            { 
                List<Order> orders =await _context.Orders.Where(x => Convert.ToDateTime(x.OrderDate) >= startDate && Convert.ToDateTime(x.OrderDate) <= endDate).ToListAsync();
            
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}