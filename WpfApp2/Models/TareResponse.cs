using WpfApp2.Database;

namespace WpfApp2.Models
{
    public class TareResponse
    {
        public TareResponse() { }
        public long Id { get; set; }
        public string Number { get; set; }
        public double GrossWeight { get; set; }
        public double TareWeight { get; set; }
        public double NetWeight { get; set; }
        public DateTime TareDate { get; set; }
        public DateTime DateGross { get; set; }

        public TareResponse(Tare tare)
        {
            Id = tare.Id;
            Number = tare.Number;
            GrossWeight = tare.GrossWeight;
            TareWeight = tare.TareWeight;
            NetWeight = tare.NetWeight;
            TareDate = tare.TareDate;
            DateGross = tare.DateGross;
        }
    }
}
