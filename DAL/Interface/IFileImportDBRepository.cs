using Microsoft.AspNetCore.Http;

namespace ImportExcel.Interface
{
	public interface IFileImportDBRepository
	{
        void ImportToDatabase(List<OrderDTO> orderDTOs);
       List<OrderDTO> SearchFromDatabase(string searchData);
       Task<List<OrderDTO>> FilterFromDatabase(DateTime startDate, DateTime endDate);
    }
}
