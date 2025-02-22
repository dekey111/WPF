using WpfApp2.Database;

namespace WpfApp2.Models
{
    public class ViewCarTareResponse
    {
        public ViewCarTareResponse() { }

        public long IdCar { get; set; }
        public string Name { get; set; }
        public string CarNumbers { get; set; }
        public long IdTare { get; set; }
        public string TareNumbers { get; set; }
        public double GrossWeight { get; set; }
        public double TareWeight { get; set; }
        public double NetWeight { get; set; }
        public string TareDate { get; set; }
        public string DateGross { get; set; }

        public string FullNameCar
        {
            get => $"[{CarNumbers}] | {Name}";
            set => Name = value;
        }
        public ViewCarTareResponse(ViewCarTare viewCarTare)
        {
            IdCar = viewCarTare.Id;
            Name = viewCarTare.Name;
            CarNumbers = viewCarTare.Number;

            IdTare = viewCarTare.Id;
            TareNumbers = viewCarTare.Number;
            GrossWeight = viewCarTare.GrossWeight;
            TareWeight = viewCarTare.TareWeight;
            NetWeight = viewCarTare.NetWeight;
            TareDate = viewCarTare.TareDate.Date.ToString("dd.MM.yyyy");
            DateGross = viewCarTare.DateGross.Date.ToString("dd.MM.yyyy");
        }

        public ViewCarTareResponse(Car car, Tare tare)
        {
            IdCar = car.Id;
            Name = car.Name;
            CarNumbers = car.Number;

            IdTare = tare.Id;
            TareNumbers = tare.Number;
            GrossWeight = tare.GrossWeight;
            TareWeight = tare.TareWeight;
            NetWeight = tare.NetWeight;
            TareDate = tare.TareDate.Date.ToString("dd.MM.yyyy");
            DateGross = tare.DateGross.Date.ToString("dd.MM.yyyy");
        }
    }
}
