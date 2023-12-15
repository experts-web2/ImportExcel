using Microsoft.AspNetCore.Http;

namespace ImportExcel.Interface
{
	public interface IFileImportRepository
	{
       int UploadFile(IFormFile formFile,string filePath);
    }
}
