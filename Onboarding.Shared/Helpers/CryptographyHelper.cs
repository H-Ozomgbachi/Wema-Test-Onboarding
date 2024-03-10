namespace Onboarding.Shared.Helpers
{
    public class CryptographyHelper
    {
        private readonly AppSettings _appSettings;
        public CryptographyHelper(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public string EncryptString(string plainText)
        {
            using Aes aes = Aes.Create();

            aes.Key = Convert.FromBase64String(_appSettings.EKey);
            aes.IV = Convert.FromBase64String(_appSettings.EIv);

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherText = memoryStream.ToArray();
            return Convert.ToBase64String(cipherText);
        }
        public string DecryptString(string cipherText)
        {
            using Aes aes = Aes.Create();

            aes.Key = Convert.FromBase64String(_appSettings.EKey);
            aes.IV = Convert.FromBase64String(_appSettings.EIv);

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Write);

            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] plainBytes = memoryStream.ToArray();
            return Encoding.ASCII.GetString(plainBytes);
        }
    }
}
