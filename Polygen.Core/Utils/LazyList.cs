using System;
using System.Collections.Generic;
using System.Text;

namespace Polygen.Core.Utils
{
    public class LazyList<T>: Lazy<List<T>>
    {
        public LazyList(): base(() => new List<T>())
        {
        }
    }
}
