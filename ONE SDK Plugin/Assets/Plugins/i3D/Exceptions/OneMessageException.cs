namespace i3D.Exceptions
{
    public class OneMessageException : OneException
    {
        public OneMessageException(OneError error) : base(error)
        {
        }
    }
}