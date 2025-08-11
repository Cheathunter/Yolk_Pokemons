
namespace Yolk_Pokemon.Application.Exceptions
{
    /// <summary>
    /// Exception used for attempt to create a duplicate record.
    /// </summary>
    public class DuplicateRecordException : Exception
    {
        /// <inheritdoc/>
        public DuplicateRecordException() { }

        /// <inheritdoc/>
        public DuplicateRecordException(string message)
            : base(message) { }

        /// <inheritdoc/>
        public DuplicateRecordException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}