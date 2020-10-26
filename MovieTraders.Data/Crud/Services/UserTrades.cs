// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SqlPlus.net
//     For more information on SqlPlus.net visit http://www.SqlPlus.net
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieTraders.Data.Crud.Models;
namespace MovieTraders.Data.Crud
{
    public partial class Service
    {

        /// <summary>
        /// Builds the command object for UserTrades method.
        /// </summary>
        /// <param name="cnn">The connection that will execute the procedure.</param>
        /// <param name="input">UserTradesInput instance for loading parameter values.</param>
        /// <returns>SqlCommand ready for execution.</returns>
        private SqlCommand GetUserTradesCommand(SqlConnection cnn, IUserTradesInput input)
        {
            SqlCommand result = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[crud].[UserTrades]",
                Connection = cnn
            };

            result.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@UserId",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                Scale = 0,
                Precision = 10,
				Value = input.UserId
            });

            result.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@ReturnValue",
                Direction = ParameterDirection.ReturnValue,
                SqlDbType = SqlDbType.Int,
                Scale = 0,
                Precision = 10,
                Value = DBNull.Value
            });

            return result;
        }
        /// <summary>
        /// Reads out variables and return values from the command
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private void SetUserTradesCommandOutputs(SqlCommand cmd, UserTradesOutput output)
        {
            if(cmd.Parameters[1].Value != DBNull.Value)
            {
                output.ReturnValue = (UserTradesOutput.Returns)cmd.Parameters[1].Value;
            }
        }


        /// <summary>
        /// Reads values from the populated reader and assigns values to properties of returned object
        /// </summary>
        /// <param name="rdr">Active SqlDataReader instance</param>
        /// <returns>Instance of UserTradesResult.Trades</returns>
        private UserTradesResult.Trades GetUserTradesTradesResultFromReader(SqlDataReader rdr)
        {
            UserTradesResult.Trades result = new UserTradesResult.Trades();
            result.TradeId = rdr.GetInt32(0);
            result.TradeStatusId = rdr.GetByte(1);
            result.RequestUserId = rdr.GetInt32(2);
            result.RequestUserName = rdr.GetString(3);
            result.ResponseUserId = rdr.GetInt32(4);
            result.ResponseUserName = rdr.GetString(5);
            result.RequestDate = rdr.GetDateTime(6);
            result.RequestDuration = rdr.GetInt32(7);
            result.Notes = rdr.GetString(8);
            return result;
        }

        /// <summary>
        /// Reads values from the populated reader and assigns values to properties of returned object
        /// </summary>
        /// <param name="rdr">Active SqlDataReader instance</param>
        /// <returns>Instance of UserTradesResult.Movies</returns>
        private UserTradesResult.Movies GetUserTradesMoviesResultFromReader(SqlDataReader rdr)
        {
            UserTradesResult.Movies result = new UserTradesResult.Movies();
            result.TradeId = rdr.GetInt32(0);
            result.RequestUserId = rdr.GetInt32(1);
            result.RequestMovieTitle = rdr.GetString(2);
            result.RequestMovieYear = rdr.GetInt32(3);
            result.RequestMovieFormat = rdr.GetByte(4);
            result.ResponseUserId = rdr.GetInt32(5);
            result.ResponseMovieTitle = rdr.GetString(6);
            result.ResponseMovieYear = rdr.GetInt32(7);
            result.ResponseMovieFormat = rdr.GetByte(8);
            return result;
        }


        private string UserTradesMultiSetIdentifier(SqlDataReader rdr)
        {
            string[] array = new string[rdr.FieldCount];
            for (int idx = 0; idx != array.Length; idx++)
            {
                array[idx] = rdr.GetName(idx);
            }
            string concatColumns = string.Concat(array);

            switch(concatColumns)
            {
                case ("TradeIdTradeStatusIdRequestUserIdRequestUserNameResponseUserIdResponseUserNameRequestDateRequestDurationNotes"): return "Trades";
                case ("TradeIdRequestUserIdRequestMovieTitleRequestMovieYearRequestMovieFormatResponseUserIdResponseMovieTitleResponseMovieYearResponseMovieFormat"): return "Movies";
            }
            throw new Exception("The multi result query could not resolve. This project needs a fresh SQL+ build");
        }
        /// <summary>
        /// Executes the command and processes the results
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private void UserTradesCommand(SqlCommand cmd, UserTradesOutput output)
        {
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                do
                {
                    if (rdr.HasRows)
                    {
                        switch (UserTradesMultiSetIdentifier(rdr))
                        {
                            case "Trades":
                                output.ResultData.TradesResult = new List<UserTradesResult.Trades>();
                                while(rdr.Read())
                                {
                                    output.ResultData.TradesResult.Add(GetUserTradesTradesResultFromReader(rdr));
                                }
                                if(output.ResultData.TradesResult.Count == 0)
                                {
                                    output.ResultData.TradesResult = null;
                                }
                                break;
                            case "Movies":
                                output.ResultData.MoviesResult = new List<UserTradesResult.Movies>();
                                while(rdr.Read())
                                {
                                    output.ResultData.MoviesResult.Add(GetUserTradesMoviesResultFromReader(rdr));
                                }
                                if(output.ResultData.MoviesResult.Count == 0)
                                {
                                    output.ResultData.MoviesResult = null;
                                }
                                break;
                        }
                    }
                } while (rdr.NextResult());
                rdr.Close();
            }
            SetUserTradesCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Executes the command and processes the results async
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private async Task UserTradesCommandAsync(SqlCommand cmd, UserTradesOutput output)
        {
            using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
            {
                do
                {
                    if (rdr.HasRows)
                    {
                        switch (UserTradesMultiSetIdentifier(rdr))
                        {
                            case "Trades":
                                output.ResultData.TradesResult = new List<UserTradesResult.Trades>();
                                while(rdr.Read())
                                {
                                    output.ResultData.TradesResult.Add(GetUserTradesTradesResultFromReader(rdr));
                                }
                                if(output.ResultData.TradesResult.Count == 0)
                                {
                                    output.ResultData.TradesResult = null;
                                }
                                break;
                            case "Movies":
                                output.ResultData.MoviesResult = new List<UserTradesResult.Movies>();
                                while(rdr.Read())
                                {
                                    output.ResultData.MoviesResult.Add(GetUserTradesMoviesResultFromReader(rdr));
                                }
                                if(output.ResultData.MoviesResult.Count == 0)
                                {
                                    output.ResultData.MoviesResult = null;
                                }
                                break;
                        }
                    }
                } while (rdr.NextResult());
                rdr.Close();
            }
            SetUserTradesCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Gets User Trades and Movies for given user id.
        /// SQL+ Routine: crud.UserTrades - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class UserTradesInput or alternatively an instance of a class implementing the IUserTradesInput interface</param>
        /// <returns>Instance of UserTradesOutput</returns>
        public UserTradesOutput UserTrades(IUserTradesInput input)
        {
            ValidateInput(input, "UserTrades");
            UserTradesOutput output = new UserTradesOutput { ResultData = new UserTradesResult() };
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetUserTradesCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    UserTradesCommand(cmd, output);
                }
                return output;
            }
            for(int idx=0; idx <= retryOptions.RetryIntervals.Count; idx++)
            {
                if (idx > 0)
                {
                    System.Threading.Thread.Sleep(retryOptions.RetryIntervals[idx - 1]);
                }
                try
                {
                    using (SqlConnection cnn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = GetUserTradesCommand(cnn, input))
                    {
                        cnn.Open();
						UserTradesCommand(cmd, output);
                        cnn.Close();
                    }
					break;
                }
                catch(SqlException sqlException)
                {
                    AllowRetryOrThrowError(idx, sqlException);
                }
            }
            return output;
        }
        /// <summary>
        /// Gets User Trades and Movies for given user id.
        /// SQL+ Routine: crud.UserTrades - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class UserTradesInput or alternatively an instance of a class implementing the IUserTradesInput interface</param>
        /// <returns>Instance of UserTradesOutput</returns>
        public async Task<UserTradesOutput> UserTradesAsync(IUserTradesInput input)
        {
            ValidateInput(input, "UserTradesAsync");
            UserTradesOutput output = new UserTradesOutput { ResultData = new UserTradesResult() };
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetUserTradesCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    await UserTradesCommandAsync(cmd, output);
                }
                return output;
            }
            for(int idx=0; idx <= retryOptions.RetryIntervals.Count; idx++)
            {
                if (idx > 0)
                {
                    await Task.Delay(retryOptions.RetryIntervals[idx - 1]);
                }
                try
                {
                    using (SqlConnection cnn = new SqlConnection(connectionString))
                    using (SqlCommand cmd = GetUserTradesCommand(cnn, input))
                    {
                        await cnn.OpenAsync();
						await UserTradesCommandAsync(cmd, output);
                        cnn.Close();
                    }
					break;
                }
                catch(SqlException sqlException)
                {
                    AllowRetryOrThrowError(idx, sqlException);
                }
            }
            return output;
        }
    }
}