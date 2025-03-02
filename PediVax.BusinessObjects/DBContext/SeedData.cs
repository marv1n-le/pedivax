using Microsoft.EntityFrameworkCore;
using PediVax.BusinessObjects.Models;
using PediVax.BusinessObjects.Helpers;

namespace PediVax.BusinessObjects.DBContext;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Tạo mật khẩu mặc định (password: Admin@123)
        string salt = PasswordHelper.GenerateSalt();
        string hash = PasswordHelper.HashPassword("Admin@123", salt);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FullName = "System Admin",
                Email = "admin@pedivax.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                PhoneNumber = "0123456789",
                Address = "PediVax HCM",
                Role = Enum.EnumList.Role.Admin,
                DateOfBirth = new DateTime(1990, 1, 1),
                IsActive = Enum.EnumList.IsActive.Active,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "System",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "System"
            }
        );
    }
}