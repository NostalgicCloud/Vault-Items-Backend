using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SQLiteMinecraftApi.Context
{
    [Table("minecraftitems")]
    public class MinecraftItem
    {
        [Key]
        [Required]
        [MaxLength(255)]
        public string UUID { get; set; }

        [MaxLength(1000)]
        public string item1 { get; set; }

        [MaxLength(1000)]
        public string item2 { get; set; }

        [MaxLength(1000)]
        public string item3 { get; set; }

        [MaxLength(1000)]
        public string item4 { get; set; }

        [MaxLength(1000)]
        public string item5 { get; set; }

        [MaxLength(1000)]
        public string item6 { get; set; }

        [MaxLength(1000)]
        public string item7 { get; set; }

        [MaxLength(1000)]
        public string item8 { get; set; }

        [MaxLength(1000)]
        public string item9 { get; set; }
    }

    public class MinecraftContext : DbContext
    {
        public DbSet<MinecraftItem> MinecraftItems { get; set; }

        public MinecraftContext(DbContextOptions<MinecraftContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=minecraftprod;User=root;Password=admin;Port=3306;",
                new MySqlServerVersion(new Version(8, 0, 21)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MinecraftItem>(entity =>
            {
                entity.ToTable("minecraftitems");
                entity.Property(e => e.item1).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item2).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item3).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item4).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item5).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item6).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item7).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item8).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
                entity.Property(e => e.item9).HasColumnType("TEXT").HasMaxLength(100).IsRequired(false);
            });
        }

        public async Task<List<MinecraftItem>> GetItemsByUUID(string uuid)
        {
            return await MinecraftItems.Where(item => item.UUID == uuid).ToListAsync();
        }
    }

}