using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abpCorrelation.Migrations
{
    /// <inheritdoc />
    public partial class updatedProducttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Sku = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Brand = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    Dimensions = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    Size = table.Column<string>(type: "text", nullable: true),
                    Material = table.Column<string>(type: "text", nullable: true),
                    Images = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Tags = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Specifications = table.Column<string>(type: "text", nullable: true),
                    Warranty = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StockQuantity = table.Column<int>(type: "integer", nullable: false),
                    MinStockLevel = table.Column<int>(type: "integer", nullable: false),
                    MaxStockLevel = table.Column<int>(type: "integer", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    ReviewCount = table.Column<int>(type: "integer", nullable: false),
                    PopularityScore = table.Column<int>(type: "integer", nullable: false),
                    IsFeatured = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LaunchDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Barcode = table.Column<string>(type: "text", nullable: true),
                    Manufacturer = table.Column<string>(type: "text", nullable: true),
                    CountryOfOrigin = table.Column<string>(type: "text", nullable: true),
                    ShippingClass = table.Column<string>(type: "text", nullable: true),
                    TaxClass = table.Column<string>(type: "text", nullable: true),
                    SeoTitle = table.Column<string>(type: "text", nullable: true),
                    SeoDescription = table.Column<string>(type: "text", nullable: true),
                    SeoKeywords = table.Column<string>(type: "text", nullable: true),
                    MetaData = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
