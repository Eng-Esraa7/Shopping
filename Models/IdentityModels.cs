using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Shopping.Controllers;
using Shopping.Models;

namespace Shopping.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<shoes> shoes { get; set; }
        public DbSet<accessories> accessories { get; set; }
        public DbSet<Dresses> Dresses { get; set; }
        public DbSet<suits> suits { get; set; }
        public DbSet<shorts> shorts { get; set; }
        public DbSet<pants> pants { get; set; }
        public DbSet<shirts> shirts { get; set; }
        public DbSet<children> childrens { get; set; }
        public DbSet<ContactUs> contactUs  { get; set; }
        public DbSet<infoOfCart> infoOfCart  { get; set; }
        public DbSet<checkOut> checkOut  { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}