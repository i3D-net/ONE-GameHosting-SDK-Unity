namespace i3D.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the SDK returns a ONE_ERROR_MESSAGE_* error code.
    /// </summary>
    public class OneMessageException : OneException
    {
        public OneMessageException(OneError error) : base(error)
        {
        }
    }
}