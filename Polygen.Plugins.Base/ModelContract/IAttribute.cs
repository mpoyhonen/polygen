namespace Polygen.Plugins.Base.ModelContract
{
    public interface IAttribute
    {
        string Name { get; }
        string Type { get; }
        string Value { get; }
    }
}