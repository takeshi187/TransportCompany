namespace TransportCompany.Classes
{
    public class Transport
    {
        public long TransportId { get; set; }
        public string TransportNum { get; set; }
        public string TransportBrand { get; set; }
        public string TransportModel { get; set; }

        public int Carrying {  get; set; }
    }
}
