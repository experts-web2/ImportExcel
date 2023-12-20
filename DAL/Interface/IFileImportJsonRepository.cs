
using ImportExcel;

namespace DAL.Interface
{
    public interface IFileImportJsonRepository
    {
        int ImportToJson(string filePath, List<OrderDTO> orders);
    }
}