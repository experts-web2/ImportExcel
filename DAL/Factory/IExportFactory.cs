using DAL.Enums;
using DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Factory
{
    public interface IExportFactory
    {
        IFileExportRepository GetDataExporter(ExporterEnum exporterType);
    }
}