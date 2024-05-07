namespace UrlShortener.Application.Common.Exceptions {
    public class AlreadyExistsException : Exception {
        /// <summary>
        /// Initializes a new instance of the AlreadyExistsException class with a specified name of the queried object and its key.
        /// </summary>
        /// <param name="objectName">Name of the queried object.</param>
        /// <param name="key">The value by which the object is queried.</param>
        public AlreadyExistsException(string key, string objectName)
            : base($"Queried object {objectName}, Key: {key}") {
        }

        /// <summary>
        /// Initializes a new instance of the AlreadyExistsException class with a specified name of the queried object, its key,
        /// and the exception that is the cause of this exception.
        /// </summary>
        /// <param name="objectName">Name of the queried object.</param>
        /// <param name="key">The value by which the object is queried.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public AlreadyExistsException(string key, string objectName, Exception innerException)
            : base($"Queried object {objectName} already exist, Key: {key}", innerException) {
        }
    }
}
