namespace ImportExcel.Interface
{
	public interface IFileInterface
	{
       void UploadFile(IFormFile formFile);
        List<Order> GetData();

    }
}
