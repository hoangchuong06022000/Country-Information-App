namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("THONGTIN")]
    public partial class THONGTIN
    {
        [Key]
        [StringLength(40)]
        public string TENQUOCGIA { get; set; }

        [StringLength(40)]
        public string THUDO { get; set; }

        [StringLength(30)]
        public string NGONNGU { get; set; }

        [StringLength(30)]
        public string CHAULUC { get; set; }

        [StringLength(30)]
        public string TIENTE { get; set; }

        [StringLength(30)]
        public string DIENTICH { get; set; }

        [StringLength(30)]
        public string DANSO { get; set; }
    }
}
