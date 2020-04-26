using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShimMathAdmin.Models.AdminModels;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ShimMathAdmin.Controllers
{
    public class AdminController : Controller
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult EditText(AdminModel model, string newText, string elementID)
        {
            try
            {
                string fileContent;
                using (StreamReader streamReader = new StreamReader(model.MainBodyView))
                {
                    fileContent = streamReader.ReadToEnd();
                    streamReader.Close();
                }

                using (StreamWriter streamWriter = new StreamWriter(model.MainBodyView))
                {

                }
            }
            catch
            {
                return StatusCode(500);
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