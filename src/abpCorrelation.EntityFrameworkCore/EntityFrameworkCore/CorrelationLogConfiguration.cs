using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using abpCorrelation.Domain.Correlation;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace abpCorrelation.EntityFrameworkCore.Correlation;

public class CorrelationLogConfiguration : IEntityTypeConfiguration<CorrelationLog>
{
    public void Configure(EntityTypeBuilder<CorrelationLog> builder)
    {
        builder.ToTable("CorrelationLogs");

        builder.ConfigureByConvention();

        // Primary key
        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.CorrelationId)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.ParentCorrelationId)
            .HasMaxLength(128);

        builder.Property(x => x.TraceId)
            .HasMaxLength(64);

        builder.Property(x => x.SpanId)
            .HasMaxLength(64);

        builder.Property(x => x.OperationType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.OperationName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.HttpMethod)
            .HasMaxLength(10);

        builder.Property(x => x.Url)
            .HasMaxLength(500);

        builder.Property(x => x.RequestData)
            .HasColumnType("text");

        builder.Property(x => x.ResponseData)
            .HasColumnType("text");

        builder.Property(x => x.ErrorMessage)
            .HasMaxLength(2000);

        builder.Property(x => x.StackTrace)
            .HasColumnType("text");

        builder.Property(x => x.Metadata)
            .HasColumnType("text");

        builder.Property(x => x.UserName)
            .HasMaxLength(256);

        builder.Property(x => x.ClientIpAddress)
            .HasMaxLength(64);

        builder.Property(x => x.UserAgent)
            .HasMaxLength(500);

        builder.Property(x => x.ApplicationName)
            .HasMaxLength(100);

        builder.Property(x => x.Environment)
            .HasMaxLength(20);

        builder.Property(x => x.Severity)
            .IsRequired()
            .HasMaxLength(20);

        // Indexes for performance
        builder.HasIndex(x => x.CorrelationId)
            .HasDatabaseName("IX_CorrelationLogs_CorrelationId");

        builder.HasIndex(x => x.ParentCorrelationId)
            .HasDatabaseName("IX_CorrelationLogs_ParentCorrelationId");

        builder.HasIndex(x => x.TraceId)
            .HasDatabaseName("IX_CorrelationLogs_TraceId");

        builder.HasIndex(x => x.OperationType)
            .HasDatabaseName("IX_CorrelationLogs_OperationType");

        builder.HasIndex(x => x.Severity)
            .HasDatabaseName("IX_CorrelationLogs_Severity");

        builder.HasIndex(x => x.IsSuccess)
            .HasDatabaseName("IX_CorrelationLogs_IsSuccess");

        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("IX_CorrelationLogs_UserId");

        builder.HasIndex(x => x.CreationTime)
            .HasDatabaseName("IX_CorrelationLogs_CreationTime");

        builder.HasIndex(x => x.DurationMs)
            .HasDatabaseName("IX_CorrelationLogs_DurationMs");

        // Composite indexes for common queries
        builder.HasIndex(x => new { x.CorrelationId, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_CorrelationId_CreationTime");

        builder.HasIndex(x => new { x.OperationType, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_OperationType_CreationTime");

        builder.HasIndex(x => new { x.Severity, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_Severity_CreationTime");

        builder.HasIndex(x => new { x.IsSuccess, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_IsSuccess_CreationTime");

        builder.HasIndex(x => new { x.UserId, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_UserId_CreationTime");

        // Multi-tenant index
        builder.HasIndex(x => new { x.TenantId, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_TenantId_CreationTime");

        // URL index for API tracking
        builder.HasIndex(x => new { x.Url, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_Url_CreationTime");

        // Performance tracking index
        builder.HasIndex(x => new { x.DurationMs, x.CreationTime })
            .HasDatabaseName("IX_CorrelationLogs_DurationMs_CreationTime");
    }
} 