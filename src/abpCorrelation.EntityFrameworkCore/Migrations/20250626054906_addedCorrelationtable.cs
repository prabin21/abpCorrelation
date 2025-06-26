using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace abpCorrelation.Migrations
{
    /// <inheritdoc />
    public partial class addedCorrelationtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CorrelationLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CorrelationId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ParentCorrelationId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    TraceId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    SpanId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    OperationType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OperationName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    HttpMethod = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    HttpStatusCode = table.Column<int>(type: "integer", nullable: true),
                    RequestData = table.Column<string>(type: "text", nullable: true),
                    ResponseData = table.Column<string>(type: "text", nullable: true),
                    ErrorMessage = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    StackTrace = table.Column<string>(type: "text", nullable: true),
                    DurationMs = table.Column<long>(type: "bigint", nullable: false),
                    Metadata = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ClientIpAddress = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ApplicationName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Environment = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Severity = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorrelationLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_CorrelationId",
                table: "CorrelationLogs",
                column: "CorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_CorrelationId_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "CorrelationId", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_CreationTime",
                table: "CorrelationLogs",
                column: "CreationTime");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_DurationMs",
                table: "CorrelationLogs",
                column: "DurationMs");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_DurationMs_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "DurationMs", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_IsSuccess",
                table: "CorrelationLogs",
                column: "IsSuccess");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_IsSuccess_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "IsSuccess", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_OperationType",
                table: "CorrelationLogs",
                column: "OperationType");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_OperationType_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "OperationType", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_ParentCorrelationId",
                table: "CorrelationLogs",
                column: "ParentCorrelationId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_Severity",
                table: "CorrelationLogs",
                column: "Severity");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_Severity_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "Severity", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_TenantId_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "TenantId", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_TraceId",
                table: "CorrelationLogs",
                column: "TraceId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_Url_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "Url", "CreationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_UserId",
                table: "CorrelationLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorrelationLogs_UserId_CreationTime",
                table: "CorrelationLogs",
                columns: new[] { "UserId", "CreationTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CorrelationLogs");
        }
    }
}
