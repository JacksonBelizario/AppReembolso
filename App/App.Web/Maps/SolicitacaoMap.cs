using App.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace App.Web.Maps
{
    public class SolicitacaoMap
    {
        public SolicitacaoMap(EntityTypeBuilder<Solicitacao> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("solicitacao");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.DataDaCompra).HasColumnName("data_da_compra");
            entityBuilder.Property(x => x.DataDaSolicitacao).HasColumnName("data_da_solicitacao");
            entityBuilder.Property(x => x.Valor).HasColumnName("valor");
            entityBuilder.Property(x => x.Categoria).HasColumnName("categoria");
            entityBuilder.Property(x => x.Descricao).HasColumnName("descricao");
            entityBuilder.Property(x => x.Latitude).HasColumnName("latitude");
            entityBuilder.Property(x => x.Longitude).HasColumnName("longitude");
            entityBuilder.Property(x => x.Anexo).HasColumnName("anexo");
            entityBuilder.Property(x => x.Status).HasColumnName("status");
        }
    }
}