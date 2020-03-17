using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class Solicitacoes
    {
        public string Id { get; set; }
        public DateTime DataDaCompra { get; set; }
        public DateTime DataDaSolicitacao { get; set; }
        public double Valor { get; set; }
        public int Categoria { get; set; }
        public string Descricao { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Anexo { get; set; }

    }
}
