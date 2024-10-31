namespace Application
{
    public class PaymentInfomation
    {
        public required Dictionary<string, string> PaymentData { get; set; }

        public required string PaymentUrl { get; set; }
    }


    public class PaymentRequestData
    {
        public required string RecordNo { get; set; }
        public required string Amount { get; set; }

        public required string PayType { get; set; }
    }

    public class PaymentRequestDataWithUrl: PaymentRequestData
    {
        public required string PaymentUrl { get; set; }
    }
}
