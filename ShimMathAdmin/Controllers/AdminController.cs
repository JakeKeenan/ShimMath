using ShimMathAdmin.Models.AdminModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;
using ShimMathAdmin.Models.CodeSpaceModels;

namespace ShimMathAdmin.Controllers
{
    public class AdminController : Controller
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public IActionResult EditText(CodeSpaceHomeModel model, string newText, string elementID)
        {
            string fileContent;
            using (StreamReader streamReader = new StreamReader(model.MainBodyView))//needs actual file name, not just the path
            {
                fileContent = streamReader.ReadToEnd();
                streamReader.Close();
            }
            //<h1 id="WelcomeHeading">Welcome To Code Space</h1>
            //\<([a-zA-Z1-9]*)\s.*id\s*=\s*\"editHeadExitOptions\"((.|\n)*?)\<\/\1>
            //Match match = Regex.Match(fileContent, "\\<([a-zA-Z1-9]*)\\s.*id\\s*=\\s*\"" + elementID + "\\s*\"((.|\n)*?)\\<\\/\\1>");
            //string modifiedContent = Regex.Replace(fileContent, "\\<.*id\\s*=\\s*\"" + elementID + "\".*\\>.*\\</.*\\>", "");
            string modifiedContent = Regex.Replace(fileContent,
                    "(?<startTag>\\<([a-zA-Z1-9]*)\\s.*id\\s*=\\s*\"" +
                    elementID + "\\s*\".*?\\>)(?<middleContent>(.|\n)*?)(?<endTag>\\<\\/\\1>)",
                    "${startTag}" + newText + "${endTag}");
            using (StreamWriter streamWriter = new StreamWriter(model.MainBodyView))
            {
                streamWriter.Write(modifiedContent);
                streamWriter.Close();
            }

            return CreatedAtAction("EditText", elementID);
        }

        public IActionResult EditTextGet(AdminModel model)
        {
            string fileText = "stuff";
            string line  = "";
            try
            {
                using (StreamReader sr = new StreamReader("Views/CodeSpace/CodeSpaceHome.cshtml"))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        fileText += line;
                    }
                }
            }
            catch (IOException e)
            {

            }
            return CreatedAtAction("", new { id = 2}, line);
        }


        public IActionResult EditTextPut(AdminModel model)
        {
            return View();
        }

        [HttpPut]
        [ActionName("EditPost")]
        public IActionResult EditUpdate(AdminModel model, string updateName)
        {
            return View();
        }

        [HttpPost]
        [ActionName("CreatePost")]
        public IActionResult CreateUpdate(AdminModel model, string updateName)
        {
            return View();
        }
    }
}