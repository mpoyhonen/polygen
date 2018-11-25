// ReSharper disable InconsistentNaming
namespace Polygen.Plugins.NHibernate
{
    public static class NHibernatePluginConstants
    {
        public const string OutputModelType_Entity_GeneratedClass = "NHibernate/Entity/GeneratedClass";
        public const string OutputModelType_Entity_CustomClass = "NHibernate/Entity/CustomClass";
        public const string OutputModelType_Mapping = "NHibernate/Mapping";

        public const string TemplateResourcePrefix = "Polygen.Plugins.NHibernate.Templates";
        public const string TemplateNamePrefix = "NHibernate";
        
        public const string OutputTemplateName_Entity_GeneratedClass = OutputModelType_Entity_GeneratedClass;
        public const string OutputTemplateName_Entity_CustomClass = OutputModelType_Entity_CustomClass;
        
        public const string Language_SQL = "SQL";
    }
}
