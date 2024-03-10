namespace Onboarding.Shared.Helpers
{
    public record UtilityHelper
    {
        private static readonly Random _rnd = new();
        public static string Serializer(object obj)
        {
            JsonSerializerSettings options = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.SerializeObject(obj, options);
        }
        public static T DeSerializer<T>(string jsonString)
        {
            JsonSerializerSettings options = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(jsonString, options);
        }
        public static string GetRandomDigits(int count)
        {
            const string charSet = "0123456789";

            char[] result = new char[count];
            for (ushort i = 0; i < count; i++)
            {
                result[i] = charSet[_rnd.Next(charSet.Length)];
            }

            return new string(result);
        }
    }
}
