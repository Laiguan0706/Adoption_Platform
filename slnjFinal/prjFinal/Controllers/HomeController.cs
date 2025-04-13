using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using prjFinal.Models;
using System.Web.Security;

namespace prjFinal.Controllers

{
    [Authorize]
    public class HomeController : Controller
    {

        // GET: Home
        DogsEntities db = new DogsEntities();
       
        int pagesize = 6;
        [Authorize(Users = "Welcome")]
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            int nowpage = page < 1 ? 1 : page;
            var dog = db.Table流浪狗s1091722.OrderByDescending(m => m.領養日期); 
            var result = dog.ToPagedList(nowpage, pagesize);
            return View(result);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
        
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string txtUid, string txtPwd)
        {
            string[] uidAry = new string[] { "Welcome" };
            string[] pwdAry = new string[] { "Project"};

            int index = -1;
            for (int i = 0; i < uidAry.Length; i++)
            {
                if (uidAry[i] == txtUid && pwdAry[i] == txtPwd)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                ViewBag.Err = "帳密錯誤!!!!";
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(txtUid, true);

                Response.Cookies["LoginOK"].Value = "Yes";
                Session["LoginOK"] = "Yes";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Table流浪狗s1091722 add)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Error = false;
                var test = db.Table流浪狗s1091722
                    .Where(m => m.編號 == add.編號)
                    .FirstOrDefault();
                if (test != null)
                {
                    ViewBag.Error = true;
                    return View(add);
                }
                db.Table流浪狗s1091722.Add(add);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(add);
        }

        public ActionResult Edit(string Id)
        {
            var dog = db.Table流浪狗s1091722
                .Where(m => m.編號 == Id).FirstOrDefault();
            return View(dog);
        }

        [HttpPost]
        public ActionResult Edit(Table流浪狗s1091722 change)
        {
            if(ModelState.IsValid)
            {
                var old = db.Table流浪狗s1091722
                    .Where(m => m.編號 == change.編號)
                    .FirstOrDefault();
                old.動物別 = change.動物別;
                old.性別 = change.性別;
                old.是否打過疫苗 = change.是否打過疫苗;
                old.領養人 = change.領養人;
                old.電話 = change.電話;
                old.電子郵件 = change.電子郵件;
                old.領養日期 = change.領養日期;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(change);
        }

        public ActionResult Delete(string Id)
        {
            var dog = db.Table流浪狗s1091722
                .Where(m => m.編號 == Id).FirstOrDefault();
            db.Table流浪狗s1091722.Remove(dog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}