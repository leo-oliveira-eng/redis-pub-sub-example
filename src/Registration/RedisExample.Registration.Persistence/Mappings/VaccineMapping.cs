using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Persistence.Mappings
{
    internal class VaccineMapping : IEntityTypeConfiguration<Vaccine>
    {
        public void Configure(EntityTypeBuilder<Vaccine> builder)
        {
            builder.ToTable(nameof(Vaccine));

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Pet).WithMany(x => x.Vaccines).HasForeignKey(x => x.PetId);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
