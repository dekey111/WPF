using WpfApp2.Database;

namespace WpfApp2.Models
{
    internal class CarTareResponse
    {
        public CarTareResponse() { }
        public long Id { get; set; }
        public long IdCar { get; set; }
        public long IdTare { get; set; }

        public CarTareResponse(CarTare carTare)
        {
            Id = carTare.Id;
            IdCar = carTare.IdCar;
            IdTare = carTare.IdTare;
        }
    }
}
