using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityCodeFirstMVCSolution.Models;

namespace EntityCodeFirstMVCSolution.Controllers
{
    public class TestController : Controller
    {
        public SimpleDomain SimpleDomain = new SimpleDomain();
        public static List<EditStandardClass> EditStandardList = new List<EditStandardClass>();
        public static List<EditTeahcerClass> EditTeahcerList = new List<EditTeahcerClass>();

        //Standard
        [HttpGet]
        public ActionResult Index()
        {
            foreach (var data in EditStandardList)
            {
                ViewData["StandardID"] = data.StandardId;
                ViewData["Standard"] = data.StandardName;
            }
            EditStandardList.Clear();
            return View(SimpleDomain.GetStandardList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                Standard standard = new Standard()
                {
                    StandardName = fc["txtName"]
                };
                SimpleDomain.AddStandard(standard);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                Standard standard = new Standard()
                {
                    StandardId = int.Parse(fc["txtID"]),
                    StandardName = fc["txtName"]
                };
                SimpleDomain.UpdateStandard(standard);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditStandard(int id)
        {
            var result = SimpleDomain.GetStandardList().Where(x => x.StandardId == id);
            foreach (var data in result)
            {
                EditStandardClass esc = new EditStandardClass()
                {
                    StandardId = data.StandardId,
                    StandardName = data.StandardName
                };
                EditStandardList.Add(esc);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult DeleteStandard(int id)
        {
            SimpleDomain.DeleteStandard(id);
            return RedirectToAction("Index");
        }


        //Teachers
        [HttpGet]
        public ActionResult TeacherForm()
        {
            foreach (var data in EditTeahcerList)
            {
                ViewData["TeacherID"] = data.TeacherId;
                ViewData["Teacher"] = data.TeacherName;
            }
            EditTeahcerList.Clear();
            return View(SimpleDomain.GetTeacherList());
        }

        [HttpPost]
        public ActionResult TeacherForm(FormCollection fc)
        {
            if (fc["btnInsert"] == "Insert")
            {
                Teacher teacher = new Teacher()
                {
                    TeacherName = fc["txtName"]
                };
                SimpleDomain.AddTeacher(teacher);
            }
            else if (fc["btnUpdate"] == "Update")
            {
                Teacher teacher = new Teacher()
                {
                    TeacherId = int.Parse(fc["txtID"]),
                    TeacherName = fc["txtName"]
                };
                SimpleDomain.UpdateTeacher(teacher);
            }
            return RedirectToAction("TeacherForm");
        }

        [HttpGet]
        public ActionResult EditTeacher(int id)
        {
            var result = SimpleDomain.GetTeacherList().Where(x => x.TeacherId == id);
            foreach (var data in result)
            {
                EditTeahcerClass etc = new EditTeahcerClass()
                {
                    TeacherId = data.TeacherId,
                    TeacherName = data.TeacherName
                };
                EditTeahcerList.Add(etc);
            }
            return RedirectToAction("TeacherForm");
        }

        [HttpGet]
        public ActionResult DeleteTeacher(int id)
        {
            SimpleDomain.DeleteTeacher(id);
            return RedirectToAction("TeacherForm");
        }

        //PartialViewExampleForList
        [HttpGet]
        public ActionResult PartialFormExample()
        {

            ViewBag.Message = "Welcome to ASP.NET MVC!";
            
            return View(new PartialModel2() { PartialModel = Sampledetails() });
        }

        //PartialViewExampleForLayout
        [HttpGet]
        public ActionResult LoginDesign()
        {
            return View();
        }


        private List<PartialModel2> Sampledetails()
        {
            List<PartialModel2> model = new List<PartialModel2>();
            model.Add(new PartialModel2()
            {
                Name = "Rima",
                Age = 20,
                Address = "Kannur"
            });
            model.Add(new PartialModel2()
            {
                Name = "Rohan",
                Age = 23,
                Address = "Ernakulam"
            });
            model.Add(new PartialModel2()
            {
                Name = "Reshma",
                Age = 22,
                Address = "Kannur"
            });
            return model;
        }


    }
}