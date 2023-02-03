using APIAutos.Model;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutos.Data.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly MySqlConfiguration _connectionString;

        public CarRepository(MySqlConfiguration connectionString)
        {
            _connectionString= connectionString;
        }

        //Instancio clase de la dependencia MySql.Data
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }
        
        async Task<bool> ICarRepository.DeleteCar(int id)
        {
            var db = dbConnection();

            var sql = @"DELETE FROM cars WHERE id = @Id";

            var result = await db.ExecuteAsync(sql, new { Id = id });

            return result > 0;
        }

        async Task<IEnumerable<Car>> ICarRepository.GetAllCars()
        {
            //Establecemos coneccion
            var db = dbConnection();
            //Query
            var sql = @"SELECT id, make, model, color, year, doors FROM cars";

            //Utilizamos metodo de la dependencia dapper
            return  await db.QueryAsync<Car>(sql, new { });
        }

        async Task<Car> ICarRepository.GetDetails(int id)
        {
            //Establecemos coneccion
            var db = dbConnection();
            //Query
            var sql = @"SELECT id, make, model, color, year, doors FROM cars WHERE id = @Id";

            //Utilizamos metodo de la dependencia dapper
            return await db.QueryFirstOrDefaultAsync<Car>(sql, new {Id = id });
        }

        async Task<bool> ICarRepository.InstertCar(Car car)
        {
            //Establecemos coneccion
            var db = dbConnection();
            //Query
            var sql = @"INSERT INTO cars (make, model, color, year, doors) VALUES(@Make, @Model, @Color, @Year, @Doors)";

            //Utilizamos metodo de la dependencia dapper
            var result = await db.ExecuteAsync(sql, new { car.Make, car.Model, car.Color, car.Year, car.Doors });

            return result > 0;
        }

        async Task<bool> ICarRepository.UpdateCar(Car car)
        {
            //Establecemos coneccion
            var db = dbConnection();
            //Query
            var sql = @"UPDATE cars SET make = @Make, model = @Model, color = @Color, year = @Year, doors = @Doors WHERE id = @Id";

            //Utilizamos metodo de la dependencia dapper
            var result = await db.ExecuteAsync(sql, new { car.Make, car.Model, car.Color, car.Year, car.Doors, car.Id});

            return result > 0;
        }
    }
}
