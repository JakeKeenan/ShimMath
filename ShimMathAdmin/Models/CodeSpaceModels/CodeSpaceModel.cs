using ShimMathAdmin.Models.AdminModels;

namespace ShimMathAdmin.Models.CodeSpaceModels
{
    public class CodeSpaceModel : LayoutModel
    {
        public CodeSpaceModel()
        {
            MainBodyView = "Views/Shared/CodeSpace/_CodeSpaceLayout.cshtml";
        }
        public string CodeSpaceBodyView { get; set; }
    }
}
