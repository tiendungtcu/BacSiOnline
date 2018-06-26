using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace TM.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole,
    CustomUserClaim>
    {
        // Các thuộc tính thêm vào bảng user
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Address { get; set; }
        public string FullName { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool? IsOnline { get; set; }

        public string Avatar { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser,int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaims(new List<Claim>()
            {
                new Claim("FullName", (this.FullName!=null)?this.FullName:this.UserName),
                new Claim("Email", (this.Email!=null)?this.Email:this.UserName),
                //new Claim("Address", (this.Address!=null)?this.Address:""),
                
            });
            return userIdentity;
        }
    }

    public class MyIdentityDbContext : IdentityDbContext<ApplicationUser, CustomRole,
    int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public MyIdentityDbContext()
            : base("IdentityDBContext"/*, throwIfV1Schema: false*/)
        {
            //Database.SetInitializer<MyIdentityDbContext>(null); // Remove default initializer
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;
        }

        public static MyIdentityDbContext Create()
        {
            return new MyIdentityDbContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // custom User table
            modelBuilder.Entity<ApplicationUser>().ToTable("TM_Users");
            modelBuilder.Entity<ApplicationUser>().Property(p => p.FullName).HasMaxLength(150);
            modelBuilder.Entity<ApplicationUser>().Property(p => p.Gender).HasMaxLength(1);
            //modelBuilder.Entity<ApplicationUser>().Property(p => p.Address).HasMaxLength(250);
            // custom Role table 
            modelBuilder.Entity<CustomRole>().ToTable("TM_Roles");
            modelBuilder.Entity<CustomRole>().Property(u => u.Description).HasMaxLength(100);
            // custom UserClaim table  
            modelBuilder.Entity<CustomUserClaim>().ToTable("TM_UserClaims");
            modelBuilder.Entity<CustomUserClaim>().Property(u => u.ClaimType).HasMaxLength(150);
            modelBuilder.Entity<CustomUserClaim>().Property(u => u.ClaimValue).HasMaxLength(500);
            // custom UserLogin table
            modelBuilder.Entity<CustomUserLogin>().ToTable("TM_UserLogins");
            // custom UserRole Table
            modelBuilder.Entity<CustomUserRole>().ToTable("TM_UserRoles");
           


        }

    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
        public CustomRole(string name, string description)
        { Name = name; Description = description; }
        public string Description { get; set; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(MyIdentityDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(MyIdentityDbContext context)
            : base(context)
        {
        }
    }
}