namespace BlazorMicroservices.Web.Utilities
{
    public class SD
    {
        public static string CouponApiBase {  get; set; }
        public static string AuthApiBase {  get; set; }

        public enum ApiType
        {
            Get,
            POST,
            PUT,
            DELETE
        }

        public const string JwtToken = "JWTToken";
        public const string UserDetails = "UserDetails";

        public const string Role_Admins = "Admins";
        public const string Role_Users = "Users";
    }
}
