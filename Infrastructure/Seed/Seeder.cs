using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seed;

public class Seeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
{
    public async Task<bool> SeedUser()
    {
        var existing = await userManager.FindByNameAsync("admin");
        if (existing != null) return false;

        var user = new User()
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            Address = "Dushanbe"
        };

        var result = await userManager.CreateAsync(user, "Deluxe123!");
        if (!result.Succeeded) return false;

        await userManager.AddToRoleAsync(user, Roles.Admin);
        return true;

    }

    public async Task<bool> SeedRole()
    {

        var newroles = new List<IdentityRole>()
        {
            new IdentityRole(Roles.Admin),
            new IdentityRole(Roles.Manager),
            new IdentityRole(Roles.User),
        };

        var roles = await roleManager.Roles.ToListAsync();

        foreach (var role in newroles)
        {
            if (roles.Exists(e => e.Name == role.Name))
            {
                continue;
            }
            await roleManager.CreateAsync(role);
        }

        return true;
    }
}

public static class Roles
{
    public const string Admin = "Admin";
    public const string Manager = "Moderator";
    public const string User = "User";
}