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
    public class FileImportJsonRepository : IFileImportJsonRepository
    {
        public int ImportToJson(string filePath, List<OrderDTO> orders)
        {
            string json = JsonConvert.SerializeObject(orders);
            File.WriteAllText(filePath, json);
            return orders.Count;
        }
    }
}