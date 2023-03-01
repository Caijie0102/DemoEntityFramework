using DemoEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoEntityFramework.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        public ActionResult Index()
        {
            //建立資料模型實體物件
            var db = new dbShoppingEntities();
            //取得db物件內的什麼內容了，我們這邊要的是OrderDetail，為了方便View使用把它轉成List集合
            var model = db.OrderDetail.ToList();
            //var model = db.OrderDetail.Where(m => m.UserId == "Bonny456" && m.Price > 50).ToList();
            return View(model);
        }

        public ActionResult Read()
        { //將所有會員資料顯示於清單
            var db = new dbShoppingEntities();
            var members = db.Member.OrderByDescending(m => m.UserId).ToList();
            return View(members);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Member member)
        {
            var db = new dbShoppingEntities();
            db.Member.Add(member); //使用Add()方法新增資料後，必須使用SaveChanges()方法才會將變更的內容儲存，同理在做Edit與Delete的時候也一樣。
            db.SaveChanges();

            return RedirectToAction("Read");
        }

        public ActionResult Edit(int id)
        {
            var db = new dbShoppingEntities();
            var member = db.Member.Where(m => m.Id == id).FirstOrDefault(); //傳回序列的第一個項目；如果找不到任何項目，則傳回預設值。
            //也可寫成下面
            //var member = db.Member.FirstOrDefault(m => m.Id == id);
            return View(member);

            //LINQ查詢Member物件中符合該唯一id的資料
        }
        [HttpPost]
        public ActionResult Edit(Member member)
        {
            var db = new dbShoppingEntities();
            var memberData = db.Member.Where(m => m.Id == member.Id).FirstOrDefault();
            memberData.UserId = member.UserId;
            memberData.UserName = member.UserName;
            memberData.Password = member.Password;
            memberData.Email = member.Email;
            memberData.Address = member.Address;
            db.SaveChanges();
            //將修改送出後的物件屬性一一賦值回該筆資料，
            //最後記得使用SaveChanges()儲存變更

            return RedirectToAction("Read");
        }

        public ActionResult Delete(int id)
        {
            var db = new dbShoppingEntities();
            var member = db.Member.Where(m => m.Id == id).FirstOrDefault();
            return View(member);
        }

        [HttpPost]
        public ActionResult Delete(Member member)
        {
            var db = new dbShoppingEntities();
            var memberData = db.Member.Where(m => m.Id == member.Id).FirstOrDefault();
            db.Member.Remove(memberData);
            //使用Remove()來移除選擇的資料，並執行SaveChange()
            db.SaveChanges();

            return RedirectToAction("Read");
        }

    }
}