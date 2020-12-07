using System;
using i3D.Exceptions;

namespace i3D
{
    internal static class OneErrorValidator
    {
        public static void Validate(int errorCode)
        {
            if (!Enum.IsDefined(typeof(OneError), errorCode))
                throw new OneInvalidErrorCodeException(errorCode);

            var error = (OneError) errorCode;

            switch (error)
            {
                case OneError.OneErrorNone:
                    return;
                
                // TODO
            }
        }
    }
}