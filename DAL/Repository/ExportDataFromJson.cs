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
            List<OrderDTO> orderDTOs = new List<OrderDTO>();
            string json = File.ReadAllText(filePath);
            orderDTOs = JsonConvert.DeserializeObject<List<OrderDTO>>(json);
            return orderDTOs;
        }
    }
}
