using Microsoft.AspNetCore.Identity;

namespace GymManagementDAL.Data.DataSeeding
{
    public static class IdentityDbContextSeeding
    {
        public static bool SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            try
            {
                var hasUsers = userManager.Users.Any();
                var hasRoles = roleManager.Roles.Any();
                if (hasUsers && hasRoles) return false;

                if (!hasRoles)
                {
                    var roles = new List<IdentityRole>()
                    {
                        new() { Name = "SuperAdmin" },
                        new() { Name = "Admin" }
                    };

                    foreach (var role in roles)
                    {
                        if (!roleManager.RoleExistsAsync(role.Name!).Result)
                            roleManager.CreateAsync(role).Wait();
                    }
                }

                if (!hasUsers)
                {
                    var mainAdmin = new ApplicationUser()
                    {
                        FirstName = "Saif",
                        LastName = "Hamza",
                        UserName = "SaifHamza",
                        Email = "SaifHamza@gmail.com",
                        PhoneNumber = "01033659272"
                    };

                    // ✅ Check result before assigning role
                    var mainAdminResult = userManager.CreateAsync(mainAdmin, "S@ifabozaid123").Result;
                    if (mainAdminResult.Succeeded)
                        userManager.AddToRoleAsync(mainAdmin, "SuperAdmin").Wait();
                    else
                        foreach (var err in mainAdminResult.Errors)
                            Console.WriteLine($"[Seed Error] {err.Code}: {err.Description}");

                    var admin = new ApplicationUser()
                    {
                        FirstName = "Mohamed",
                        LastName = "Hamza",
                        UserName = "MohamedHamza",
                        Email = "MohamedHamza@gmail.com",
                        PhoneNumber = "01033659373"
                    };

                    // ✅ Check result before assigning role
                    var adminResult = userManager.CreateAsync(admin, "Moh@medabozaid123").Result;
                    if (adminResult.Succeeded)
                        userManager.AddToRoleAsync(admin, "Admin").Wait();
                    else
                        foreach (var err in adminResult.Errors)
                            Console.WriteLine($"[Seed Error] {err.Code}: {err.Description}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to seed {ex}");
                return false;
            }
        }
    }
}