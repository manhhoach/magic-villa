using MagicVilla_VillaAPI.Models.Villa;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<VillaModel> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VillaModel>().HasData(
                new VillaModel()
                {
                    Id = 1,
                    Amenity = "vip",
                    Details = "no details",
                    ImageUrl = "no.jpg",
                    Name = "villa 1",
                    Occupancy = 10,
                    Rate = 10,
                    Sqft = 10,
                    CreatedDate = DateTime.Now

                },
                new VillaModel()
                {
                    Id = 2,
                    Amenity = "vip",
                    Details = "no details",
                    ImageUrl = "no.jpg",
                    Name = "villa 1",
                    Occupancy = 10,
                    Rate = 10,
                    Sqft = 10,
                    CreatedDate = DateTime.Now
                },
                 new VillaModel()
                 {
                     Id = 3,
                     Amenity = "vip",
                     Details = "no details",
                     ImageUrl = "no.jpg",
                     Name = "villa 1",
                     Occupancy = 10,
                     Rate = 10,
                     Sqft = 10,
                     CreatedDate = DateTime.Now
                 },
                new VillaModel()
                {
                    Id = 4,
                    Amenity = "vip",
                    Details = "no details",
                    ImageUrl = "no.jpg",
                    Name = "villa 1",
                    Occupancy = 10,
                    Rate = 10,
                    Sqft = 10,
                    CreatedDate = DateTime.Now

                }
             );
        }
    }
}
