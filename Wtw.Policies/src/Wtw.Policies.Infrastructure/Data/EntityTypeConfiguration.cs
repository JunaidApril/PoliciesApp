using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wtw.Policies.Domain.Models;

namespace Wtw.Policies.Infrastructure.Data
{
    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .Property(entity => entity.CreatedBy_UUID)
                .IsRequired();

            builder
                .Property(entity => entity.CreatedAt)
                .IsRequired();

            builder
                .Property(entity => entity.ModifiedBy_UUID)
                .IsRequired();

            builder
                .Property(entity => entity.ModifiedAt)
                .IsRequired();

            builder.HasKey(entity => entity.UUID);
            builder.Property(entity => entity.UUID)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("newsequentialid()");

            builder.HasQueryFilter(entity => entity.Active);
        }
    }
}
