namespace healthcare_visuzlier25.Util
{
    public class Base64Tools
    {
        //TODO: checl for a way to pass the correct Ilogger here
        private static ILogger<Base64Tools> logger = LoggerFactory.Create((elem) => { elem.AddConsole(); }).CreateLogger<Base64Tools>();

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Base64Encode(plainTextBytes);
        }

        public static string Base64Encode(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        public static string Base64DecodeToString(string base64EncodedData)
        {
            var base64EncodedBytes = Base64DecodeToBytes(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static byte[] Base64DecodeToBytes(string base64EncodedData)
        {
            return Convert.FromBase64String(base64EncodedData);
        }
        public static bool IsBase64(string base64String)
        {
            // Credit: oybek https://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                logger.LogWarning("Caught exception when asserting whether string is base64 encoded or not");
                // Handle the exception
            }
            return false;
        }

    }
}
