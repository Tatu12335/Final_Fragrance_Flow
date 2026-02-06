using Dapper;
using Fragrance_flow_DL_VERSION_.interfaces;
using Fragrance_flow_DL_VERSION_.models;
using Microsoft.Data.SqlClient;

namespace Fragrance_flow_DL_VERSION_.classes.logic.Suggestion_logic
{
    public class SuggestionLogic : ISuggestion
    {
        
        private readonly string _connectionString;
        private readonly ILoggger _loggger;
        public SuggestionLogic(string connectionString, ILoggger loggger)
        {
            _connectionString = connectionString;
            _loggger = loggger;
        }

        public async Task<IEnumerable<Fragrance>> ScentOfTheDay(double? temp, int id)
        {
            string sqlQuery = "SELECT TOP 1 * from tuoksut WHERE userId = @Id";
            if (temp >= 10 && temp <= 20)
            {
                sqlQuery = "SELECT TOP 1 * FROM tuoksut WHERE userId = @Id AND (category = 'floral' or category = 'fresh' or weather = 'mild' or weather = 'mild sunny') ORDER BY NEWID()";
                try
                {
                    using (var conn = new SqlConnection(_connectionString))
                    {
                        var fragrance = await conn.QueryAsync<Fragrance>(sqlQuery, new { Id = id });
                        if (fragrance != null) return fragrance;
                    }
                }
                catch (Exception ex)
                {
                    _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    throw new Exception(" An error occured : " + ex.Message);

                }
            }
            else if (temp > 20)
            {
                try
                {
                    sqlQuery = "SELECT TOP 1 * FROM tuoksut WHERE userId = @Id AND (category = 'fresh' or category = 'airy' or weather = 'warm' or weather = 'warm sunny') order by NEWID()";

                    using (var conn = new SqlConnection(_connectionString))
                    {
                        var fragrance = await conn.QueryAsync<Fragrance>(sqlQuery, new { Id = id });
                        if (fragrance != null) return fragrance;
                    }
                }
                catch (Exception ex)
                {
                    _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    throw new Exception(" An error occured : " + ex.Message);
                }
            }
            else
            {
                sqlQuery = "SELECT TOP 1 * FROM tuoksut WHERE userId = @Id AND ( category = 'gourmand' or category = 'amber' or weather = 'cold' or weather = 'cold sunny') ORDER BY NEWID();";

                try
                {
                    using (var conn = new SqlConnection(_connectionString))
                    {
                        var fragrance = await conn.QueryAsync<Fragrance>(sqlQuery, new { Id = id });
                        if (fragrance != null) return fragrance;

                    }
                }
                catch (Exception ex)
                {
                    _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                    throw new Exception(" An error occured : " + ex.Message);
                }
            }
            return null;
        }
        public async Task<Fragrance?> SuggestBasedOnFeeling(string feeling, int id)
        {
            string sqlQuery = "SELECT COUNT(1) FROM tuoksut";
            // A switch-case might have been a cleaner solution but it's okay for now.
            try
            {
                if (feeling == "energetic")
                {
                    sqlQuery = "SELECT TOP 1 * FROM tuoksut WHERE userId = @ID AND (category = 'fresh') ORDER BY NEWID()";

                    using (var conn = new SqlConnection(_connectionString))
                    {
                        var queryResult = await conn.QueryFirstOrDefaultAsync<Fragrance>(sqlQuery, new { ID = id });
                        if (queryResult == null) return null;
                        return queryResult;
                    }
                }
                else if (feeling == "calm")
                {
                    sqlQuery = "SELECT TOP 1 * FROM tuoksut WHERE userId = @ID AND (category = 'floral' OR notes = 'flowers' or notes = 'floral') ORDER BY NEWID()";

                    using (var conn = new SqlConnection(_connectionString))
                    {
                        var queryResult = await conn.QueryFirstOrDefaultAsync<Fragrance>(sqlQuery, new { ID = id });
                        if (queryResult == null) return null;
                        return queryResult;
                    }
                }
                else if (feeling == "powerful")
                {
                    sqlQuery = "SELECT TOP 1 * FROM tuoksut where userId = @ID AND (category = 'woody' OR notes = 'woods' OR notes = 'woody' OR notes = 'oud') ORDER BY NEWID()";

                    using (var conn = new SqlConnection(_connectionString))
                    {
                        var queryResult = await conn.QueryFirstOrDefaultAsync<Fragrance>(sqlQuery, new { ID = id });
                        if (queryResult == null) return null;
                        return queryResult;
                    }
                }
                else
                {
                    throw new Exception(" 'FEELING' is Incorrect format, try again");
                }
            }
            catch (Exception ex)
            {
                _loggger.Log($" [error] : {ex.Message}. {ex.StackTrace}");
                throw new Exception("An error occured : " + ex.Message);
            }


        }
        public async Task<string> FragranceForWeather(double temp)
        {
            return $" Temp : {temp}";
        }
    }
}
