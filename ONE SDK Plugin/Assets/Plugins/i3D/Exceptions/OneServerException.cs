namespace i3D.Exceptions
{
    public class OneServerException : OneException
    {
        public OneServerException(OneError error) : base(error)
        {
        }
    }
}