using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using abpCorrelation.Domain.Products;

namespace abpCorrelation.EntityFrameworkCore.Products;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Sku).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Cost).HasColumnType("decimal(18,2)");
        builder.Property(x => x.Category).HasMaxLength(100);
        builder.Property(x => x.Brand).HasMaxLength(100);
        builder.Property(x => x.Images).HasMaxLength(1000);
        builder.Property(x => x.Tags).HasMaxLength(500);
        builder.Property(x => x.Status).HasConversion<int>();
        // Add more property configurations as needed
    }
} 