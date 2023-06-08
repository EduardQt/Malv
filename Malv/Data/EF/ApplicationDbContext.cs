using Malv.Data.EF.Entity;
using Malv.Data.EF.Entity.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Malv.Data.EF;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MalvUser> Users { get; set; }
    public DbSet<UserData> UserDatas { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Ad> Ads { get; set; }
    public DbSet<AdImage> AdImages { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Municipality> Municipalities { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<MailToken> MailTokens { get; set; }
    public DbSet<AdWatch> AdWatches { get; set; }

    public bool HasTransaction { get; set; } = false;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne(x => x.Parent)
            .WithMany(x => x.Children)
            .HasForeignKey(x => x.ParentId).IsRequired(false);

        modelBuilder.Entity<Category>().ToTable("Category");

        modelBuilder.Entity<Ad>().ToTable("Ad");
        modelBuilder.Entity<Ad>()
            .HasOne(x => x.User)
            .WithMany(x => x.Ads)
            .HasForeignKey(x => x.UserId);
        modelBuilder.Entity<Ad>()
            .HasOne(x => x.Municipality)
            .WithMany(x => x.Ads)
            .HasForeignKey(x => x.MunicipalityId);

        modelBuilder.Entity<AdImage>().ToTable("AdImage");
        modelBuilder.Entity<AdImage>()
            .HasOne(x => x.Ad)
            .WithMany(x => x.AdImages)
            .HasForeignKey(x => x.AdId);

        modelBuilder.Entity<MalvUser>().ToTable("User");

        modelBuilder.Entity<UserData>().ToTable("UserData");
        modelBuilder.Entity<UserData>()
            .HasOne(x => x.User)
            .WithOne(x => x.UserData)
            .HasForeignKey<UserData>(x => x.UserId);

        modelBuilder.Entity<CarAd>().ToTable("CarAd");
        modelBuilder.Entity<CarAd>()
            .HasOne(x => x.Ad)
            .WithOne(x => x.CarAd)
            .HasForeignKey<CarAd>(x => x.AdId);

        modelBuilder.Entity<Chat>().ToTable("Chat");

        modelBuilder.Entity<ChatMessage>().ToTable("ChatMessage");
        modelBuilder.Entity<ChatMessage>()
            .HasOne(x => x.Chat)
            .WithMany(x => x.ChatMessages)
            .HasForeignKey(x => x.ChatId);

        modelBuilder.Entity<ChatMessage>()
            .HasOne(x => x.User)
            .WithMany(x => x.ChatMessages)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<Municipality>().ToTable("Municipality");
        modelBuilder.Entity<Municipality>()
            .HasOne(x => x.Country)
            .WithMany(x => x.Municipalities)
            .HasForeignKey(x => x.CountryId);

        modelBuilder.Entity<Country>().ToTable("Country");

        modelBuilder.Entity<MailToken>().ToTable("MailToken");
        modelBuilder.Entity<MailToken>()
            .HasOne(a => a.User)
            .WithMany(w => w.MailTokens)
            .HasForeignKey(x => x.UserId);

        modelBuilder.Entity<AdWatch>().ToTable("AdWatch");
        modelBuilder.Entity<AdWatch>()
            .HasOne(h => h.Ad)
            .WithMany(h => h.AdWatches)
            .HasForeignKey(h => h.AdId);

        modelBuilder.Entity<AdWatch>()
            .HasOne(h => h.User)
            .WithMany(h => h.AdWatches)
            .HasForeignKey(h => h.UserId);

        modelBuilder.Entity<AdCategory>().ToTable("AdCategory");
        modelBuilder.Entity<AdCategory>()
            .HasOne(h => h.Ad)
            .WithMany(h => h.AdCategories)
            .HasForeignKey(h => h.AdId);
        
        modelBuilder.Entity<AdCategory>()
            .HasOne(h => h.Category)
            .WithMany(h => h.AdCategories)
            .HasForeignKey(h => h.CategoryId);
        
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        throw new Exception("Here");
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        if (!HasTransaction)
            throw new Exception("Here1");
        return base.SaveChangesAsync(cancellationToken);
    }
}