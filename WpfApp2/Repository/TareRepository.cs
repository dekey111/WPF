using System.Data.Entity;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.Database;

namespace WpfApp2.Repository
{
    public class TareRepository : IRepository<TareResponse>
    {
        private readonly BdtestTaskServerstalContext _db;
        public TareRepository()
        {
            _db = new BdtestTaskServerstalContext();
        }

        public async Task<IEnumerable<TareResponse>> GetAll()
        {
            List<TareResponse> tareResponses = new List<TareResponse>();
            _db.Tares.ToList().ForEach(x => tareResponses.Add(new TareResponse(x)));
            return tareResponses;
        }

        public async Task<TareResponse> Create(TareResponse item)
        {
            var findTare = await _db.Tares.FirstOrDefaultAsync(x => x.Number == item.Number);
            if (findTare != null)
                throw new Exception("Такая тара уже есть!");

            Tare tare = new Tare()
            {
                Number = item.Number,
                GrossWeight = item.GrossWeight,
                DateGross = item.DateGross,
                NetWeight = item.NetWeight,
                TareDate = item.TareDate,
                TareWeight = item.TareWeight
            };
            _db.Tares.Add(tare);

            return new TareResponse(tare);
        }

        public async Task<TareResponse> Update(TareResponse item)
        {
            var findTare = await _db.Tares.FirstOrDefaultAsync(x => x.Id == item.Id);
            if (findTare == null)
                throw new Exception("Тара не найдена!");

            findTare.Number = item.Number;
            findTare.GrossWeight= item.GrossWeight;
            findTare.DateGross= item.DateGross;
            findTare.NetWeight = item.NetWeight;
            findTare.TareDate = item.TareDate;
            findTare.TareWeight = item.TareWeight;

            _db.Tares.Update(findTare);
            return new TareResponse(findTare);
        }

        public async Task Delete(long id)
        {
            var findTare = await _db.Tares.FirstOrDefaultAsync(x => x.Id == id);
            if (findTare == null)
                throw new Exception("Тара не найдена!");

            _db.Tares.Remove(findTare);
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
