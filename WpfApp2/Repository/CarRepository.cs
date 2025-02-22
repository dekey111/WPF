using System.Data.Entity;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.Database;

namespace WpfApp2.Repository
{
    internal class CarRepository : IRepository<CarResponse>
    {
        private readonly BdtestTaskServerstalContext _db;

        public CarRepository()
        {
            _db = new BdtestTaskServerstalContext();
        }
        public async Task<IEnumerable<CarResponse>> GetAll()
        {
            List<CarResponse> carResponses = new List<CarResponse>();
            _db.Cars.ToList().ForEach(x => carResponses.Add(new CarResponse(x)));
            return carResponses;
        }

        public async Task<CarResponse> Create(CarResponse item)
        {
            if (_db.Cars.Any(x => x.Number == item.Number))
                throw new Exception("Транспорт с таким номером уже есть!");

            Car car = new Car()
            {
                Name = item.Name,
                Number = item.Number,
            };
            _db.Cars.Add(car);
            return new CarResponse(car);
        }

        public async Task<CarResponse> Update(CarResponse item)
        {
            var findCar = _db.Cars.FirstOrDefault(x => x.Id == item.Id);
            if (findCar == null)
                throw new Exception("Транспорт не найден!");

            findCar.Name = item.Name;
            findCar.Number = item.Number;

            _db.Cars.Update(findCar);
            return new CarResponse(findCar);
        }

        public async Task Delete(long id)
        {
            var findCar = _db.Cars.FirstOrDefault(x => x.Id == id);
            if (findCar == null)
                throw new Exception("Транспорт не найден!");


            await Save();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            _db.SaveChanges();
        }
    }
}
