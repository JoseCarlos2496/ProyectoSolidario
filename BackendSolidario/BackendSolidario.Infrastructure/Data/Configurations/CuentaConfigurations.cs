using BackendSolidario.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendSolidario.Infrastructure.Data.Configurations {
    public class CuentaConfigurations : IEntityTypeConfiguration<Cuenta> {

        void IEntityTypeConfiguration<Cuenta>.Configure(EntityTypeBuilder<Cuenta> builder) {
            builder.ToTable("cuenta");

            builder.HasKey(x => x.NumeroCuenta);

            builder.Property(x => x.NumeroCuenta)
                   .IsRequired()
                   .HasColumnName("cue_numero_cuenta")
                   .HasColumnType("varchar")
                   .HasMaxLength(16);

            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasColumnName("cue_id")
                   .HasColumnType("int")
                   .UseIdentityColumn(1);

            builder.Property(x => x.TipoCuenta)
                   .IsRequired()
                   .HasColumnName("cue_tipo_cuenta")
                   .HasColumnType("varchar")
                   .HasMaxLength(16);

            builder.Property(x => x.SaldoInicial)
                   .IsRequired()
                   .HasColumnName("cue_saldo_inicial")
                   .HasColumnType("decimal(15,3)");

            builder.Property(x => x.ClienteId)
                   .IsRequired()
                   .HasColumnName("cue_clienteId")
                   .HasColumnType("int");

            builder.Property(x => x.Estado)
                   .IsRequired()
                   .HasColumnName("cue_estado")
                   .HasColumnType("bit")
                   .HasDefaultValue(1);
        }
    }
}
