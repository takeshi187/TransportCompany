using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportCompany.Classes
{
    public class Trailer
    {
        public int TrailerId { get; private set; }
        public int TrailerTypeId { get; private set; }
        public string TrailerNum { get; private set; }
        public string TrailerBrand { get; private set; }
        public string TrailerModel { get; private set; }
        public int TrailerCarrying { get; private set; }
    }
}
