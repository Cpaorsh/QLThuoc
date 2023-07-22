using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLThuoc.Class;
using QLThuoc.Models;

namespace QLThuoc.Controllers
{
    public class TongController : Controller
    {
        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();
        // GET: TongHop
        public ActionResult Index()
        {
            IList<XNT> tONGHOPHT = new List<XNT>();

            tONGHOPHT = db.THUOCs.Select(x => new XNT()
            {
                CTPXUATs = x.CTPXUATs,
                CTPNHAPs = x.CTPNHAPs,
                TONKHOes = x.TONKHOes,
                mathuoc = x.mathuoc,
                tenthuoc = x.tenthuoc,
                hoatchat = x.hoatchat,
                loaithuoc = x.loaithuoc,
                dvtinh = x.dvtinh,
                dongia = x.dongia
                //slxuat = x.mathuoc + (x.CTPXUAT.slxuat ?? 0),
                //slnhap = x.CTPNHAPs.slnhap,
                //for (int i = 0; i<n; i++)
                //{
                //    i = i + CTPNHAP.slnhap;
                //}
                //slton = x.TONKHOes.slton
            }).ToList();
            return View(tONGHOPHT.ToList());
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using QLThuoc.Class;
//using QLThuoc.Models;

//namespace QLThuoc.Controllers
//{
//    public class TongHopController : Controller
//    {
//        private aspnet_QLThuocEntities db = new aspnet_QLThuocEntities();
//        // GET: TongHop
//        public ActionResult Index(string search)
//        {
//            IList<TONGHOPHT> tONGHOPHT = new List<TONGHOPHT>();

//            tONGHOPHT = db.THUOCs.Select(x => new TONGHOPHT()
//            {
//                CTPXUATs = x.CTPXUATs,
//                CTPNHAPs = x.CTPNHAPs,
//                TONKHOes = x.TONKHOes,
//                mathuoc = x.mathuoc,
//                tenthuoc = x.tenthuoc,
//                hoatchat = x.hoatchat,
//                loaithuoc = x.loaithuoc,
//                noisx = x.noisx,
//                dvtinh = x.dvtinh,
//                dongia = x.dongia
//                //slxuat = x.mathuoc + (x.CTPXUAT.slxuat ?? 0),
//                //slnhap = x.CTPNHAPs.slnhap,
//                //for (int i = 0; i<n; i++)
//                //{
//                //    i = i + CTPNHAP.slnhap;
//                //}
//                //slton = x.TONKHOes.slton
//            }).ToList();



//            if (search == null)
//                return View(tONGHOPHT.ToList());
//            else
//                return View(tONGHOPHT.Where(s => s.tenthuoc.StartsWith(search)).ToList());

//        }
//    }
//}
