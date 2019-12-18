using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Domain.DataContracts;
using Weather.Domain.Models;
using Weather.Domain.Repositories;

namespace Weather.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<List<City>> GetAll()
        {
            var sql = new string[2] {
                "select * from cities;",
                "select * from temperatures where city_id = {0};"
            };

            using var connection = CreateConnection();
            var cities = await connection.QueryAsync<City>(sql[0]);

            foreach (var city in cities)
            {
                var temperatures = await connection.QueryAsync<Temperature>(String.Format(sql[1], city.Id.ToString()));

                city.Temperatures = new List<Temperature>();
                city.Temperatures.AddRange(temperatures);
            }

            return cities.ToList();
        }

        public async Task<City> GetByName(RequestCity cityModel)
        {
            var sql = new string[2] {
                "select * from cities where name = '{0}' and state = '{1}';",
                "select t.id, t.degrees, t.created from cities c inner join temperatures t on c.id = t.city_id where c.name = '{0}' and c.state = '{1}';"
            };

            using var connection = CreateConnection();

            var city = await connection.QueryFirstOrDefaultAsync<City>(String.Format(sql[0], cityModel.Name, cityModel.State));

            if (city != null)
            {
                var result = await connection.QueryAsync<Temperature>(String.Format(sql[1], cityModel.Name, cityModel.State));
                city.Temperatures = result.ToList();
            }

            return city;
        }

        public async Task<City> AddCity(RequestCity cityModel)
        {
            var sql = new string[2] {
                "select * from cities where name = '{0}' and state = '{1}';",
                "insert into cities(name, state) values('{0}', '{1}');"
            };

            using var connection = CreateConnection();

            var city = await connection.QueryAsync<City>(String.Format(sql[0], cityModel.Name, cityModel.State));

            if (!city.Any())
            {
                var result = await connection.ExecuteAsync(String.Format(sql[1], cityModel.Name, cityModel.State));

                if (result == 1)
                {
                    city = await connection.QueryAsync<City>(String.Format(sql[0], cityModel.Name, cityModel.State));
                    return city.FirstOrDefault();
                }
            }

            return null;
        }

        public async Task<bool> DeleteCity(RequestCity cityModel)
        {
            var sql = new string[3] {
                "select * from cities where name = '{0}' and state = '{1}';",
                "delete temperatures where city_id = {0};",
                "delete cities where name = '{0}' and state = '{1}';"
            };

            using var connection = CreateConnection();

            var city = await connection.QueryAsync<City>(String.Format(sql[0], cityModel.Name, cityModel.State));

            if (city.Any())
            {
                var result = await connection.ExecuteAsync(String.Format(sql[1], city.First().Id));
                result = await connection.ExecuteAsync(String.Format(sql[2], cityModel.Name, cityModel.State));

                return result == 1;
            }

            return false;
        }

        public async Task<bool> DeleteTemperatures(RequestCity cityModel)
        {
            var sql = new string[2] {
                "select * from cities where name = '{0}' and state = '{1}';",
                "delete temperatures where city_id = {0};"
            };

            using var connection = CreateConnection();

            var city = await connection.QueryAsync<City>(String.Format(sql[0], cityModel.Name, cityModel.State));

            if (city.Any())
            {
                var result = await connection.ExecuteAsync(String.Format(sql[1], city.First().Id));
                return result > 0;
            }

            return false;
        }

        public async Task<City> AddTemperature(Temperature temperature)
        {
            var sql = new string[2] {
                "insert into temperatures(degrees, created, city_id) values({0}, '{1}', {2});",
                "select * from cities where id = {0};"
            };

            using var connection = CreateConnection();

            var result = await connection.ExecuteAsync(String.Format(sql[0], temperature.Degrees, temperature.Created.ToString(), temperature.CityId.ToString()));

            if (result > 0)
            {
                var city = await connection.QueryAsync<City>(String.Format(sql[1], temperature.CityId));

                if (city.Any())
                    return city.FirstOrDefault();
            }

            return null;
        }
    }
}