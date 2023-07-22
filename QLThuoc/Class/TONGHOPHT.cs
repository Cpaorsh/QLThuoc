using QLThuoc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace QLThuoc.Class
{
    public class TONGHOPHT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TONGHOPHT()
        {
            this.CTPNHAPs = new HashSet<CTPNHAP>();
            this.CTPXUATs = new HashSet<CTPXUAT>();
            this.TONKHOes = new HashSet<TONKHO>();
        }

        public int mathuoc { get; set; }
        [Display(Name = "Số lượng tồn kì trước")]
        public int sldau { get; set; }
        [Display(Name = "Số lượng nhập")]
        public int slnhap { get; set; }
        [Display(Name = "Số lượng xuất")]
        public int slxuat { get; set; }
        [Display(Name = "Số lượng hao hụt")]
        public int slhaohut { get; set; }
        [Display(Name = "Số lượng cuối")]
        public int slcuoi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPNHAP> CTPNHAPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTPXUAT> CTPXUATs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TONKHO> TONKHOes { get; set; }
        public virtual THUOC THUOC { get; set; }
    }
}