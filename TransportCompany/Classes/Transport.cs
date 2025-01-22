using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportCompany.Classes
{
    public class Transport
    {
        public int TransportId { get; private set; }
        public int TrailerId { get; private set; }
        public string TransportNum { get; private set; }
        public string TransportBrand { get; private set; }
        public string TransportModel { get; private set; }
    }
}
