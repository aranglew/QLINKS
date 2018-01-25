using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QFSLINKS.Models;
using QFSLINKS.Data;
using QFSLINKS.Services;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Data;
using QFSLINKS.Models.MenuViewModels;

namespace QFSLINKS.Controllers
{
    public class HomeController : Controller
    {
        private readonly QfslinksContext _context;

        public HomeController(QfslinksContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var links = from s in _context.SDR_QFS_DataU
                        where s.TopicID == 0 && !s.UserName.Contains("Imported")
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.Data.Contains(searchString)
                                       || s.UserName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "desc_desc":
                    links = links.OrderByDescending(s => s.UserName);
                    break;
                case "Date":
                    links = links.OrderBy(s => s.Data);
                    break;
                case "date_desc":
                    links = links.OrderByDescending(s => s.SortOrder);
                    break;
                default:
                    links = links.OrderBy(s => s.UserName);
                    break;
            }
            int pageSize = 3;
            var TotalItems = links.Count();
            ViewBag.TotalItems = TotalItems;
            var TotalPages = Math.Ceiling((decimal)TotalItems / (decimal)pageSize);
            ViewBag.TotalPages = TotalPages;

            return View("UserList", await PaginatedList<SDR_QFS_Datau>.CreateAsync(links, page ?? 1, pageSize));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public List<SDR_QFS_Division> GetDivisionList()
        {
            var divisions = from d in _context.SDR_QFS_Division
                            select d;
            return divisions.ToList();

        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult AddUserAjax(DTO_user New_user)
        {
            Debug.WriteLine(Request.Body);
            if (!this.ModelState.IsValid)
                return (ActionResult)this.View("Index", (object)New_user);

            SDR_QFS_Datau sdrQfsDataU = new SDR_QFS_Datau();
            if (!string.IsNullOrEmpty(New_user.UserName))
            {
                sdrQfsDataU.UserName = New_user.UserName.Trim();
                sdrQfsDataU.Data = New_user.Data;
                sdrQfsDataU.Location = New_user.Location;
                sdrQfsDataU.UserInitials = New_user.UserInitials;
                sdrQfsDataU.TopicID = 0;
                sdrQfsDataU.Division = New_user.Division;
                sdrQfsDataU.UserEmail = New_user.UserEmail;
                sdrQfsDataU.UserPhone = New_user.UserPhone;
                sdrQfsDataU.VimsVisible = (New_user.VimsVisible == "true" ? 1 : 0);
                sdrQfsDataU.VimsDelegate = (New_user.VimsDelegate == "true" ? 1 : 0);
                sdrQfsDataU.VimsAccess = New_user.VimsAccess;
                sdrQfsDataU.Access = New_user.Access;
                sdrQfsDataU.AccessA = New_user.AccessA;
                this._context.SDR_QFS_DataU.Add(sdrQfsDataU);
                try
                {
                    this._context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return RedirectToAction("Index", new{sortOrder = "",currentFilter = New_user.UserName,searchString = New_user.UserName,New_user = New_user});
        }

        public JsonResult DetailsbyUser(string UserName)
        {
            ViewBag.CurrentUser = UserName;
            ViewBag.CurrentFilter = UserName;

            if (string.IsNullOrEmpty(UserName))
                return this.Json((object)null);
            var userdetails = Json(_context.SDR_QFS_DataU.Where(p => p.TopicID == 0 && p.UserName == UserName));
            return userdetails;
        }

        public JsonResult GetUserList(string division)
        {
            var users = from a in _context.SDR_QFS_DataU
                        where (a.TopicID <= 0
                        && !a.UserName.Contains("Imported")
                        && a.Division == division)
                        orderby a.UserName
                        select a.UserName;
            return Json(users);
        }

        [HttpPost]
        public ActionResult EditUser(DTO_user Edit_user)
        {
            if (!this.ModelState.IsValid)
                return View("Index", Edit_user);
            if (!string.IsNullOrEmpty(Edit_user.UserName))
            {
                SDR_QFS_Datau sdrQfsDataU = this._context.SDR_QFS_DataU.Find(Edit_user.TopicUserID);
                sdrQfsDataU.UserName = Edit_user.UserName.Trim();
                sdrQfsDataU.Data = Edit_user.Data;
                sdrQfsDataU.Location = Edit_user.Location;
                sdrQfsDataU.Division = Edit_user.Division;
                sdrQfsDataU.UserInitials = Edit_user.UserInitials;
                sdrQfsDataU.UserEmail = Edit_user.UserEmail;
                sdrQfsDataU.Access = Edit_user.Access;
                sdrQfsDataU.AccessA = Edit_user.AccessA;
                sdrQfsDataU.UserPhone = Edit_user.UserPhone;
                sdrQfsDataU.VimsVisible = (Edit_user.VimsVisible == "True" ? 1 : 0);
                sdrQfsDataU.VimsDelegate = (Edit_user.VimsDelegate == "True" ? 1 : 0);
                sdrQfsDataU.VimsAccess = Edit_user.VimsAccess;
                this._context.SaveChanges();
            }
            return RedirectToAction("Index", new{sortOrder = "",currentFilter = Edit_user.UserName,searchString = Edit_user.UserName});
        }

        public ActionResult Detailsview(string SearchParam, int CurrentPage, string CurrentFilter)
        {
            IEnumerable<DTO_menubyuser> list;
            if (string.IsNullOrEmpty(SearchParam))
                return (ActionResult)this.View("Details");
            else
            {

                SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@UserNameFilter", SearchParam) };
                list = RDFacadeExtensions.GetModelFromQuery<DTO_menubyuser>(_context, "Exec SDR_QFS_MenuByUser @UserNameFilter", parms);
                ViewBag.CurrentUser = SearchParam;
                ViewBag.page = CurrentPage;
                ViewBag.CurrentFilter = CurrentFilter;
            }
            return View("Details", list);
        }

        public JsonResult GetAllLinks()
        {
            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@UserNameFilter", "") };
            var list = Json(RDFacadeExtensions.GetModelFromQuery<DTO_menubyuser>(_context, "Exec SDR_QFS_AllLinks", parms));
            return list;
        }

        [HttpPost]
        public ActionResult AddUserLinkAjax(DTO_menubyuser DTO_Link)
        {
            SDR_QFS_Datau sdrQfsDataU = new SDR_QFS_Datau();
            if (!string.IsNullOrEmpty(DTO_Link.Username))
            {
                sdrQfsDataU.UserName = DTO_Link.Username;
                sdrQfsDataU.TopicID = DTO_Link.TopicId;
                sdrQfsDataU.Data = DTO_Link.Description;
                this._context.SDR_QFS_DataU.Add(sdrQfsDataU);
                try
                {
                    this._context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return (ActionResult)this.RedirectToAction("Detailsview", (object)new { SearchParam = "", CurrentPage = 1, currentFilter = DTO_Link.Username });
        }

        public async Task<IActionResult> MenuItem(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            ViewBag.LinkType = _context.SDR_QFS_LinkType.ToList();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var source = from s in _context.SDR_QFS_Data
                         where s.TopicID != 0
                         select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                source = source.Where(s => s.Data.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }


            ViewBag.CurrentFilter = currentFilter;
            ViewBag.NameSortParm = sortOrder;
            ViewBag.DataSortParm = sortOrder;

            int enteredNumber;
            bool flag = int.TryParse(searchString, out enteredNumber);

            IQueryable<SDR_QFS_Data> queryable;
            if (!(sortOrder == "topicid_desc"))
            {
                if (!(sortOrder == "fn_desc"))
                {
                    if (sortOrder == "fn_asc")
                        queryable = (IQueryable<SDR_QFS_Data>)source.OrderBy<SDR_QFS_Data, string>((a => a.Data));
                    else
                        queryable = (IQueryable<SDR_QFS_Data>)source.OrderBy<SDR_QFS_Data, int?>((a => a.TopicID));
                }
                else
                    queryable = (IQueryable<SDR_QFS_Data>)source.OrderByDescending<SDR_QFS_Data, string>((a => a.Data));
            }
            else
                queryable = (IQueryable<SDR_QFS_Data>)source.OrderByDescending<SDR_QFS_Data, int?>((a => a.TopicID));
            int num1 = 20;
            int num2 = page ?? 1;
            return View("MenuItem", await PaginatedList<SDR_QFS_Data>.CreateAsync(queryable, num2, num1));

        }

        public JsonResult GetLinkById(int TopicUserId, string username)
        {

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@UserNameFilter", username) };
            var list = RDFacadeExtensions.GetModelFromQuery<DTO_menubyuser>(_context, "Exec dbo.SDR_QFS_MenuByUser @UserNameFilter", parms);
            list = list.Where(p => p.TopicUserID == TopicUserId);

            return Json(list);
        }

        [HttpPost]
        public ActionResult DeleteUserLink(DTO_menubyuser data)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View("Detailsview", (object)data);

            ViewBag.Message = "Delete User Link";
            if (data.TopicUserID <= 0)
                return (ActionResult)this.View("Detailsview", (object)data);
            SDR_QFS_Datau sdrQfsDataU = this._context.SDR_QFS_DataU.Find(data.TopicUserID);
            _context.SDR_QFS_DataU.Remove(sdrQfsDataU);
            _context.SaveChanges();
            return RedirectToAction("Detailsview", (object)new {SearchParam = sdrQfsDataU.UserName,CurrentPage = 1,currentFilter = sdrQfsDataU.UserName});
        }

        [HttpPost]
        public ActionResult EditUserLink(DTO_menubyuser data)
        {
            SDR_QFS_Datau sdrQfsDataU2 = this._context.SDR_QFS_DataU.Find(data.TopicUserID);
            if (!string.IsNullOrEmpty(data.Username))
            {
                sdrQfsDataU2.UserName = data.Username;
                sdrQfsDataU2.SortOrder = (decimal)data.SortOrder;
                sdrQfsDataU2.TopicUserID = data.TopicUserID;
                sdrQfsDataU2.Data = data.DataU;
                sdrQfsDataU2.TopicID = data.TopicId;
                try
                {
                    this._context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return RedirectToAction("Detailsview", new { SearchParam = data.Username, CurrentPage = 1, currentFilter = data.Username });
        }

        [HttpPost]
        public ActionResult CopyUserLinks(string currentuser, string copyTo)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View("Index", currentuser);
            else
            {
                if (!string.IsNullOrEmpty(currentuser) && !string.IsNullOrEmpty(copyTo))
                {
                    SqlParameter parm1 = new SqlParameter("@copyfrom", currentuser);
                    SqlParameter parm2 = new SqlParameter("@copyto", copyTo);
                    SqlParameter[] parms = { parm1, parm2 };

                    var list = Json(RDFacadeExtensions.GetModelFromQuery<SDR_QFS_Data>(_context, "exec dbo.SDR_QFS_CopyLinks @copyfrom, @copyto", parms));
                }
            }
            //return (ActionResult)this.RedirectToAction("UserList", new { sortOrder = "", currentFilter = copyTo, searchString = copyTo });
            return RedirectToAction("Index", new { sortOrder = "", currentFilter = copyTo, searchString = copyTo });
        }

        [HttpPost]
        public JsonResult GetSingleLink(SDR_QFS_Data data)
        {
            if (data.TID <= 0)
                return this.Json(null);

            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@TopicFilter", data.TID) };
            var list = RDFacadeExtensions.GetModelFromQuery<SDR_QFS_Data>(_context, "Exec dbo.SDR_QFS_Topic @TopicFilter", parms);
            return Json(list);
        }

        [HttpPost]
        public ActionResult EditDataLink(SDR_QFS_Data data)
        {
            if (!ModelState.IsValid)
                return (ActionResult)this.View("MenuItem", (object)data);
            ViewBag.Message = "EditDataLink";

            if (!string.IsNullOrEmpty(data.Data))
            {
                SDR_QFS_Data sdrQfsData = this._context.SDR_QFS_Data.Find(data.TID);
                sdrQfsData.TID = data.TID;
                sdrQfsData.GroupID = data.GroupID;
                sdrQfsData.TopicID = data.TopicID;
                sdrQfsData.Topic = data.Topic;
                sdrQfsData.Type = data.Type;
                sdrQfsData.SortOrder = data.SortOrder;
                sdrQfsData.Description = data.Description;
                sdrQfsData.Data = data.Data;
                sdrQfsData.Location = data.Location;
                sdrQfsData.Format = data.Format;
                sdrQfsData.Detail = data.Detail;
                this._context.SaveChanges();
            }
            return RedirectToAction("MenuItem", new { sortOrder = "", currentFilter = data.Description, searchString = data.Description, page = 1 });
        }
        [HttpPost]
        public ActionResult AddItem(DTO_MenuItem newMenuitem, string Hsearch, string Hsortname, string Hsortdata, int Hpage)
        {
            SDR_QFS_Data sdrQfsData1 = new SDR_QFS_Data();
            if (!string.IsNullOrEmpty(newMenuitem.TopicID.ToString()))
            {
                sdrQfsData1.TopicID = newMenuitem.TopicID;
                sdrQfsData1.Description = newMenuitem.Description;
                sdrQfsData1.Type = newMenuitem.Type;
                sdrQfsData1.Location = newMenuitem.Location;
                sdrQfsData1.Data = newMenuitem.Data;
                sdrQfsData1.Format = newMenuitem.Format;
                sdrQfsData1.Detail = newMenuitem.Detail;
                SDR_QFS_Data sdrQfsData2 = sdrQfsData1;
                int? sortOrder = newMenuitem.sortOrder;
                Decimal? nullable = sortOrder.HasValue ? new Decimal?((Decimal)sortOrder.GetValueOrDefault()) : new Decimal?();
                sdrQfsData2.SortOrder = nullable;
                this._context.SDR_QFS_Data.Add(sdrQfsData1);
                try
                {
                    this._context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return RedirectToAction("MenuItem", new { sortOrder = Hsortname, currentFilter = newMenuitem.Description });
        }
     
        public ActionResult EditUser(string SearchParam, int CurrentPage, string CurrentFilter)
        {
            if (string.IsNullOrEmpty(SearchParam))
                return (ActionResult)this.View();

            ViewBag.Message = "Edit User";
            ViewBag.CurrentPage = "";
            ViewBag.CurrentFilter =  "";


            SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@UserNameFilter", SearchParam) };
            var list = Json(RDFacadeExtensions.GetModelFromQuery<DTO_user>(_context, "Exec SDR_QFS_userByUsername @UserNameFilter", parms));
            return  View("EditUser", list);
        }

        public JsonResult Details(string SearchParam, int CurrentPage, string CurrentFilter)
        {
            if (string.IsNullOrEmpty(SearchParam))
                return this.Json(null);
            else
            {
                ViewBag.Message = "Details";
                ViewBag.Page = CurrentPage;
                ViewBag.CurrentUser = CurrentFilter;
                ViewBag.SearchParam = SearchParam;
               SqlParameter[] parms = new SqlParameter[] { new SqlParameter("@UserNameFilter", SearchParam) };
                var list = RDFacadeExtensions.GetModelFromQuery<DTO_user>(_context, "Exec dbo.SDR_QFS_MenuSP @UserNameFilter", parms);
                ViewBag.TotalRecords = list.Count();
                return Json(list);
            }
        }
    }
}


