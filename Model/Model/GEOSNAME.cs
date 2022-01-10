using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Model
{
    public class GEOSNAME
    {
        public IList<Country> geonames { get; set; }
    }
    public class Country
    {
        public string countryName { get; set; }
        public string capital { get; set; }
        public string languages { get; set; }
        public string continentName { get; set; }
        public string areaInSqKm { get; set; }
        public string currencyCode { get; set; }
        public string population { get; set; }
    }
}
