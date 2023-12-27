using Microsoft.AspNetCore.Http;

namespace ImportExcel.Interface
{
	public interface IFileImportDBRepository
	{
        void ImportToDatabase(List<OrderDTO> orderDTOs);
       List<OrderDTO> SearchFromDatabase(string searchData);
       List<OrderDTO> FilterFromDatabase(DateTime startDate, DateTime endDate);
    }
}
