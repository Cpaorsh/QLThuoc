using QLThuoc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QLThuoc.Class
{
    public class XNT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public XNT()
        {
            this.CTPNHAPs = new HashSet<CTPNHAP>();
            this.CTPXUATs = new HashSet<CTPXUAT>();
            this.TONKHOes = new HashSet<TONKHO>();
        }

        public int mathuoc { get; set; }
        [Display(Name = "Tên thuốc")]
        public string tenthuoc { get; set; }

        [Display(Name = "Hoạt chất")]
        public string hoatchat { get; set; }

        [Display(Name = "Loại thuốc")]
        public string loaithuoc { get; set; }

        [Display(Name = "Nơi sản xuất")]
        public string noisx { get; set; }
        [Display(Name = "Đơn vị tính")]
        public string dvtinh { get; set; }
        [Display(Name = "Đơn giá")]
        public int dongia { get; set; }
        [Display(Name = "Số lượng xuất")]
        public int slxuat { get; set; }
        [Display(Name = "Số lượng nhập")]
        public int slnhap { get; set; }
        [Display(Name = "Số lượng tồn")]
        public int slton { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPNHAP> CTPNHAPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPXUAT> CTPXUATs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TONKHO> TONKHOes { get; set; }
    }
}