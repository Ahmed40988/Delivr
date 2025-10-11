
namespace Deliver.Dal.Data;

public class ApplicationDbContext:IdentityDbContext<ApplicationUser,IdentityRole<int>,int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<VehicleType> vehicleTypes { get; set; }
    public DbSet<Government> governments { get; set; }
    public DbSet<City> cities { get; set; }
    public DbSet<Zone> zones { get; set; }
    public DbSet<Street> streets { get; set; }
    public DbSet<Address> addresses { get; set; }
    public DbSet<ParentCategory> parentCategories { get; set; }
    public DbSet<SubCategory> subCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurations();

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SubCategory>()
            .HasOne(s => s.ParentCategory)
            .WithMany(p => p.SubCategories)
            .HasForeignKey(s => s.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Supplier>()
            .HasOne(s => s.SubCategory)
            .WithMany(sub => sub.Suppliers)
            .HasForeignKey(s => s.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
