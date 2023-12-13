namespace ImportExcel
{
	public class Order
	{
        public string? Image { get; set; }
        public string? Name { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}