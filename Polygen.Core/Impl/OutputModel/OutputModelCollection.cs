using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.OutputModel;

namespace Polygen.Core.Impl.OutputModel
{
    public class OutputModelCollection: IOutputModelCollection
    {
        private readonly List<IOutputModel> _models = new List<IOutputModel>();

        public IEnumerable<IOutputModel> Models => this._models;

        public void AddOutputModel(IOutputModel outputModel)
        {
            this._models.Add(outputModel);
        }
    }
}
