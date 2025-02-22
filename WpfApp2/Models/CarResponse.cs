using WpfApp2.Database;

namespace WpfApp2.Models
{
    public class CarResponse
    {
        public CarResponse() { }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public string FullNameCar
        {
            get => $"[{Number}] | {Name}";
            set => Name = value;
        }

        public CarResponse(Car car)
        {
            Id = car.Id;
            Name = car.Name;
            Number = car.Number;
        }
    }
}
