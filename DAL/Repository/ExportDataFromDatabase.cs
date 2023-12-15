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
        public List<Order> ExportData(string filePath = null)
        {
            List<Order> orders = _context.Orders.ToList();
            return orders;
        }
    }
}
