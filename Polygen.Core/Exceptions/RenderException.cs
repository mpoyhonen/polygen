using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polygen.Core.Exceptions
{
    public class RenderException : CodeGenerationException
    {
        public RenderException()
            : base()
        {
        }

        public RenderException(string message)
            : base(message)
        {
        }

        public RenderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
