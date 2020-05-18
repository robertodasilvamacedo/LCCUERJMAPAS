using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LCCUERJMapas.Models
{
    public class ResultadoBuscaEndereco
    {
        [JsonProperty("place_id")]
        public string PlaceId {get;set;}

        [JsonProperty("licence")]
        public string License { get; set; }

        [JsonProperty("osm_type")]
        public string OsmType { get; set; }

        [JsonProperty("osm_id")]
        public string OsmId { get; set; }

        [JsonProperty("boundingbox")]
        public List<string> BoundingBox { get; set; }        

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longitude { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("class")]
        public string Classe { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("importance")]
        public string Importance { get; set; }
        public string Cep { get; set; }
        public string UrlMapa { get; set; }
    }
}
