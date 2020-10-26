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
        /// Builds the command object for MoviesByCounty method.
        /// </summary>
        /// <param name="cnn">The connection that will execute the procedure.</param>
        /// <param name="input">MoviesByCountyInput instance for loading parameter values.</param>
        /// <returns>SqlCommand ready for execution.</returns>
        private SqlCommand GetMoviesByCountyCommand(SqlConnection cnn, IMoviesByCountyInput input)
        {
            SqlCommand result = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[crud].[MoviesByCounty]",
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
                ParameterName = "@PageSize",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                Scale = 0,
                Precision = 10,
				Value = input.PageSize
            });

            result.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@PageNumber",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Int,
                Scale = 0,
                Precision = 10,
				Value = input.PageNumber
            });

            result.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@PageCount",
                Direction = ParameterDirection.Output,
                SqlDbType = SqlDbType.Int,
                Scale = 0,
                Precision = 10,
                Value = DBNull.Value
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
        private void SetMoviesByCountyCommandOutputs(SqlCommand cmd, MoviesByCountyOutput output)
        {
            if(cmd.Parameters[3].Value != DBNull.Value)
            {
                output.PageCount = (int?)cmd.Parameters[3].Value;
            }
            if(cmd.Parameters[4].Value != DBNull.Value)
            {
                output.ReturnValue = (MoviesByCountyOutput.Returns)cmd.Parameters[4].Value;
            }
        }

        /// <summary>
        /// Reads values from the populated reader and assigns values to properties of returned object
        /// </summary>
        /// <param name="rdr">Active SqlDataReader instance</param>
        /// <returns>Instance of MoviesByCountyResult</returns>
        private MoviesByCountyResult GetMoviesByCountyResultFromReader(SqlDataReader rdr)
        {
            MoviesByCountyResult result = new MoviesByCountyResult();

            result.MovieId = rdr.GetInt32(0);

            result.Title = rdr.GetString(1);

            result.Year = rdr.GetInt32(2);

            result.Link = rdr.GetString(3);

            result.GenreId = rdr.GetByte(4);

            result.FormatId = rdr.GetByte(5);

            result.UserMovieId = rdr.GetInt32(6);

            result.UserId = rdr.GetInt32(7);

            return result;
        }


        /// <summary>
        /// Executes the command and processes the results
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private void MoviesByCountyCommand(SqlCommand cmd, MoviesByCountyOutput output)
        {
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                output.ResultData = new List<MoviesByCountyResult>();
                while(rdr.Read())
                {
                    output.ResultData.Add(GetMoviesByCountyResultFromReader(rdr));
                }
                rdr.Close();
            }
            SetMoviesByCountyCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Executes the command and processes the results async
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private async Task MoviesByCountyCommandAsync(SqlCommand cmd, MoviesByCountyOutput output)
        {
            using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
            {
                output.ResultData = new List<MoviesByCountyResult>();
                while(rdr.Read())
                {
                    output.ResultData.Add(GetMoviesByCountyResultFromReader(rdr));
                }
                rdr.Close();
            }
            SetMoviesByCountyCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Movies by County based on user Id excludes users records
        /// SQL+ Routine: crud.MoviesByCounty - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class MoviesByCountyInput or alternatively an instance of a class implementing the IMoviesByCountyInput interface</param>
        /// <returns>Instance of MoviesByCountyOutput</returns>
        public MoviesByCountyOutput MoviesByCounty(IMoviesByCountyInput input)
        {
            ValidateInput(input, "MoviesByCounty");
            MoviesByCountyOutput output = new MoviesByCountyOutput();
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetMoviesByCountyCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    MoviesByCountyCommand(cmd, output);
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
                    using (SqlCommand cmd = GetMoviesByCountyCommand(cnn, input))
                    {
                        cnn.Open();
						MoviesByCountyCommand(cmd, output);
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
        /// Movies by County based on user Id excludes users records
        /// SQL+ Routine: crud.MoviesByCounty - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class MoviesByCountyInput or alternatively an instance of a class implementing the IMoviesByCountyInput interface</param>
        /// <returns>Instance of MoviesByCountyOutput</returns>
        public async Task<MoviesByCountyOutput> MoviesByCountyAsync(IMoviesByCountyInput input)
        {
            ValidateInput(input, "MoviesByCountyAsync");
            MoviesByCountyOutput output = new MoviesByCountyOutput();
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetMoviesByCountyCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    await MoviesByCountyCommandAsync(cmd, output);
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
                    using (SqlCommand cmd = GetMoviesByCountyCommand(cnn, input))
                    {
                        await cnn.OpenAsync();
						await MoviesByCountyCommandAsync(cmd, output);
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