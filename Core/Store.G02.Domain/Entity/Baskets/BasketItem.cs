namespace Store.G02.Domain.Entity.Baskets
{
    public class BasketItem
    {
        public int Id { get; set; } 

        public string ProductName { get; set; }

        public string PictureUrl { get; set; }

        public Decimal Price { get; set; }

        public int Quantity { get; set; }



    }
}