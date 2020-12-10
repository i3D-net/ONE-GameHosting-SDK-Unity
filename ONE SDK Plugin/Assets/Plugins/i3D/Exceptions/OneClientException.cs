namespace i3D.Exceptions
{
    public class OneClientException : OneException
    {
        public OneClientException(OneError error) : base(error)
        {
        }
    }
}