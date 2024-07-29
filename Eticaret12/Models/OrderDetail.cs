namespace Eticaret12.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrıce { get; set; }
    }
}
