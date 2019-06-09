using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Meassurement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeassurementsController : ControllerBase
    {
        private string ConnectionString = "Server=tcp:mock32019.database.windows.net,1433;Initial Catalog=Meassurement;Persist Security Info=False;User ID=fisnik95;Password=Password1599;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        // GET: api/Meassurements
        [HttpGet]
        public IEnumerable<Meassurement> GetAllMeassurements()
        {
            const string selectString = "select * from dbo.Meassurement";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<Meassurement> meassurementList = new List<Meassurement>();
                        while (reader.Read())
                        {
                            Meassurement meassurement = ReadMeassurement(reader);
                            meassurementList.Add(meassurement);
                        }
                        return meassurementList;
                    }
                }
            }
        }

        private static Meassurement ReadMeassurement(IDataRecord reader)
        {
            int id = reader.GetInt32(0);
            decimal pressure = reader.GetDecimal(1);
            decimal humidity = reader.GetDecimal(2);
            decimal temperature = reader.GetDecimal(3);
            DateTime timestamp = reader.GetDateTime(4);
            Meassurement meassurement = new Meassurement
            {
                Id = id,
                Pressure = pressure,
                Humidity = humidity,
                Temperature = temperature,
                TimeStamp = timestamp
            };
            return meassurement;
        }

        // GET: api/Meassurements/5
        [Route("{id}")]
        public Meassurement GetMeassurementById(int id)
        {
            const string selectString = "select * from Meassurement where Id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (!reader.HasRows) { return null; }
                        reader.Read(); // advance cursor to first row
                        return ReadMeassurement(reader);
                    }
                }
            }
        }


        // POST: api/Meassurements
       
        [HttpPost]
        public int AddMeasurrement([FromBody] Meassurement value)
        {
            const string insertString = "insert into Measurrement (pressure, humidity , temperature , timestamp) values (@pressure, @humidity, @temperature, @timestamp)";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@pressure", value.Pressure);
                    insertCommand.Parameters.AddWithValue("@humidity", value.Humidity);
                    insertCommand.Parameters.AddWithValue("@temperature", value.Temperature);
                    insertCommand.Parameters.AddWithValue("@timestamp", value.TimeStamp);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // PUT: api/Meassurements/5
        [HttpPut("{id}")]
        public int UpdateMeassurement(int id, [FromBody] Meassurement value)
        {
            const string updateString =
                "update Measurrement set pressure=@pressure, humidity=@humidity, temperature=@temperature, timestamp=@timestamp where id=@id;";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand updateCommand = new SqlCommand(updateString, databaseConnection))
                {
                    updateCommand.Parameters.AddWithValue("@pressure", value.Pressure);
                    updateCommand.Parameters.AddWithValue("@humidity", value.Humidity);
                    updateCommand.Parameters.AddWithValue("@temperature", value.Temperature);
                    updateCommand.Parameters.AddWithValue("@timestamp", value.TimeStamp);
                    updateCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int DeleteMeassurement(int id)
        {
            const string deleteStatement = "delete from Meassurement where id=@id";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(deleteStatement, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@id", id);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }
    }
}
