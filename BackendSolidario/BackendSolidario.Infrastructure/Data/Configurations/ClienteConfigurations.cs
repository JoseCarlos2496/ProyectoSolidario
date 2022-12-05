using BackendSolidario.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendSolidario.Infrastructure.Data.Configurations {
    public class ClienteConfigurations : IEntityTypeConfiguration<Cliente> {

        void IEntityTypeConfiguration<Cliente>.Configure(EntityTypeBuilder<Cliente> builder) {

            builder.ToTable("cliente");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasColumnName("cli_id")
                   .HasColumnType("int");

            builder.Property(x => x.Identificacion)
                   .IsRequired()
                   .HasColumnName("identificacion")
                   .HasColumnType("varchar")
                   .HasMaxLength(16);

            builder.Property(x => x.Nombre)
                   .IsRequired()
                   .HasColumnName("nombre")
                   .HasColumnType("varchar")
                   .HasMaxLength(64);

            builder.Property(x => x.Usuario)
                   .IsRequired()
                   .HasColumnName("cli_usuario")
                   .HasColumnType("varchar")
                   .HasMaxLength(16);

            builder.Property(x => x.Contrasena)
                   .IsRequired()
                   .HasColumnName("cli_contrasena")
                   .HasColumnType("varchar")
                   .HasMaxLength(500);

            builder.Property(x => x.Salt)
                   .IsRequired(false)
                   .HasColumnName("cli_salt")
                   .HasColumnType("varchar")
                   .HasMaxLength(500);


            builder.Property(x => x.Estado)
                   .IsRequired()
                   .HasColumnName("cli_estado")
                   .HasColumnType("bit")
                   .HasDefaultValue(1);
        }
    }
}
