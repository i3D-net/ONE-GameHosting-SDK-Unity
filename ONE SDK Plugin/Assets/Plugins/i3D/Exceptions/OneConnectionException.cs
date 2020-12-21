namespace i3D.Exceptions
{
    public class OneConnectionException : OneException
    {
        public OneConnectionException(OneError error) : base(error)
        {
        }
    }
}