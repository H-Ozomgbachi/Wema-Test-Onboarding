namespace Onboarding.Shared.Helpers
{
    public class HashHelper
    {
        private const int _saltSize = 64;

        public static string GenerateSalt()
        {
            byte[] salt = new byte[_saltSize];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
        }
        public static void CreateHashAndSalt(string plain, out string pSalt, out string pHash)
        {
            pSalt = GenerateSalt();

            byte[] saltBytes = Convert.FromBase64String(pSalt);
            byte[] pBytes = Encoding.UTF8.GetBytes(plain);

            using var hmacSha512 = new HMACSHA512(saltBytes);

            byte[] hash = hmacSha512.ComputeHash(pBytes);
            pHash = Convert.ToBase64String(hash);
        }
        public static bool VerifyP(string p, string pSalt, string pHash)
        {
            byte[] saltBytes = Convert.FromBase64String(pSalt);
            byte[] pBytes = Encoding.UTF8.GetBytes(p);

            using var hmacSha512 = new HMACSHA512(saltBytes);
            byte[] hash = hmacSha512.ComputeHash(pBytes);

            return pHash == Convert.ToBase64String(hash);
        }
    }
}
