using System;
using System.Text;
using System.Xml.Linq;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;

namespace Polygen.Core.Exceptions
{
    public class OutputModelException : CodeGenerationException
    {
        public OutputModelException(IOutputModel model, string message)
            : base(FormatMessage(model, message))
        {
        }

        public OutputModelException(IOutputModel model, string message, Exception innerException)
             : base(FormatMessage(model, message), innerException)
        {
        }

        private static string FormatMessage(IOutputModel outputModel, string message)
        {
            var buf = new StringBuilder(512);

            buf.Append("Error in output model '").Append(outputModel.Type).Append("'");

            if (outputModel.DesignModel != null)
            {
                var designModelElement = outputModel.DesignModel.Element;

                buf.Append(" for design model ");
                buf.Append(designModelElement.Definition.Name.LocalName);

                buf.Append(": ");

                buf.Append(message);
                buf.Append(" (").Append(outputModel.DesignModel.ParseLocation).Append(")");
            }

            return buf.ToString();
        }
    }
}
