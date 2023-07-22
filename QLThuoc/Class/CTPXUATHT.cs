using QLThuoc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QLThuoc.Class
{
    public class CTPXUATHT
    {
        public int id { get; set; }
        public int maphieu { get; set; }
        public int mathuoc { get; set; }
        [Display(Name = "Số lượng xuất")]
        public int slxuat { get; set; }
        [Display(Name = "Thành tiền")]
        public int thanhtien { get; set; }

        public virtual PXUAT PXUAT { get; set; }
        public virtual THUOC THUOC { get; set; }
    }
}