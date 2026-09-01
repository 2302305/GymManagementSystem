using System.Text.Json;

namespace GymManagementDAL.Data.DataSeeding
{
    public static class GymDbSeeding
    {
        public static bool SeedData(GymDbContext gymDbContext)
        {
            try
            {
                var HasPlans = gymDbContext.Plans.Any();
                var HasCategories = gymDbContext.Categories.Any();
                if (HasPlans && HasCategories) return false;
                if (!HasPlans)
                {
                    var Plans = LoadDataFromJsonFile<Plan>("plans.json");
                    if (Plans.Any())
                        gymDbContext.Plans.AddRange(Plans);
                }
                if (!HasCategories)
                {
                    var Categories = LoadDataFromJsonFile<Category>("categories.json");
                    if (Categories.Any())
                        gymDbContext.Categories.AddRange(Categories);
                }
                return gymDbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Seeding Failed {ex.Message}");
                return false;
            }

        }
        private static List<T> LoadDataFromJsonFile<T>(string JsonFileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\JsonFiles", JsonFileName);
            if (!File.Exists(FilePath)) throw new FileNotFoundException(FilePath);
            string Data = File.ReadAllText(FilePath);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(Data, Options) ?? new List<T>();
        }
    }
}
