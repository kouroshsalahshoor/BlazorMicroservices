namespace BlazorMicroservices.Web.Utilities
{
    public class SD
    {
        public static string CouponApiBase {  get; set; }
        public enum ApiType
        {
            Get,
            POST,
            PUT,
            DELETE
        }
    }
}
