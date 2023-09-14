namespace ProjectModel.ReceiptComponents
{
    public class Receipt
    {
        public long Id { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalQuantity { get; set; }
        public float Total { get; set; }
        public ReceiptItem[] Items { get; set; }
    }
}