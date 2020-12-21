namespace i3D.Exceptions
{
    public class OnePayloadException : OneException
    {
        public OnePayloadException(OneError error) : base(error)
        {
        }
    }
}