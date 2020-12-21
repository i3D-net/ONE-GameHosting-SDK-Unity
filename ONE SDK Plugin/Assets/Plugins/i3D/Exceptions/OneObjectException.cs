namespace i3D.Exceptions
{
    public class OneObjectException : OneException
    {
        public OneObjectException(OneError error) : base(error)
        {
        }
    }
}