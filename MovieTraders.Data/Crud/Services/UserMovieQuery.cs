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
        /// Builds the command object for UserMovieQuery method.
        /// </summary>
        /// <param name="cnn">The connection that will execute the procedure.</param>
        /// <param name="input">UserMovieQueryInput instance for loading parameter values.</param>
        /// <returns>SqlCommand ready for execution.</returns>
        private SqlCommand GetUserMovieQueryCommand(SqlConnection cnn, IUserMovieQueryInput input)
        {
            SqlCommand result = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[crud].[UserMovieQuery]",
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
                ParameterName = "@MovieId",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                Scale = 0,
                Precision = 10,
				Value = (object)input.MovieId ?? DBNull.Value
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
        private void SetUserMovieQueryCommandOutputs(SqlCommand cmd, UserMovieQueryOutput output)
        {
            if(cmd.Parameters[2].Value != DBNull.Value)
            {
                output.ReturnValue = (UserMovieQueryOutput.Returns)cmd.Parameters[2].Value;
            }
        }


        /// <summary>
        /// Reads values from the populated reader and assigns values to properties of returned object
        /// </summary>
        /// <param name="rdr">Active SqlDataReader instance</param>
        /// <returns>Instance of UserMovieQueryResult.UserMovies</returns>
        private UserMovieQueryResult.UserMovies GetUserMovieQueryUserMoviesResultFromReader(SqlDataReader rdr)
        {
            UserMovieQueryResult.UserMovies result = new UserMovieQueryResult.UserMovies();
            result.MovieId = rdr.GetInt32(0);
            result.FormatId = rdr.GetByte(1);
            return result;
        }

        /// <summary>
        /// Reads values from the populated reader and assigns values to properties of returned object
        /// </summary>
        /// <param name="rdr">Active SqlDataReader instance</param>
        /// <returns>Instance of UserMovieQueryResult.Movies</returns>
        private UserMovieQueryResult.Movies GetUserMovieQueryMoviesResultFromReader(SqlDataReader rdr)
        {
            UserMovieQueryResult.Movies result = new UserMovieQueryResult.Movies();
            result.MovieId = rdr.GetInt32(0);
            result.GenreId = rdr.GetByte(1);
            result.Year = rdr.GetInt32(2);
            result.Title = rdr.GetString(3);
            result.Link = rdr.GetString(4);
            return result;
        }


        private string UserMovieQueryMultiSetIdentifier(SqlDataReader rdr)
        {
            switch(rdr.FieldCount)
            {
                case (2): return "UserMovies";
                case (5): return "Movies";
            }
            throw new Exception("The multi result query could not resolve. This project needs a fresh SQL+ build");
        }
        /// <summary>
        /// Executes the command and processes the results
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private void UserMovieQueryCommand(SqlCommand cmd, UserMovieQueryOutput output)
        {
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                do
                {
                    if (rdr.HasRows)
                    {
                        switch (UserMovieQueryMultiSetIdentifier(rdr))
                        {
                            case "UserMovies":
                                output.ResultData.UserMoviesResult = new List<UserMovieQueryResult.UserMovies>();
                                while(rdr.Read())
                                {
                                    output.ResultData.UserMoviesResult.Add(GetUserMovieQueryUserMoviesResultFromReader(rdr));
                                }
                                if(output.ResultData.UserMoviesResult.Count == 0)
                                {
                                    output.ResultData.UserMoviesResult = null;
                                }
                                break;
                            case "Movies":
                                output.ResultData.MoviesResult = new List<UserMovieQueryResult.Movies>();
                                while(rdr.Read())
                                {
                                    output.ResultData.MoviesResult.Add(GetUserMovieQueryMoviesResultFromReader(rdr));
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
            SetUserMovieQueryCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Executes the command and processes the results async
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private async Task UserMovieQueryCommandAsync(SqlCommand cmd, UserMovieQueryOutput output)
        {
            using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
            {
                do
                {
                    if (rdr.HasRows)
                    {
                        switch (UserMovieQueryMultiSetIdentifier(rdr))
                        {
                            case "UserMovies":
                                output.ResultData.UserMoviesResult = new List<UserMovieQueryResult.UserMovies>();
                                while(rdr.Read())
                                {
                                    output.ResultData.UserMoviesResult.Add(GetUserMovieQueryUserMoviesResultFromReader(rdr));
                                }
                                if(output.ResultData.UserMoviesResult.Count == 0)
                                {
                                    output.ResultData.UserMoviesResult = null;
                                }
                                break;
                            case "Movies":
                                output.ResultData.MoviesResult = new List<UserMovieQueryResult.Movies>();
                                while(rdr.Read())
                                {
                                    output.ResultData.MoviesResult.Add(GetUserMovieQueryMoviesResultFromReader(rdr));
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
            SetUserMovieQueryCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Queries dbo.Movie table.
        /// SQL+ Routine: crud.UserMovieQuery - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class UserMovieQueryInput or alternatively an instance of a class implementing the IUserMovieQueryInput interface</param>
        /// <returns>Instance of UserMovieQueryOutput</returns>
        public UserMovieQueryOutput UserMovieQuery(IUserMovieQueryInput input)
        {
            ValidateInput(input, "UserMovieQuery");
            UserMovieQueryOutput output = new UserMovieQueryOutput { ResultData = new UserMovieQueryResult() };
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetUserMovieQueryCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    UserMovieQueryCommand(cmd, output);
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
                    using (SqlCommand cmd = GetUserMovieQueryCommand(cnn, input))
                    {
                        cnn.Open();
						UserMovieQueryCommand(cmd, output);
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
        /// Queries dbo.Movie table.
        /// SQL+ Routine: crud.UserMovieQuery - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class UserMovieQueryInput or alternatively an instance of a class implementing the IUserMovieQueryInput interface</param>
        /// <returns>Instance of UserMovieQueryOutput</returns>
        public async Task<UserMovieQueryOutput> UserMovieQueryAsync(IUserMovieQueryInput input)
        {
            ValidateInput(input, "UserMovieQueryAsync");
            UserMovieQueryOutput output = new UserMovieQueryOutput { ResultData = new UserMovieQueryResult() };
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetUserMovieQueryCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    await UserMovieQueryCommandAsync(cmd, output);
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
                    using (SqlCommand cmd = GetUserMovieQueryCommand(cnn, input))
                    {
                        await cnn.OpenAsync();
						await UserMovieQueryCommandAsync(cmd, output);
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