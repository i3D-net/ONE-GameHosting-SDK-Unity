namespace i3D.Exceptions
{
    public class OneCountException : OneException
    {
        public OneCountException(OneError error) : base(error)
        {
        }
    }
}