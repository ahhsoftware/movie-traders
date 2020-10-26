using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MovieTraders.Functions
{
    public class ServiceFactory
    {
        private class CrudLogger : Data.Crud.Models.ITransientLogger
        {
            private readonly ILogger log;

            public CrudLogger(ILogger log)
            {
                this.log = log;
            }
            public void Log(SqlException sqlException)
            {
                log.LogError(sqlException.ToString());
            }
        }

        private class CrudRetryOptions : Data.Crud.Models.RetryOptions
        {
            public CrudRetryOptions(ILogger log) :
            base(
                new List<int> { 2, 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 },
                new List<int> { 1000, 2000, 5000, 10000 },
                new CrudLogger(log)
                )
            { }
        }

        private static Data.Crud.Service crudService;
        public static Data.Crud.Service CrudService(ILogger log)
        {
            if (crudService == null)
            {
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                crudService = new Data.Crud.Service(connectionString, new CrudRetryOptions(log));
            }
            return crudService;
        }

        private class ExtendedLogger : Data.Extended.Models.ITransientLogger
        {
            private readonly ILogger log;

            public ExtendedLogger(ILogger log)
            {
                this.log = log;
            }
            public void Log(SqlException sqlException)
            {
                log.LogError(sqlException.ToString());
            }
        }

        private class ExtendedRetryOptions : Data.Extended.Models.RetryOptions
        {
            public ExtendedRetryOptions(ILogger log) :
            base(
                new List<int> { 2, 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 },
                new List<int> { 1000, 2000 },
                new ExtendedLogger(log)
                )
            { }
        }

        

        private static Data.Extended.Service extendedService;
        public static Data.Extended.Service ExtendedService(ILogger log)
        {
            if (extendedService == null)
            {
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                extendedService = new Data.Extended.Service(connectionString, new ExtendedRetryOptions(log));
            };

            return extendedService;
        }
    }
}
