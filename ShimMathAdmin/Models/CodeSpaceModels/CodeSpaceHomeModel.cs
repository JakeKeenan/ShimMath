using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShimMathAdmin.Models.CodeSpaceModels
{
    public class CodeSpaceHomeModel : CodeSpaceModel
    {
        public CodeSpaceHomeModel()
        {
            MainBodyView = "/Views/CodeSpace/CodeSpaceHome/CodeSpaceHome.cshtml";
        }
    }
}
