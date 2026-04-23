using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class BedenManager
    {
        public static DataTable BedenListeleBL()
        {
            return BedenDAL.BedenListele();
        }
    }
}
