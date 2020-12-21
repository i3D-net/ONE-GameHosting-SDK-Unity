namespace i3D.Exceptions
{
    public class OneValidationException : OneException
    {
        public OneValidationException(OneError error) : base(error)
        {
        }
    }
}