using System.Text;

namespace healthcare_visuzlier25.Crypto
{
    public interface ICryptoProvider
    {
        public byte[] EncryptRaw(byte[] bytes);
        public byte[] DecryptRaw(byte[] bytes);


        public string Base64Encode(byte[] bytes)
        {
            return Util.Base64Tools.Base64Encode(EncryptRaw(bytes));
        }
        public string Base64Encode(string input)
        {
            return Util.Base64Tools.Base64Encode(EncryptRaw(Encoding.UTF8.GetBytes(input)));
        }

        public string Base64Decode(string input)
        {
            var rawBytes = Util.Base64Tools.Base64DecodeToBytes(input);
            var in1 = DecryptRaw(rawBytes);
            var finalString = Encoding.UTF8.GetString(in1);
            return finalString;
        }
    }
}
