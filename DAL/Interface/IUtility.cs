using ImportExcel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUtility
    {
        List<OrderDTO> GetDataFromExcel(IFormFile file);
    }
}