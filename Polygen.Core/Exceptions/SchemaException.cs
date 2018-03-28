using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polygen.Core.Exceptions
{
    public class SchemaException : CodeGenerationException
    {
        public SchemaException()
            : base()
        {
        }

        public SchemaException(string message)
            : base(message)
        {
        }

        public SchemaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
