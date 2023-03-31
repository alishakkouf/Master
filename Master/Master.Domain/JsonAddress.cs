using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Master.Domain
{
    public class JsonAddress
    {
        public string CountryName { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Street { get; set; }

        public string Building { get; set; }

        public static JsonAddress Deserialize(string textAddress)
        {
            if (string.IsNullOrEmpty(textAddress) || textAddress.ToUpper().Equals("NULL"))
                return null;

            return JsonConvert.DeserializeObject<JsonAddress>(textAddress);
        }

        public static string Serialize(JsonAddress jsonAddress)
        {
            return JsonConvert.SerializeObject(jsonAddress);
        }

        [Newtonsoft.Json.JsonIgnore]
        public string Normalized
        {
            get
            {
                var result = "";
                if (!string.IsNullOrEmpty(Street)) result += Street;
                if (!string.IsNullOrEmpty(Building)) result += " " + Building;
                if (!string.IsNullOrEmpty(result)) result += ", ";
                if (!string.IsNullOrEmpty(PostalCode)) result += PostalCode + " ";
                if (!string.IsNullOrEmpty(City)) result += City + ", ";
                if (!string.IsNullOrEmpty(CountryName)) result += CountryName;

                return result;
            }
        }
    }
}
