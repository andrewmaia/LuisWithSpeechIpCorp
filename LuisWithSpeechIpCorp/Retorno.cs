using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LuisWithSpeechIpCorp
{
    public class Retorno
    {
        public Retorno(string json)
        {
            JObject jObject = JObject.Parse(json);
            Query = (string)jObject["query"];
            Entidades = ((JArray)jObject["entities"]).ToObject<IList<Entidade>>();
        }

        public string Query { get; set; }

        public IList<Entidade> Entidades { get; set; }
    }
}
