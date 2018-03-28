using Polygen.Core.Stage;
using Polygen.Core.Template;

namespace Polygen.Plugins.Entity.StageHandler
{
    /// <summary>
    /// Registers all templates in this assembly
    /// </summary>
    public class RegisterTemplates : StageHandlerBase
    {
        public RegisterTemplates() : base(StageType.RegisterTemplates, "Entity")
        {
        }

        public ITemplateCollection TemplateCollection { get; set; }

        public override void Execute()
        {
            this.TemplateCollection.LoadTemplates(this.GetType().Assembly);
        }
    }
}
