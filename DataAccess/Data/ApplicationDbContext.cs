using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<HotelRoom> HotelRooms { get; set; }
    public DbSet<HotelRoomImage> HotelRoomImages { get; set; }
    public DbSet<HotelAmenity> HotelAmenities { get; set; }
}
