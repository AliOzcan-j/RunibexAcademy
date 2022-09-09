using Entities.Concrete;
using Entities.DTOs.Car;
using Entities.DTOs.CarImage;
using Entities.DTOs.Payment;
using Entities.DTOs.Rental;
using Entities.DTOs.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameworkCore
{
    public class AcademyContext : DbContext
    {
        public AcademyContext()
        {
        }

        public AcademyContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<CarDetailDto> CarDetailDtos { get; set; }
        public DbSet<UserDetailDto> UserDetailDtos { get; set; }
        public DbSet<UserForLoginDto> UserForLoginDtos { get; set; }
        public DbSet<UserForRegisterDto> UserForRegisterDtos { get; set; }
        public DbSet<CarImageListDto> carImageListDtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("Default");

                optionsBuilder
                .LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information)
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options =>
                 {
                     options.EnableStringComparisonTranslations();
                 });
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserModel
            modelBuilder.Entity<User>().HasOne(x => x.Supplier).WithOne(x => x.User).HasForeignKey<Supplier>(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnType("varchar(320)").IsUnicode(false);
            modelBuilder.Entity<User>().Property(x => x.FirstName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<User>().Property(x => x.LastName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<User>().Property(x => x.ContactNumber).HasColumnType("varchar(50)");
            modelBuilder.Entity<User>().Property(x => x.PasswordSalt).HasColumnType("varbinary(500)");
            modelBuilder.Entity<User>().Property(x => x.PasswordHash).HasColumnType("varbinary(500)");
            modelBuilder.Entity<User>().Property(x => x.Stasus).HasDefaultValue(true);
            #endregion

            #region SupplierModel
            modelBuilder.Entity<Supplier>().Property(x => x.CompanyName).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Supplier>().Property(x => x.Address).HasColumnType("nvarchar(100)");
            modelBuilder.Entity<Supplier>().Property(x => x.Postcode).HasColumnType("varchar(8)");
            #endregion

            #region RentalModel
            modelBuilder.Entity<Rental>().Property(x => x.RentDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<Rental>().HasOne(x => x.Payment).WithOne(x => x.Rental).HasForeignKey<Rental>(x => x.PaymentId);
            modelBuilder.Entity<Rental>().HasOne(x => x.Car).WithMany(x => x.Rentals).HasForeignKey(x => x.CarId);
            modelBuilder.Entity<Rental>().HasOne(x => x.User).WithMany(x => x.Rentals).HasForeignKey(x => x.UserId);
            #endregion

            #region PaymentModel
            modelBuilder.Entity<Payment>().Property(x => x.PaymentDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<Payment>().Property(x => x.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Payment>().HasOne(x => x.User).WithMany(x => x.Payments).HasForeignKey(x => x.UserId);
            #endregion

            #region OpeationClaimModel
            modelBuilder.Entity<OperationClaim>().Property(x => x.Name).HasColumnType("varchar(50)");
            #endregion

            #region ModelModel
            modelBuilder.Entity<Model>().Property(x => x.NamePrefix).HasColumnType("varchar(50)");
            modelBuilder.Entity<Model>().Property(x => x.NameSuffix).HasColumnType("varchar(50)");
            modelBuilder.Entity<Model>().Property(x => x.ModelYear).HasColumnType("varchar(15)");
            modelBuilder.Entity<Model>().HasOne(x => x.Brand).WithMany(x => x.Models).HasForeignKey(x => x.BrandId);
            #endregion

            #region FuelTypeModel
            modelBuilder.Entity<FuelType>().Property(x => x.Name).HasColumnType("varchar(50)");
            #endregion

            #region CurrencyModel
            modelBuilder.Entity<Currency>().Property(x => x.Name).HasColumnType("varchar(50)");
            modelBuilder.Entity<Currency>().Property(x => x.IsoCode).HasColumnType("varchar(10)");
            #endregion

            #region CountryModel
            modelBuilder.Entity<Country>().Property(x => x.Name).HasColumnType("nvarchar(50)");
            modelBuilder.Entity<Country>().Property(x => x.CountryCode).HasColumnType("varchar(10)");
            modelBuilder.Entity<Country>().Property(x => x.IsoCode).HasColumnType("varchar(10)");
            #endregion

            #region ColorModel
            modelBuilder.Entity<Color>().Property(x => x.Name).HasColumnType("varchar(15)");
            #endregion

            #region CarImageModel
            modelBuilder.Entity<CarImage>().Property(x => x.EditDate).ValueGeneratedOnAdd();
            #endregion

            #region CarModel
            modelBuilder.Entity<Car>().Property(x => x.MilageLimit).HasDefaultValue(true);
            modelBuilder.Entity<Car>().Property(x => x.DailyPrice).HasPrecision(18, 2);
            modelBuilder.Entity<Car>().Property(x => x.IsDeleted).HasDefaultValue(false);
            modelBuilder.Entity<Car>().HasOne(x => x.Supplier).WithMany(x => x.Cars).HasForeignKey(x => x.SupplierId);
            modelBuilder.Entity<Car>().HasOne(x => x.Model).WithMany(x => x.Cars).HasForeignKey(x => x.ModelId);
            modelBuilder.Entity<Car>().HasOne(x => x.Color).WithMany(x => x.Cars).HasForeignKey(x => x.ColorId);
            modelBuilder.Entity<Car>().HasOne(x => x.FuelType).WithMany(x => x.Cars).HasForeignKey(x => x.FuelTypeId);
            #endregion

            #region BrandModel
            modelBuilder.Entity<Brand>().Property(x => x.Name).HasColumnType("nvarchar(50)");
            #endregion

            #region DtoModels
            modelBuilder.Entity<CarDetailDto>().HasNoKey().ToTable(nameof(CarDetailDto), x => x.ExcludeFromMigrations());
            modelBuilder.Entity<UserDetailDto>().HasNoKey().ToTable(nameof(UserDetailDto), x => x.ExcludeFromMigrations());
            modelBuilder.Entity<UserForLoginDto>().HasNoKey().ToTable(nameof(UserForLoginDto), x => x.ExcludeFromMigrations());
            modelBuilder.Entity<UserForRegisterDto>().HasNoKey().ToTable(nameof(UserForRegisterDto), x => x.ExcludeFromMigrations());
            modelBuilder.Entity<CarImageListDto>().HasNoKey().ToTable(nameof(carImageListDtos), x => x.ExcludeFromMigrations());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
