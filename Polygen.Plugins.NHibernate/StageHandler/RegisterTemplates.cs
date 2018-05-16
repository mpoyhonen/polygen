using Polygen.Core.Stage;
using Polygen.Core.Template;

namespace Polygen.Plugins.NHibernate.StageHandler
{
    /// <summary>
    /// Registers all templates in this assembly
    /// </summary>
    public class RegisterTemplates : StageHandlerBase
    {
        public RegisterTemplates() : base(StageType.RegisterTemplates, "NHibernate")
        {
        }

        public ITemplateCollection TemplateCollection { get; set; }

        public override void Execute()
        {
            TemplateCollection.LoadTemplates(GetType().Assembly);
        }
    }
}
