using Microsoft.AspNetCore.Mvc;

namespace ImportExcel.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult GetData()
		{
			List<Order> orders = new List<Order>()
			{
			 new Order(){Name = "Clarifying Graphene Sheet Mask",
				Image="https://vouponlive.blob.core.windows.net/products/216/images/small_01_6cd6079e-5bf5-4965-b488-f31e5e3624c3.jpeg",
				OrderDate = Convert.ToDateTime("11/7/2023"),
				Price =  20.00M,
				Discount =  17.00M
			 }
			};
            return View(orders);
        }
	}
}