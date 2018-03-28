using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygen.Core.OutputModel
{
    public interface IOutputModelCollection
    {
        void AddOutputModel(IOutputModel outputModel);
        IEnumerable<IOutputModel> Models { get; }
    }
}
