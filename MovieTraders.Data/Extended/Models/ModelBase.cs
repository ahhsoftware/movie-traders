// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SqlPlus.net
//     For more information on SqlPlus.net visit http://www.SqlPlus.net
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.IO;
using System;

namespace MovieTraders.Data.Extended.Models
{
    #region Valid Input

    /// <summary>
    /// Interface for input object base class.
    /// </summary>
    public interface IValidInput
    {
        /// <summary>
        /// List of ValidationResults populated during the is valid call
        /// </summary>
        List<ValidationResult> ValidationResults { get; set; }

        /// <summary>
        /// Validates the input object and populates ValidationResults
        /// </summary>
        /// <returns>True if no validation errors are found</returns>
        bool IsValid();
    }

    /// <summary>
    /// This is the abstract class inplementation of the IValidInput interface, which all input objects derive from.
    /// </summary>
    public abstract class ValidInput : IValidInput
    {
        /// <summary>
        /// List of ValidationResults populated during the is valid call
        /// </summary>
        public List<ValidationResult> ValidationResults { set; get; } = new List<ValidationResult>();

        /// <summary>
        /// Validates the input object and populates ValidationResults
        /// If overridden in a derived class use base.IsValid() after any custom validation.
        /// </summary>
        /// <returns>True if no validation errors are found</returns>
        public virtual bool IsValid()
        {
            Validator.TryValidateObject(this, new ValidationContext(this), ValidationResults, true);
            return ValidationResults.Count == 0;
        }
    }

    #endregion

    #region Transient Logger

    /// <summary>
    /// Implementation of this interface is utilized for any transient error logging.
    /// </summary>
    public interface ITransientLogger
    {
        /// <summary>
        /// Logs the exception
        /// </summary>
        /// <param name="sqlException">The exception to log</param>
        void Log(SqlException sqlException);
    }

    /// <summary>
    /// Example of a simple implentation of ITransientLogger
    /// </summary>
    public class TransientFileLogger : ITransientLogger
    {
        /// <summary>
        /// Logs the exception
        /// </summary>
        /// <param name="sqlException">The exception to log</param>
        public void Log(SqlException sqlException)
        {
            try
            {
                using (StreamWriter wr = File.CreateText(System.IO.Path.GetTempFileName()))
                {
                    wr.WriteAsync(sqlException.ToString());
                }
            }
            catch { }
        }
    }

    #endregion

    #region RetryOptions
    /// <summary>
    /// Implementation of this interface is utilized for handling any transient errors.
    /// </summary>
    public interface IRetryOptions
    {
        /// <summary>
        /// List of error numbers to treat as transient
        /// </summary>
        List<int> TransientErrorNumbers { get; }

        /// <summary>
        /// List of Retry Intervals in milliseconds
        /// </summary>
        List<int> RetryIntervals { get; }

        /// <summary>
        /// Implentation of ITransientLogger
        /// </summary>
        ITransientLogger Logger { get; }
    }

    /// <summary>
    /// Base class for retry options. Non transaction instantiation of the service component takes an optional parameter for retry options.
    /// When no options are explicitly passed the NoRetryOptions are utilized.
    /// </summary>
    public abstract class RetryOptions : IRetryOptions
    {
        /// <summary>
        /// Constructor for the RetryObjections
        /// </summary>
        /// <param name="transientErrorNumbers">List of error numbers to treat as transient</param>
        /// <param name="retryIntervals">List of Retry Intervals in milliseconds</param>
        /// <param name="logger">Implentation of ITransientLogger</param>
        public RetryOptions(List<int> transientErrorNumbers, List<int> retryIntervals, ITransientLogger logger)
        {
            if(transientErrorNumbers == null)
            {
                throw new ArgumentNullException(nameof(transientErrorNumbers), "Value cannot be null");
            }

            if(retryIntervals == null)
            {
                throw new ArgumentNullException(nameof(retryIntervals), "Value cannot be null");
            }

            TransientErrorNumbers = transientErrorNumbers;
            RetryIntervals = retryIntervals;
            Logger = logger;
        }

        /// <summary>
        /// List of error numbers to treat as transient
        /// </summary>
        public List<int> TransientErrorNumbers { private set; get; }

        /// <summary>
        /// List of Retry Intervals in milliseconds
        /// </summary>
        public List<int> RetryIntervals { private set; get; }
        
        /// <summary>
        /// Implentation of ITransientLogger
        /// </summary>
        public ITransientLogger Logger { private set; get; }
    }

    /// <summary>
    /// No error numbers, no intervals, and no logging.
    /// </summary>
    public class NoRetryOptions : RetryOptions
    {
        /// <summary>
        /// Construtor for the NoRetryOptions 
        /// </summary>
        public NoRetryOptions() :
            base(
                new List<int>(),
                new List<int>(),
                null
                )
        { }
    }

    /// <summary>
    /// Somewhat balanced retry options for AzureSql where there is a user interacting with the underlying DB calls.
    /// </summary>
    public class SampleRetryOptions : RetryOptions
    {
        /// <summary>
        /// Constructor for the SampleRetryOptions
        /// </summary>
        public SampleRetryOptions() :
            base(
                new List<int> { 2, 4060, 40197, 40501, 40613, 49918, 49919, 49920, 11001 },
                new List<int> { 1000, 2000, 5000 },
                new TransientFileLogger()
                )
        { }
    }

    #endregion
}