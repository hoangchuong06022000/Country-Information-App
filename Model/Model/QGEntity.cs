using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Model.Model
{
    public partial class QGEntity : DbContext
    {
        public QGEntity()
            : base("name=QGEntity")
        {
        }

        public virtual DbSet<THONGTIN> THONGTINs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
