using maildb.Domain;
using maildb.HtmlAttribute;
using System.Collections.Generic;
using System.Web.Mvc;

namespace maildb.Models
{
    public class mailController : Controller
    {
        public int pageSize = 10;
        public int showPages = 15;
        public int count = 0;

        // отображение списка пользователей
        public ViewResult Index(string sortOrder, int page = 1)
        {
            string sortName = null;
            System.Web.Helpers.SortDirection sortDir = System.Web.Helpers.SortDirection.Ascending;
            sortOrder = Base.parseSortForDB(sortOrder, out sortName, out sortDir);
            MailRepository rep = new MailRepository();
            MailGrid users = new MailGrid {
                Mail = rep.List(sortName, sortDir, page, pageSize, out count),
                PagingInfo = new PagingInfo
                {
                    currentPage = page,
                    itemsPerPage = pageSize,
                    totalItems = count,
                    showPages = showPages
                },
                SortingInfo = new SortingInfo {
                    currentOrder = sortName,
                    currentDirection = sortDir
                }
            };
            return View(users);
        }

        [ReferrerHold]
        [HttpPost]
        public ActionResult Index(string onNewUser)
        {
            if (onNewUser != null)
            {
                TempData["referrer"] = ControllerContext.RouteData.Values["referrer"];
                return View("New", new MailModel(new MailClass()));
            }
            return View();
        }

        [ReferrerHold]
        public ActionResult New()
        {
            TempData["referrer"] = ControllerContext.RouteData.Values["referrer"];
            return View("New", new MailModel(new MailClass()));
        }

        [HttpPost]
        public ActionResult New(MailModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Mail == null) RedirectToAction("Index");
                MailRepository rep = new MailRepository();
                if (rep.AddMail(model.Mail)) TempData["message"] = string.Format("{0} has been added", model.Mail.title);
                else TempData["error"] = string.Format("{0} has not been added!", model.Mail.title);
                if (TempData["referrer"] != null) return Redirect(TempData["referrer"].ToString());
                return RedirectToAction("Index");
            }
            else
            {
                model = new MailModel(model.Mail); 
                return View(model);
            }
        }

        [ReferrerHold]
        public ActionResult Edit(int Idmail)
        {
            MailRepository rep = new MailRepository();
            MailClass mail = rep.FetchByID(Idmail);
            if (mail == null) return HttpNotFound();
            TempData["referrer"] = ControllerContext.RouteData.Values["referrer"];
            return View(new MailModel(mail));
        }

        [HttpPost]
        public ActionResult Edit(MailModel model, string action)
        {
            if (action == "Cancel")
            {
                if (TempData["referrer"] != null) return Redirect(TempData["referrer"].ToString());
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                if (model.Mail == null) RedirectToAction("Index");
                MailRepository rep = new MailRepository();
                if (action == "Save")
                {
                    if (rep.ChangeMail(model.Mail)) TempData["message"] = string.Format("{0} has been saved", model.Mail.title);
                    else TempData["error"] = string.Format("{0} has not been saved!", model.Mail.title);
                }
                if (action == "Remove")
                {
                    if (rep.RemoveUser(model.Mail)) TempData["message"] = string.Format("{0} has been removed", model.Mail.title);
                    else TempData["error"] = string.Format("{0} has not been removed!", model.Mail.title);
                }
                if (TempData["referrer"] != null) return Redirect(TempData["referrer"].ToString());
                return RedirectToAction("Index");
            }
            else
            {
                model = new MailModel(model.Mail);
                return View(model);
            }
        }

  
    }
}