using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: CLSCompliant(true)]

namespace CSLib
{
    public interface IValidator
    {
        // does the data match the given regular expression
        bool IsValid(string testData, string regex);
    }
}
