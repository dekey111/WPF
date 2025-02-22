using WpfApp2.Database;

namespace WpfApp2.Models
{
    public class TareResponse
    {
        public TareResponse() { }
        public long Id { get; set; }
        public long? IdCar { get; set; }
        public string Number { get; set; }
        public double GrossWeight { get; set; }
        public double TareWeight { get; set; }
        public double NetWeight { get; set; }
        public DateTime TareDate { get; set; }
        public DateTime DateGross { get; set; }

        public override string ToString()
        {
            return $"Тара № {Number} \nБрутто: {GrossWeight} \nТара: {TareWeight} \nНетто: {NetWeight} \nДата тары: {TareDate.Date.ToString("dd.MM.yyyy")} \nДата Брутто: {DateGross.Date.ToString("dd.MM.yyyy")} \n";
        }

        public TareResponse(Tare tare)
        {
            Id = tare.Id;
            IdCar = tare.IdCar;
            Number = tare.Number;
            GrossWeight = tare.GrossWeight;
            TareWeight = tare.TareWeight;
            NetWeight = tare.NetWeight;
            TareDate = tare.TareDate;
            DateGross = tare.DateGross;
        }
    }
}
