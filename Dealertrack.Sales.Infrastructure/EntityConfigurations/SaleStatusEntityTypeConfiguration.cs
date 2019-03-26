using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealertrack.Sales.Infrastructure.EntityConfigurations
{
    class SaleStatusEntityTypeConfiguration : IEntityTypeConfiguration<SaleStatus>
    {
        public void Configure(EntityTypeBuilder<SaleStatus> orderStatusConfiguration)
        {
            orderStatusConfiguration.ToTable("salestatus", SalesContext.DEFAULT_SCHEMA);

            orderStatusConfiguration.HasKey(o => o.Id);

            orderStatusConfiguration.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            orderStatusConfiguration.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
