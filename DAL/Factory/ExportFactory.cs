using DAL.Enums;
using DAL.Interface;
using DAL.Repository;
using ImportExcel.DAL;

namespace DAL.Factory
{
    public class ExportFactory: IExportFactory
    {
        private readonly AppDbContext _context;

        public ExportFactory(AppDbContext db)
        {
            _context = db;
        }
        public ExportFactory() { }
        public IFileExportRepository GetDataExporter(ExporterEnum exporterType)
        {
            return exporterType switch
            {
                ExporterEnum.Json => new ExportDataFromJson(),
                ExporterEnum.Database => new ExportDataFromDatabase(_context),
                _ => throw new NotSupportedException("Exporter type not supported"),
            };
        }
    }
}