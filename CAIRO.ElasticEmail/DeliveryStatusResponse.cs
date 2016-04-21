namespace CAIRO.ElasticEmail
{
    public class DeliveryStatusResponseXml
    {
        public string ErrorMessage { get; set; }
        public ResultType ResultType { get; set; }
        public DeliveryStatusXml DeliveryStatus { get; set; }
    }

    public class DeliveryStatusResponseJson
    {
        public string ErrorMessage { get; set; }
        public ResultType ResultType { get; set; }
        public DeliveryStatusJson DeliveryStatus { get; set; }
    }
}