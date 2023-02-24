using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class DataAccess
    {

        readonly ConnectionStringSettings dbstring = ConfigurationManager.ConnectionStrings["dbReserveDiscount"];
        public string StoredProcedure(DynamicParameters parameters, String Spname)
        {
            string result;
            using (IDbConnection db = new SqlConnection(dbstring.ConnectionString))
            {
                var StoredProcedure = db.Query<dynamic>(Spname, parameters,
                                commandType: CommandType.StoredProcedure).ToList();

                var json = JsonConvert.SerializeObject(StoredProcedure, Formatting.Indented);
                result = json;
            }

            return result;
        }

    }
}