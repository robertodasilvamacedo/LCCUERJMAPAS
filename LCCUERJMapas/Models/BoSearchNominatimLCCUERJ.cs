using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCCUERJMapas.Models
{
    public class BoSearchNominatimLCCUERJ : IBoSearchNominatimLCCUERJ
    {
        public List<ResultadoBuscaEndereco> ObterInformacaoLocalizacao(CepContext context, string street, string city, string county, string state, string country, string postalcode, string format)
        {
            try
            {
                StringBuilder builderParametros = new StringBuilder();
                string endTemp = string.Empty;
                Cep objCep = null;

                if (postalcode != null && !postalcode.Equals(""))
                {
                    objCep = context.ObterEnderecoCompletoCep(postalcode);
                    if(objCep != null)
                    {
                        country = "brasil";
                        street = objCep.Endereco;
                        county = objCep.Bairro;
                    }
                }
                if (street != null && !street.Equals(""))
                {
                    string[] temp = street.Split(" ");

                    for (int i = 0; i < temp.Length; i++)
                    {
                        int indice = temp[i].IndexOf('/');
                        //Retirando o que tiver depois do /
                        if(indice != -1)
                        {
                            temp[i] = temp[i].Substring(0, indice);
                            endTemp += temp[i] + " ";
                        }
                        else
                        {
                            endTemp += temp[i] + " ";
                        }
                    }

                    if(temp.Length >= 6)
                    {
                        endTemp = temp[0] + " " + temp[1] + " " + temp[2] + " " + temp[3] + " " + temp[4] + " " + temp[5];
                    }
                    builderParametros.Append($"street={endTemp}&");
                }
                if (city != null && !city.Equals(""))
                {
                    builderParametros.Append($"city={city}&");
                }
                if (county != null && !county.Equals(""))
                {
                    builderParametros.Append($"county={county}&");
                }
                if (state != null && !state.Equals(""))
                {
                    builderParametros.Append($"state={state}&");
                }
                if (country != null && !country.Equals(""))
                {
                    builderParametros.Append($"country={country}&");
                }
                if (format != null && !format.Equals(""))
                {
                    builderParametros.Append($"format={format}&");
                }

                var client = new RestClient("http://maps.lcc.ime.uerj.br/");
                string stringRequest = $@"search.php?{builderParametros.ToString()}";
                var request = new RestRequest(stringRequest, Method.GET);
                IRestResponse response = client.Execute(request);

                if(!response.IsSuccessful)
                {
                    throw new Exception(response.Content);
                }

                List<ResultadoBuscaEndereco> lstResultadoConsultaNominatim = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultadoBuscaEndereco>>(response.Content);

                for (int i = 0; i < lstResultadoConsultaNominatim.Count; i++)
                {
                    lstResultadoConsultaNominatim[i].Cep = postalcode;
                    lstResultadoConsultaNominatim[i].UrlMapa = "http://maps.lcc.ime.uerj.br/search?street="+endTemp+"&county="+county+"&country="+country;

                    string[] displayNameArrayTemp = lstResultadoConsultaNominatim[i].DisplayName.Split(",");

                    lstResultadoConsultaNominatim[i].Rua = displayNameArrayTemp[0];
                    lstResultadoConsultaNominatim[i].Bairro = displayNameArrayTemp[1];
                    lstResultadoConsultaNominatim[i].DescricaoLocalidade = displayNameArrayTemp[2];
                    lstResultadoConsultaNominatim[i].Cidade = displayNameArrayTemp[3];
                    lstResultadoConsultaNominatim[i].Estado = displayNameArrayTemp[7];
                    lstResultadoConsultaNominatim[i].RegiaoPais = displayNameArrayTemp[8];
                    lstResultadoConsultaNominatim[i].Pais = "Brasil";
                }

                if(lstResultadoConsultaNominatim.Count == 0 && objCep != null)
                {
                    lstResultadoConsultaNominatim.Add(new ResultadoBuscaEndereco() { Cep=objCep.DescricaoCep,DisplayName = objCep.Endereco + "/" + objCep.Bairro + "/" + objCep.EstadoMunicipio });
                }

                return lstResultadoConsultaNominatim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
