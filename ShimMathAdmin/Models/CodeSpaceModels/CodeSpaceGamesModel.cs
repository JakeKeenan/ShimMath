using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShimMathAdmin.Models.CodeSpaceModels
{
    public class CodeSpaceGamesModel : CodeSpaceModel
    {
        public CodeSpaceGamesModel()
        {
            CodeSpaceBodyView = "Views/CodeSpace/CodeSpaceGamesList.cshtml";
        }
    }
}
