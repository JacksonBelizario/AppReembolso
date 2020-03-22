﻿using System;

namespace App.Web.Models
{
    public class Solicitacao
    {
        public int Id { get; set; }
        public DateTime DataDaCompra { get; set; }
        public DateTime DataDaSolicitacao { get; set; }
        public double Valor { get; set; }
        public int Categoria { get; set; }
        public string Descricao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Anexo { get; set; }
        public int Status { get; set; }

    }
}
