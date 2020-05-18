using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCCUERJMapas.Models
{
    public interface IBoSearchNominatimLCCUERJ
    {
        public List<ResultadoBuscaEndereco> ObterInformacaoLocalizacao(CepContext context, string street=null, string city = null, string county = null, string state = null, string country = null, string postalcode = null, string format = "json");
    }
}
