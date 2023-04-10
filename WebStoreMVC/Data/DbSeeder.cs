namespace WebStoreMVC.Data
{
    public class DbSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            //add roles to database
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString())); //customers

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
            };

            var isUserInDatabase = await userManager.FindByEmailAsync(admin.Email);
            if (isUserInDatabase is null)
            {
                await userManager.CreateAsync(admin, "2000.Admin");
                await userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}