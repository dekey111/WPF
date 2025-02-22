using System.Data.Entity;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.Database;

namespace WpfApp2.Repository
{
    internal class ViewCarTareRepository : IRepository<ViewCarTareResponse>
    {

        private readonly BdtestTaskServerstalContext _db;
        public ViewCarTareRepository()
        {
            _db = new BdtestTaskServerstalContext();
        }

        public async Task<IEnumerable<ViewCarTareResponse>> GetAll()
        {
            List<ViewCarTareResponse> ViewCarTareResponse = new List<ViewCarTareResponse>();
            _db.ViewCarTare.ToList().ForEach(x => ViewCarTareResponse.Add(new ViewCarTareResponse(x)));
            return ViewCarTareResponse;
        }

        public async Task<ViewCarTareResponse> Create(ViewCarTareResponse item)
        {
            var findView = await _db.CarTares.FirstOrDefaultAsync(x => x.IdCar == item.IdCar && x.IdTare == item.IdTare);
            if (findView != null)
                throw new Exception("Такая связь уже есть!");

            CarTare carTare = new CarTare()
            {
                IdCar = item.IdCar,
                IdTare = item.IdTare,
            };
            _db.CarTares.Add(carTare);
            await _db.SaveChangesAsync();
            var findCar = await _db.Cars.FirstOrDefaultAsync(x => x.Id == item.IdCar);
            var findTare = await _db.Tares.FirstOrDefaultAsync(x => x.Id == item.IdTare);


            return new ViewCarTareResponse(findCar, findTare);
        }
        public async Task<ViewCarTareResponse> Update(ViewCarTareResponse item)
        {
            var findCareTares = await _db.CarTares.FirstOrDefaultAsync(x => x.IdCar == item.IdCar && x.IdTare == item.IdTare);
            if (findCareTares == null)
                throw new Exception("Связь не найдена!");

            findCareTares.IdCar = item.IdCar;
            findCareTares.IdTare = item.IdTare;

            _db.CarTares.Update(findCareTares);

            var findCar = await _db.Cars.FirstOrDefaultAsync(x => x.Id == item.IdCar);
            var findTare = await _db.Tares.FirstOrDefaultAsync(x => x.Id == item.IdTare);


            return new ViewCarTareResponse(findCar, findTare);
        }

        public async Task Delete(long id)
        {
            var findCareTares = await _db.CarTares.FirstOrDefaultAsync(x => x.Id == id);
            if (findCareTares == null)
                throw new Exception("Связь не найдена!");

            _db.CarTares.Remove(findCareTares);
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
            await _db.SaveChangesAsync();
        }
    }
}
