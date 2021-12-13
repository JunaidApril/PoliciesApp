using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wtw.Policies.Domain.Models;

namespace Wtw.Policies.Infrastructure.Data.EntityTypeConfigurations
{
    public class PolicyConfiguration : EntityTypeConfiguration<Policy>
    {
        public override void Configure(EntityTypeBuilder<Policy> builder)
        {
            base.Configure(builder);

            builder
              .Property(policy => policy.PolicyHolderUUID)
              .IsRequired();

            builder.HasOne(policy => policy.PolicyHolder)
                .WithMany(holder => holder.Policies)
                .HasForeignKey(policy => policy.PolicyHolderUUID);
        }
    }
}
