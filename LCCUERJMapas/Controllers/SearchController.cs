using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LCCUERJMapas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace LCCUERJMapas.Controllers
{   
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IBoSearchNominatimLCCUERJ _boBuscar;

        public SearchController(ILogger<SearchController> logger, IBoSearchNominatimLCCUERJ boBuscar)
        {
            _logger = logger;
            _boBuscar = boBuscar;
        }

        [HttpGet]
        [Route("api/Buscar/")]
        public IActionResult Search(string street, string city, string county, string state, string country, string postalcode, string format="json")
        {
            try
            {
                CepContext context = HttpContext.RequestServices.GetService(typeof(LCCUERJMapas.Models.CepContext)) as CepContext;
                List<ResultadoBuscaEndereco> objRetorno = _boBuscar.ObterInformacaoLocalizacao(context, street, city, county, state, country, postalcode, format);
                return StatusCode(200, Newtonsoft.Json.JsonConvert.SerializeObject(objRetorno));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
