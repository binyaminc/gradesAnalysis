using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class DalFactory
    {
        private static Idal dal;

        public static Idal getDal_imp()
        {
            if (dal == null)
                dal = new Dal_XML_imp();
            return dal;
        }
    }
}
