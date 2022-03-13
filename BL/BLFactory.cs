using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public class BLFactory
    {
        private static IBL bl;

        public static IBL getBL_imp()
        {
            if (bl == null)
                bl = new BL_imp();
            return bl;
        }
    }
}
