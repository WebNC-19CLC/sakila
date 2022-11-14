using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using sakilaAppMySQL.Infrastructure.Domain.Entities.Authentication;

namespace sakilaAppMySQL.Infrastructure.Context
{
  public partial class sakilaContext :  IdentityDbContext<ApplicationUser>
  {
    public sakilaContext()
    {
    }

    public sakilaContext(DbContextOptions<sakilaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; } = null!;
    public virtual DbSet<ActorInfo> ActorInfos { get; set; } = null!;
    public virtual DbSet<Address> Addresses { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<City> Cities { get; set; } = null!;
    public virtual DbSet<Country> Countries { get; set; } = null!;
    public virtual DbSet<Customer> Customers { get; set; } = null!;
    public virtual DbSet<CustomerList> CustomerLists { get; set; } = null!;
    public virtual DbSet<Film> Films { get; set; } = null!;
    public virtual DbSet<FilmActor> FilmActors { get; set; } = null!;
    public virtual DbSet<FilmCategory> FilmCategories { get; set; } = null!;
    public virtual DbSet<FilmList> FilmLists { get; set; } = null!;
    public virtual DbSet<FilmText> FilmTexts { get; set; } = null!;
    public virtual DbSet<Inventory> Inventories { get; set; } = null!;
    public virtual DbSet<Language> Languages { get; set; } = null!;
    public virtual DbSet<NicerButSlowerFilmList> NicerButSlowerFilmLists { get; set; } = null!;
    public virtual DbSet<Payment> Payments { get; set; } = null!;
    public virtual DbSet<Rental> Rentals { get; set; } = null!;
    public virtual DbSet<SalesByFilmCategory> SalesByFilmCategories { get; set; } = null!;
    public virtual DbSet<SalesByStore> SalesByStores { get; set; } = null!;
    public virtual DbSet<StaffList> StaffLists { get; set; } = null!;
    public virtual DbSet<Store> Stores { get; set; } = null!;
    public virtual DbSet<staff> staff { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        optionsBuilder.UseMySql("server=localhost;user=root;password=P@ssw0rd;database=sakila", ServerVersion.Parse("8.0.31-mysql"));
      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
          .HasCharSet("utf8mb4");

      modelBuilder.Entity<Actor>(entity =>
      {
        entity.ToTable("actor");

        entity.HasIndex(e => e.LastName, "idx_actor_last_name");

        entity.Property(e => e.ActorId).HasColumnName("actor_id");

        entity.Property(e => e.FirstName)
                  .HasMaxLength(45)
                  .HasColumnName("first_name");

        entity.Property(e => e.LastName)
                  .HasMaxLength(45)
                  .HasColumnName("last_name");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
      });

      modelBuilder.Entity<ActorInfo>(entity =>
      {
        entity.HasNoKey();

        entity.ToView("actor_info");

        entity.Property(e => e.ActorId).HasColumnName("actor_id");

        entity.Property(e => e.FilmInfo)
                  .HasColumnType("text")
                  .HasColumnName("film_info");

        entity.Property(e => e.FirstName)
                  .HasMaxLength(45)
                  .HasColumnName("first_name");

        entity.Property(e => e.LastName)
                  .HasMaxLength(45)
                  .HasColumnName("last_name");
      });

      modelBuilder.Entity<Address>(entity =>
      {
        entity.ToTable("address");

        entity.HasIndex(e => e.CityId, "idx_fk_city_id");

        entity.Property(e => e.AddressId).HasColumnName("address_id");

        entity.Property(e => e.Address1)
                  .HasMaxLength(50)
                  .HasColumnName("address");

        entity.Property(e => e.Address2)
                  .HasMaxLength(50)
                  .HasColumnName("address2");

        entity.Property(e => e.CityId).HasColumnName("city_id");

        entity.Property(e => e.District)
                  .HasMaxLength(20)
                  .HasColumnName("district");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.Phone)
                  .HasMaxLength(20)
                  .HasColumnName("phone");

        entity.Property(e => e.PostalCode)
                  .HasMaxLength(10)
                  .HasColumnName("postal_code");

        entity.HasOne(d => d.City)
                  .WithMany(p => p.Addresses)
                  .HasForeignKey(d => d.CityId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_address_city");
      });

      modelBuilder.Entity<Category>(entity =>
      {
        entity.ToTable("category");

        entity.Property(e => e.CategoryId)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("category_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.Name)
                  .HasMaxLength(25)
                  .HasColumnName("name");
      });

      modelBuilder.Entity<City>(entity =>
      {
        entity.ToTable("city");

        entity.HasIndex(e => e.CountryId, "idx_fk_country_id");

        entity.Property(e => e.CityId).HasColumnName("city_id");

        entity.Property(e => e.City1)
                  .HasMaxLength(50)
                  .HasColumnName("city");

        entity.Property(e => e.CountryId).HasColumnName("country_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.HasOne(d => d.Country)
                  .WithMany(p => p.Cities)
                  .HasForeignKey(d => d.CountryId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_city_country");
      });

      modelBuilder.Entity<Country>(entity =>
      {
        entity.ToTable("country");

        entity.Property(e => e.CountryId).HasColumnName("country_id");

        entity.Property(e => e.Country1)
                  .HasMaxLength(50)
                  .HasColumnName("country");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");
      });

      modelBuilder.Entity<Customer>(entity =>
      {
        entity.ToTable("customer");

        entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

        entity.HasIndex(e => e.StoreId, "idx_fk_store_id");

        entity.HasIndex(e => e.LastName, "idx_last_name");

        entity.Property(e => e.CustomerId).HasColumnName("customer_id");

        entity.Property(e => e.Active)
                  .IsRequired()
                  .HasColumnName("active")
                  .HasDefaultValueSql("'1'");

        entity.Property(e => e.AddressId).HasColumnName("address_id");

        entity.Property(e => e.CreateDate)
                  .HasColumnType("datetime")
                  .HasColumnName("create_date");

        entity.Property(e => e.Email)
                  .HasMaxLength(50)
                  .HasColumnName("email");

        entity.Property(e => e.FirstName)
                  .HasMaxLength(45)
                  .HasColumnName("first_name");

        entity.Property(e => e.LastName)
                  .HasMaxLength(45)
                  .HasColumnName("last_name");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.StoreId).HasColumnName("store_id");

        entity.HasOne(d => d.Address)
                  .WithMany(p => p.Customers)
                  .HasForeignKey(d => d.AddressId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_customer_address");

        entity.HasOne(d => d.Store)
                  .WithMany(p => p.Customers)
                  .HasForeignKey(d => d.StoreId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_customer_store");
      });

      modelBuilder.Entity<CustomerList>(entity =>
      {
        entity.HasNoKey();

        entity.ToView("customer_list");

        entity.Property(e => e.Address)
                  .HasMaxLength(50)
                  .HasColumnName("address");

        entity.Property(e => e.City)
                  .HasMaxLength(50)
                  .HasColumnName("city");

        entity.Property(e => e.Country)
                  .HasMaxLength(50)
                  .HasColumnName("country");

        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Name)
                  .HasMaxLength(91)
                  .HasColumnName("name");

        entity.Property(e => e.Notes)
                  .HasMaxLength(6)
                  .HasColumnName("notes")
                  .HasDefaultValueSql("''");

        entity.Property(e => e.Phone)
                  .HasMaxLength(20)
                  .HasColumnName("phone");

        entity.Property(e => e.Sid).HasColumnName("SID");

        entity.Property(e => e.ZipCode)
                  .HasMaxLength(10)
                  .HasColumnName("zip code");
      });

      modelBuilder.Entity<Film>(entity =>
      {
        entity.ToTable("film");

        entity.HasIndex(e => e.LanguageId, "idx_fk_language_id");

        entity.HasIndex(e => e.OriginalLanguageId, "idx_fk_original_language_id");

        entity.HasIndex(e => e.Title, "idx_title");

        entity.Property(e => e.FilmId).HasColumnName("film_id");

        entity.Property(e => e.Description)
                  .HasColumnType("text")
                  .HasColumnName("description");

        entity.Property(e => e.LanguageId).HasColumnName("language_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.Length).HasColumnName("length");

        entity.Property(e => e.OriginalLanguageId).HasColumnName("original_language_id");

        entity.Property(e => e.Rating)
                  .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                  .HasColumnName("rating")
                  .HasDefaultValueSql("'G'");

        entity.Property(e => e.ReleaseYear)
                  .HasColumnType("year")
                  .HasColumnName("release_year");

        entity.Property(e => e.RentalDuration)
                  .HasColumnName("rental_duration")
                  .HasDefaultValueSql("'3'");

        entity.Property(e => e.RentalRate)
                  .HasPrecision(4, 2)
                  .HasColumnName("rental_rate")
                  .HasDefaultValueSql("'4.99'");

        entity.Property(e => e.ReplacementCost)
                  .HasPrecision(5, 2)
                  .HasColumnName("replacement_cost")
                  .HasDefaultValueSql("'19.99'");

        entity.Property(e => e.Title)
                  .HasMaxLength(128)
                  .HasColumnName("title");

        entity.HasOne(d => d.Language)
                  .WithMany(p => p.FilmLanguages)
                  .HasForeignKey(d => d.LanguageId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_film_language");

        entity.HasOne(d => d.OriginalLanguage)
                  .WithMany(p => p.FilmOriginalLanguages)
                  .HasForeignKey(d => d.OriginalLanguageId)
                  .HasConstraintName("fk_film_language_original");
      });

      modelBuilder.Entity<FilmActor>(entity =>
      {
        entity.HasKey(e => new { e.ActorId, e.FilmId })
                  .HasName("PRIMARY")
                  .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        entity.ToTable("film_actor");

        entity.HasIndex(e => e.FilmId, "idx_fk_film_id");

        entity.Property(e => e.ActorId).HasColumnName("actor_id");

        entity.Property(e => e.FilmId).HasColumnName("film_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.HasOne(d => d.Actor)
                  .WithMany(p => p.FilmActors)
                  .HasForeignKey(d => d.ActorId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_film_actor_actor");

        entity.HasOne(d => d.Film)
                  .WithMany(p => p.FilmActors)
                  .HasForeignKey(d => d.FilmId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_film_actor_film");
      });

      modelBuilder.Entity<FilmCategory>(entity =>
      {
        entity.HasKey(e => new { e.FilmId, e.CategoryId })
                  .HasName("PRIMARY")
                  .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        entity.ToTable("film_category");

        entity.HasIndex(e => e.CategoryId, "fk_film_category_category");

        entity.Property(e => e.FilmId).HasColumnName("film_id");

        entity.Property(e => e.CategoryId).HasColumnName("category_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.HasOne(d => d.Category)
                  .WithMany(p => p.FilmCategories)
                  .HasForeignKey(d => d.CategoryId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_film_category_category");

        entity.HasOne(d => d.Film)
                  .WithMany(p => p.FilmCategories)
                  .HasForeignKey(d => d.FilmId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_film_category_film");
      });

      modelBuilder.Entity<FilmList>(entity =>
      {
        entity.HasNoKey();

        entity.ToView("film_list");

        entity.Property(e => e.Actors)
                  .HasColumnType("text")
                  .HasColumnName("actors");

        entity.Property(e => e.Category)
                  .HasMaxLength(25)
                  .HasColumnName("category");

        entity.Property(e => e.Description)
                  .HasColumnType("text")
                  .HasColumnName("description");

        entity.Property(e => e.Fid)
                  .HasColumnName("FID")
                  .HasDefaultValueSql("'0'");

        entity.Property(e => e.Length).HasColumnName("length");

        entity.Property(e => e.Price)
                  .HasPrecision(4, 2)
                  .HasColumnName("price")
                  .HasDefaultValueSql("'4.99'");

        entity.Property(e => e.Rating)
                  .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                  .HasColumnName("rating")
                  .HasDefaultValueSql("'G'");

        entity.Property(e => e.Title)
                  .HasMaxLength(128)
                  .HasColumnName("title");
      });

      modelBuilder.Entity<FilmText>(entity =>
      {
        entity.HasKey(e => e.FilmId)
                  .HasName("PRIMARY");

        entity.ToTable("film_text");

        entity.HasIndex(e => new { e.Title, e.Description }, "idx_title_description")
                  .HasAnnotation("MySql:FullTextIndex", true);

        entity.Property(e => e.FilmId)
                  .ValueGeneratedNever()
                  .HasColumnName("film_id");

        entity.Property(e => e.Description)
                  .HasColumnType("text")
                  .HasColumnName("description");

        entity.Property(e => e.Title).HasColumnName("title");
      });

      modelBuilder.Entity<Inventory>(entity =>
      {
        entity.ToTable("inventory");

        entity.HasIndex(e => e.FilmId, "idx_fk_film_id");

        entity.HasIndex(e => new { e.StoreId, e.FilmId }, "idx_store_id_film_id");

        entity.Property(e => e.InventoryId)
                  .HasColumnType("mediumint unsigned")
                  .HasColumnName("inventory_id");

        entity.Property(e => e.FilmId).HasColumnName("film_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.StoreId).HasColumnName("store_id");

        entity.HasOne(d => d.Film)
                  .WithMany(p => p.Inventories)
                  .HasForeignKey(d => d.FilmId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_inventory_film");

        entity.HasOne(d => d.Store)
                  .WithMany(p => p.Inventories)
                  .HasForeignKey(d => d.StoreId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_inventory_store");
      });

      modelBuilder.Entity<Language>(entity =>
      {
        entity.ToTable("language");

        entity.Property(e => e.LanguageId)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("language_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.Name)
                  .HasMaxLength(20)
                  .HasColumnName("name")
                  .IsFixedLength();
      });

      modelBuilder.Entity<NicerButSlowerFilmList>(entity =>
      {
        entity.HasNoKey();

        entity.ToView("nicer_but_slower_film_list");

        entity.Property(e => e.Actors)
                  .HasColumnType("text")
                  .HasColumnName("actors");

        entity.Property(e => e.Category)
                  .HasMaxLength(25)
                  .HasColumnName("category");

        entity.Property(e => e.Description)
                  .HasColumnType("text")
                  .HasColumnName("description");

        entity.Property(e => e.Fid)
                  .HasColumnName("FID")
                  .HasDefaultValueSql("'0'");

        entity.Property(e => e.Length).HasColumnName("length");

        entity.Property(e => e.Price)
                  .HasPrecision(4, 2)
                  .HasColumnName("price")
                  .HasDefaultValueSql("'4.99'");

        entity.Property(e => e.Rating)
                  .HasColumnType("enum('G','PG','PG-13','R','NC-17')")
                  .HasColumnName("rating")
                  .HasDefaultValueSql("'G'");

        entity.Property(e => e.Title)
                  .HasMaxLength(128)
                  .HasColumnName("title");
      });

      modelBuilder.Entity<Payment>(entity =>
      {
        entity.ToTable("payment");

        entity.HasIndex(e => e.RentalId, "fk_payment_rental");

        entity.HasIndex(e => e.CustomerId, "idx_fk_customer_id");

        entity.HasIndex(e => e.StaffId, "idx_fk_staff_id");

        entity.Property(e => e.PaymentId).HasColumnName("payment_id");

        entity.Property(e => e.Amount)
                  .HasPrecision(5, 2)
                  .HasColumnName("amount");

        entity.Property(e => e.CustomerId).HasColumnName("customer_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.PaymentDate)
                  .HasColumnType("datetime")
                  .HasColumnName("payment_date");

        entity.Property(e => e.RentalId).HasColumnName("rental_id");

        entity.Property(e => e.StaffId).HasColumnName("staff_id");

        entity.HasOne(d => d.Customer)
                  .WithMany(p => p.Payments)
                  .HasForeignKey(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_payment_customer");

        entity.HasOne(d => d.Rental)
                  .WithMany(p => p.Payments)
                  .HasForeignKey(d => d.RentalId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("fk_payment_rental");

        entity.HasOne(d => d.Staff)
                  .WithMany(p => p.Payments)
                  .HasForeignKey(d => d.StaffId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_payment_staff");
      });

      modelBuilder.Entity<Rental>(entity =>
      {
        entity.ToTable("rental");

        entity.HasIndex(e => e.CustomerId, "idx_fk_customer_id");

        entity.HasIndex(e => e.InventoryId, "idx_fk_inventory_id");

        entity.HasIndex(e => e.StaffId, "idx_fk_staff_id");

        entity.HasIndex(e => new { e.RentalDate, e.InventoryId, e.CustomerId }, "rental_date")
                  .IsUnique();

        entity.Property(e => e.RentalId).HasColumnName("rental_id");

        entity.Property(e => e.CustomerId).HasColumnName("customer_id");

        entity.Property(e => e.InventoryId)
                  .HasColumnType("mediumint unsigned")
                  .HasColumnName("inventory_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.RentalDate)
                  .HasColumnType("datetime")
                  .HasColumnName("rental_date");

        entity.Property(e => e.ReturnDate)
                  .HasColumnType("datetime")
                  .HasColumnName("return_date");

        entity.Property(e => e.StaffId).HasColumnName("staff_id");

        entity.HasOne(d => d.Customer)
                  .WithMany(p => p.Rentals)
                  .HasForeignKey(d => d.CustomerId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_rental_customer");

        entity.HasOne(d => d.Inventory)
                  .WithMany(p => p.Rentals)
                  .HasForeignKey(d => d.InventoryId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_rental_inventory");

        entity.HasOne(d => d.Staff)
                  .WithMany(p => p.Rentals)
                  .HasForeignKey(d => d.StaffId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_rental_staff");
      });

      modelBuilder.Entity<SalesByFilmCategory>(entity =>
      {
        entity.HasNoKey();

        entity.ToView("sales_by_film_category");

        entity.Property(e => e.Category)
                  .HasMaxLength(25)
                  .HasColumnName("category");

        entity.Property(e => e.TotalSales)
                  .HasPrecision(27, 2)
                  .HasColumnName("total_sales");
      });

      modelBuilder.Entity<SalesByStore>(entity =>
      {
        entity.HasNoKey();

        entity.ToView("sales_by_store");

        entity.Property(e => e.Manager)
                  .HasMaxLength(91)
                  .HasColumnName("manager");

        entity.Property(e => e.Store)
                  .HasMaxLength(101)
                  .HasColumnName("store");

        entity.Property(e => e.TotalSales)
                  .HasPrecision(27, 2)
                  .HasColumnName("total_sales");
      });

      modelBuilder.Entity<StaffList>(entity =>
      {
        entity.HasNoKey();

        entity.ToView("staff_list");

        entity.Property(e => e.Address)
                  .HasMaxLength(50)
                  .HasColumnName("address");

        entity.Property(e => e.City)
                  .HasMaxLength(50)
                  .HasColumnName("city");

        entity.Property(e => e.Country)
                  .HasMaxLength(50)
                  .HasColumnName("country");

        entity.Property(e => e.Id).HasColumnName("ID");

        entity.Property(e => e.Name)
                  .HasMaxLength(91)
                  .HasColumnName("name");

        entity.Property(e => e.Phone)
                  .HasMaxLength(20)
                  .HasColumnName("phone");

        entity.Property(e => e.Sid).HasColumnName("SID");

        entity.Property(e => e.ZipCode)
                  .HasMaxLength(10)
                  .HasColumnName("zip code");
      });

      modelBuilder.Entity<Store>(entity =>
      {
        entity.ToTable("store");

        entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

        entity.HasIndex(e => e.ManagerStaffId, "idx_unique_manager")
                  .IsUnique();

        entity.Property(e => e.StoreId)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("store_id");

        entity.Property(e => e.AddressId).HasColumnName("address_id");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.ManagerStaffId).HasColumnName("manager_staff_id");

        entity.HasOne(d => d.Address)
                  .WithMany(p => p.Stores)
                  .HasForeignKey(d => d.AddressId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_store_address");

        entity.HasOne(d => d.ManagerStaff)
                  .WithOne(p => p.Store)
                  .HasForeignKey<Store>(d => d.ManagerStaffId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_store_staff");
      });

      modelBuilder.Entity<staff>(entity =>
      {
        entity.HasIndex(e => e.AddressId, "idx_fk_address_id");

        entity.HasIndex(e => e.StoreId, "idx_fk_store_id");

        entity.Property(e => e.StaffId)
                  .ValueGeneratedOnAdd()
                  .HasColumnName("staff_id");

        entity.Property(e => e.Active)
                  .IsRequired()
                  .HasColumnName("active")
                  .HasDefaultValueSql("'1'");

        entity.Property(e => e.AddressId).HasColumnName("address_id");

        entity.Property(e => e.Email)
                  .HasMaxLength(50)
                  .HasColumnName("email");

        entity.Property(e => e.FirstName)
                  .HasMaxLength(45)
                  .HasColumnName("first_name");

        entity.Property(e => e.LastName)
                  .HasMaxLength(45)
                  .HasColumnName("last_name");

        entity.Property(e => e.LastUpdate)
                  .HasColumnType("timestamp")
                  .ValueGeneratedOnAddOrUpdate()
                  .HasColumnName("last_update")
                  .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.Password)
                  .HasMaxLength(40)
                  .HasColumnName("password")
                  .UseCollation("utf8mb4_bin");

        entity.Property(e => e.Picture)
                  .HasColumnType("blob")
                  .HasColumnName("picture");

        entity.Property(e => e.StoreId).HasColumnName("store_id");

        entity.Property(e => e.Username)
                  .HasMaxLength(16)
                  .HasColumnName("username");

        entity.HasOne(d => d.Address)
                  .WithMany(p => p.staff)
                  .HasForeignKey(d => d.AddressId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_staff_address");

        entity.HasOne(d => d.StoreNavigation)
                  .WithMany(p => p.staff)
                  .HasForeignKey(d => d.StoreId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_staff_store");
      });

      base.OnModelCreating(modelBuilder);

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
