using Dealertrack.Sales.Domain.AggregatesModel.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dealertrack.Sales.Infrastructure.EntityConfigurations
{
    class SaleEntityTypeConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> orderConfiguration)
        {
            orderConfiguration.ToTable("sales", SalesContext.DEFAULT_SCHEMA);

            orderConfiguration.HasKey(o => o.Id);

            orderConfiguration.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("saleseq", SalesContext.DEFAULT_SCHEMA);

            orderConfiguration.Property<int>("DealNumber").IsRequired();
            orderConfiguration.Property<string>("CustomerName").IsRequired();
            orderConfiguration.Property<string>("DealershipName").IsRequired();
            orderConfiguration.Property<string>("Vehicle").IsRequired();
            orderConfiguration.Property<decimal>("Price").IsRequired();
            orderConfiguration.Property<DateTime>("Date").IsRequired();
            orderConfiguration.Property<int>("SaleStatusId").IsRequired();

            orderConfiguration.HasOne(o => o.SaleStatus)
                .WithMany()
                .HasForeignKey("SaleStatusId");
        }
    }
}
