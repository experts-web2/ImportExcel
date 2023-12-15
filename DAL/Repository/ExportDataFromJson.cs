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
    public class ExportDataFromJson : IFileExportRepository
    {
        public List<Order> ExportData(string filePath = null)
        {
            List<Order> orders = new List<Order>();
            string json = File.ReadAllText(filePath);
            orders = JsonConvert.DeserializeObject<List<Order>>(json);
            return orders;
        }
    }
}
