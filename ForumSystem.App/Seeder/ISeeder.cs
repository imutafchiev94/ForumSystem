using ForumSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.App.Seeder
{
    public interface ISeeder
    {

        Task SeedAsync(ForumSystemDbContext dbContext, IServiceProvider serviceProvider);

    }
}
