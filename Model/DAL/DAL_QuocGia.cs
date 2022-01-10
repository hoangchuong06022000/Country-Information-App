using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Model;

namespace Model.DAL
{
    public class DAL_QuocGia
    {
        public List<THONGTIN> GetAll()
        {
            QGEntity db = new QGEntity();
            return db.THONGTINs.ToList();
        }

        public bool Insert(THONGTIN thongTin)
        {
            try
            {
                QGEntity db = new QGEntity();
                if(db.THONGTINs.ToList().Contains(thongTin))
                {
                    return false;
                }
                else
                {
                    db.THONGTINs.Add(thongTin);
                    db.SaveChanges();
                }
                
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
