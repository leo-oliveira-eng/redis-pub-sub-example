using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Persistence.Mappings
{
    internal class PetMapping : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable(nameof(Pet));

            builder.HasKey(x => x.Id);

            builder.Ignore( x => x.DomainEvents);

            builder.HasOne(x => x.Human).WithMany(x => x.Pets).HasForeignKey(x => x.HumanId);
        }
    }
}
