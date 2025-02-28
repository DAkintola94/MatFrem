namespace MatFrem.Models.DomainModel
{
    public class OrderProducts
    {
        public int OrderID { get; set; }
        public OrderModel OrderM { get; set; }

        public int ProductID { get; set; }
        public ProductModel ProductM { get; set; }

    }
}
