using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bakery.Services.Application;
using Bakery.Services.Application.Models.CustomerAddress;
using Dapper;
using MySql.Data.MySqlClient;

namespace Bakery.Services.Domain.Address
{
    public class AddressRepository : IAddressRepository
    {
        private readonly string _connectionString;
        private readonly IDbConnectionResolver _connector;

        public AddressRepository(string connection, IDbConnectionResolver connector)
        {
            _connectionString = connection;
            _connector = connector;
        }

        public async Task<IEnumerable<NearestAddressDto>> GetNearest(NearestLocation request)
        {
            request.Today = DateTime.UtcNow;
            
            using (IDbConnection db = _connector.ResolveConnection(_connectionString))
            {
                var query = @"SELECT ca.CustomerId,
                                     ca.AddressId,
                                    ca.AddressName,
                                    ca.Latitude,
                                    ca.Longitude,
                                    ca.DateStart,
                                    ca.DateEnd, 
                                    (
                                    6371 *
                                    acos(cos(radians(@Latitude)) * 
                                    cos(radians(ca.Latitude)) * 
                                    cos(radians(ca.Longitude) - 
                                    radians(@Longitude)) + 
                                    sin(radians(@Latitude)) * 
                                    sin(radians(ca.Latitude)))
                                    ) AS distance 
                                    FROM CustomerAddress ca
                                    WHERE ca.DateEnd is null or ca.DateEnd > @Today
                                    HAVING distance < @Distance;";
                var result = await db.QueryAsync<NearestAddressDto>(query, request);

                return result;
            }
        }
    }
}