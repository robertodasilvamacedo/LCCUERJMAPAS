using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCCUERJMapas.Models
{
    public class Cep
    {
        public int Id { get; set; }
        public string DescricaoCep { get; set; }
        public string EstadoMunicipio { get; set; }
        public string Bairro { get; set; }
        public string Endereco { get; set; }
    }
}
