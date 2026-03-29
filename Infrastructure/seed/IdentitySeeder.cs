using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class AdminSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        // -----------------------
        // Seed Admin Role
        // -----------------------
        Console.WriteLine("entered to seeding");
        const string adminRole = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRole))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(adminRole));
        }

        // -----------------------
        // Seed Admin User
        // -----------------------
     

        var existingAdmin = await userManager.FindByEmailAsync("admin@daraliftaa.com");

        if (existingAdmin == null)
        {
            var adminUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Email = "admin@daraliftaa.com",
              PhoneNumber=" ",

                // ✅ أضف الـ Required Fields
                FirstName = "Admin",
                LastName = "System",
                Gender = Gender.male,
                BirthDate = DateTime.Now.Date,
                IsDeleted = false,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                AccessFailedCount = 0,
                // String fields - خليها empty مش null
                AcademicQualification = " ",
                AcademicTitle = " ",
                AddressInsideCairo = " ",
                AddressOutsideCairo = " ",
                Appreciation = " ",
                Doctrine = " ",
                ImagePath = " ",
                JobTitle = " ",
                NationalIdImage = " ",
                Organization = " ",
                Skills = " ",
                Specialization = " ",
                WhatsappNumber = " ",
                pin = "1",
                MaritalState = " ",

            };

            var result = await userManager.CreateAsync(adminUser, "Admin@1234");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to seed admin: {errors}");
            }
        }
        else
        {
            // لو الأدمن موجود بس مش في الـ Role، حطه فيها
            if (!await userManager.IsInRoleAsync(existingAdmin, adminRole))
            {
                await userManager.AddToRoleAsync(existingAdmin, adminRole);
            }
        }
    }
}
