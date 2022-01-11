using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisExample.Registration.Domain.Models;
using Valuables.Utils;

namespace RedisExample.Registration.Persistence.Mappings
{
    internal class HumanMapping : IEntityTypeConfiguration<Human>
    {
        public void Configure(EntityTypeBuilder<Human> builder)
        {
            builder.ToTable(nameof(Human));

            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.DomainEvents);

            builder.OwnsOne(p => p.Email, email =>
            {
                email.Property(p => p.Address).HasColumnName(nameof(Email));
            });

            builder.OwnsOne(p => p.CPF, cpf =>
            {
                cpf.Property(p => p.Text).HasColumnName(nameof(CPF));
            });

            builder.OwnsOne(p => p.Address, address =>
            {
                address.Property(p => p.Cep).HasColumnName(nameof(Address.Cep)).HasMaxLength(10);
                address.Property(p => p.City).HasColumnName(nameof(Address.City)).HasMaxLength(256);
                address.Property(p => p.Complement).HasColumnName(nameof(Address.Complement)).HasMaxLength(256);
                address.Property(p => p.Neighborhood).HasColumnName(nameof(Address.Neighborhood)).HasMaxLength(256);
                address.Property(p => p.Street).HasColumnName(nameof(Address.Street)).HasMaxLength(256);
                address.Property(p => p.UF).HasColumnName(nameof(Address.UF)).HasMaxLength(2);
                address.Property(p => p.Number).HasColumnName(nameof(Address.Number)).HasMaxLength(20);
            });
        }
    }
}
