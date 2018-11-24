namespace Polygen.Core.Impl.OutputModel
{
    public class OutputModelFragment
    {
        public OutputModelFragment(string type)
        {
            Type = type;
        }

        /// <summary>
        /// Defines the fragment type. 
        /// </summary>
        public string Type { get; }
    }
}