using System.Data.Entity;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.Database;

namespace WpfApp2.Repository
{
    public class TareRepository : IRepositoryToFind<TareResponse>
    {
        private readonly BdtestTaskServerstalContext _db;
        public TareRepository()
        {
            _db = new BdtestTaskServerstalContext();
        }

        /// <summary>
        /// Метод полоучения всех тар
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TareResponse>> GetAll()
        {
            List<TareResponse> tareResponses = new List<TareResponse>();
            _db.Tares.ToList().ForEach(x => tareResponses.Add(new TareResponse(x)));
            return tareResponses;
        }

        /// <summary>
        /// Метод получения всех тар для конкретного транспорта
        /// </summary>
        /// <param name="id">Принимает idCar</param>
        /// <returns>Возвращает список всех тар</returns>
        public async Task<IEnumerable<TareResponse>> GetFromId(long id)
        {
            List<TareResponse> tareResponses = new List<TareResponse>();
            _db.Tares.Where(x => x.IdCar == id).ToList().ForEach(x => tareResponses.Add(new TareResponse(x)));
            return tareResponses;
        }

        /// <summary>
        /// Метод создания новой тары
        /// </summary>
        /// <param name="item">Принимает параметры тары</param>
        /// <returns>Возвращает новый объект тары</returns>
        /// <exception cref="Exception"></exception>
        public async Task<TareResponse> Create(TareResponse item)
        {
            if (_db.Tares.Any(x => x.Number == item.Number))
                throw new Exception("Такая тара уже есть!");

            Tare tare = new Tare()
            {
                Number = item.Number,
                IdCar = item.IdCar,
                GrossWeight = item.GrossWeight,
                DateGross = item.DateGross,
                NetWeight = item.NetWeight,
                TareDate = item.TareDate,
                TareWeight = item.TareWeight
            };
            _db.Tares.Add(tare);
            await Save();
            return new TareResponse(tare);
        }


        /// <summary>
        /// Метод обновления тары
        /// </summary>
        /// <param name="item">Принимает параметры тары</param>
        /// <returns>Возвращает новый объект тары</returns>
        /// <exception cref="Exception"></exception>
        public async Task<TareResponse> Update(TareResponse item)
        {
            var findTare = _db.Tares.FirstOrDefault(x => x.Id == item.Id);
            if (findTare == null)
                throw new Exception("Тара не найдена!");

            findTare.IdCar = item.IdCar;
            findTare.Number = item.Number;
            findTare.GrossWeight= item.GrossWeight;
            findTare.DateGross= item.DateGross;
            findTare.NetWeight = item.NetWeight;
            findTare.TareDate = item.TareDate;
            findTare.TareWeight = item.TareWeight;

            _db.Tares.Update(findTare);
            await Save();
            return new TareResponse(findTare);
        }

        /// <summary>
        /// Метод удаления тары
        /// </summary>
        /// <param name="id">Принимает id тары</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Delete(long id)
        {
            var findTare = _db.Tares.FirstOrDefault(x => x.Id == id);
            if (findTare == null)
                throw new Exception("Тара не найдена!");

            _db.Tares.Remove(findTare);
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
