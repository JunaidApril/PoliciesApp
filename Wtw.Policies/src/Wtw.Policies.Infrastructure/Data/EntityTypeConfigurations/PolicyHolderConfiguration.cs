using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wtw.Policies.Domain.Models;

namespace Wtw.Policies.Infrastructure.Data.EntityTypeConfigurations
{
    public class PolicyHolderConfiguration : EntityTypeConfiguration<PolicyHolder>
    {
        public override void Configure(EntityTypeBuilder<PolicyHolder> builder)
        {
            base.Configure(builder);

            builder
              .Property(policyHolder => policyHolder.Name)
              .IsRequired();

            builder
              .Property(policyHolder => policyHolder.Age)
              .IsRequired();

            builder
              .Property(policyHolder => policyHolder.Gender)
              .IsRequired();
        }
    }
}
