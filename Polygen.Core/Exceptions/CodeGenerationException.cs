using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polygen.Core.Exceptions
{
    public class CodeGenerationException : Exception
    {
        public CodeGenerationException()
        {
        }

        public CodeGenerationException(string message)
            : base(message)
        {
        }

        public CodeGenerationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
