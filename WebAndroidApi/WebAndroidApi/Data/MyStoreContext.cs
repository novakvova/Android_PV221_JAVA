using Microsoft.EntityFrameworkCore;
using WebAndroidApi.Data.Entities;

namespace WebAndroidApi.Data
{
    public class MyStoreContext : DbContext
    {
        public MyStoreContext(DbContextOptions<MyStoreContext> options) : base(options) { }

        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
