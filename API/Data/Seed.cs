using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.Json;
using API.Entites;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace API.Data
{
    public static class Seed
    {
        public async static Task<bool> SeedUsers(DataContext dbContext)
        {
            if (await dbContext.Users.AnyAsync()) return true;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Shreya"));
                user.PasswordSalt=hmac.Key;
                user.UserName=user.UserName.ToLower();

                dbContext.Users.Add(user);

            }
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}