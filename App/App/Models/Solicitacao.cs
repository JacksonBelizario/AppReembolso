using System;
using static App.Web.Models.Enums;

namespace App.Models
{
    public class Solicitacao
    {
        public int Id { get; set; }
        public DateTime DataDaCompra { get; set; }
        public DateTime DataDaSolicitacao { get; set; }
        public double Valor { get; set; }
        public Categorias Categoria { get; set; }
        public string Descricao { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Anexo { get; set; }
        public Status Status { get; set; }

    }
}
