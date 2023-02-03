using APIAutos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutos.Data.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetDetails(int id);
        Task<bool> InstertCar(Car car);
        Task<bool> UpdateCar(Car car);
        Task<bool> DeleteCar(int id);
    }
}
