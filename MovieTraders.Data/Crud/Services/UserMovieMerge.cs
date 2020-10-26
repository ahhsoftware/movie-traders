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
using MovieTraders.Data.UserDefinedTypes;
namespace MovieTraders.Data.Crud
{
    public partial class Service
    {

        /// <summary>
        /// Builds the command object for UserMovieMerge method.
        /// </summary>
        /// <param name="cnn">The connection that will execute the procedure.</param>
        /// <param name="input">UserMovieMergeInput instance for loading parameter values.</param>
        /// <returns>SqlCommand ready for execution.</returns>
        private SqlCommand GetUserMovieMergeCommand(SqlConnection cnn, IUserMovieMergeInput input)
        {
            SqlCommand result = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[crud].[UserMovieMerge]",
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
				Value = input.MovieId
            });

            result.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@Formats",
                Direction = ParameterDirection.Input,
                SqlDbType = SqlDbType.Structured,
                TypeName = "dbo.TinyInts",
				Value = TinyIntsHelpers.BuildDataTable(input.Formats)
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
        private void SetUserMovieMergeCommandOutputs(SqlCommand cmd, UserMovieMergeOutput output)
        {
            if(cmd.Parameters[3].Value != DBNull.Value)
            {
                output.ReturnValue = (UserMovieMergeOutput.Returns)cmd.Parameters[3].Value;
            }
        }
        /// <summary>
        /// Executes the command and processes the results
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private void UserMovieMergeCommand(SqlCommand cmd, UserMovieMergeOutput output)
        {
            cmd.ExecuteNonQuery();
            SetUserMovieMergeCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Executes the command and processes the results async
        /// </summary>
        /// <param name="cmd">Active SqlCommand instance</param>
        /// <param name="output">EmptyStringInsertOutput instance</param>
        private async Task UserMovieMergeCommandAsync(SqlCommand cmd, UserMovieMergeOutput output)
        {
            await cmd.ExecuteNonQueryAsync();
            SetUserMovieMergeCommandOutputs(cmd, output);
        }
        /// <summary>
        /// Merges dbo.UserMovie table.
        /// SQL+ Routine: crud.UserMovieMerge - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class UserMovieMergeInput or alternatively an instance of a class implementing the IUserMovieMergeInput interface</param>
        /// <returns>Instance of UserMovieMergeOutput</returns>
        public UserMovieMergeOutput UserMovieMerge(IUserMovieMergeInput input)
        {
            ValidateInput(input, "UserMovieMerge");
            UserMovieMergeOutput output = new UserMovieMergeOutput();
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetUserMovieMergeCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    UserMovieMergeCommand(cmd, output);
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
                    using (SqlCommand cmd = GetUserMovieMergeCommand(cnn, input))
                    {
                        cnn.Open();
						UserMovieMergeCommand(cmd, output);
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
        /// Merges dbo.UserMovie table.
        /// SQL+ Routine: crud.UserMovieMerge - Authored by Alan Hyneman
        /// </summary>
        /// <param name="input">An instance of class UserMovieMergeInput or alternatively an instance of a class implementing the IUserMovieMergeInput interface</param>
        /// <returns>Instance of UserMovieMergeOutput</returns>
        public async Task<UserMovieMergeOutput> UserMovieMergeAsync(IUserMovieMergeInput input)
        {
            ValidateInput(input, "UserMovieMergeAsync");
            UserMovieMergeOutput output = new UserMovieMergeOutput();
			if(sqlConnection != null)
            {
                using (SqlCommand cmd = GetUserMovieMergeCommand(sqlConnection, input))
                {
                    cmd.Transaction = sqlTransaction;
                    await UserMovieMergeCommandAsync(cmd, output);
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
                    using (SqlCommand cmd = GetUserMovieMergeCommand(cnn, input))
                    {
                        await cnn.OpenAsync();
						await UserMovieMergeCommandAsync(cmd, output);
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