namespace MovieStore.API.Business.Common
{
    public static class PropertyUpdator
    {
        public static string Update(string oldVal, string newVal)
        {
            return newVal is null || newVal.Trim() == string.Empty ? oldVal : newVal.Trim();
        }
    }
}