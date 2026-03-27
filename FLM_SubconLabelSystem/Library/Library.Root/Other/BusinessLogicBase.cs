using System;
using System.Configuration;

namespace Library.Root.Other
{
    /// <summary>
    /// Handler the Common Business Logic Function
    /// -------------------------------------------
    /// C.C.Yeon    25 April 2011   Initial Version
    /// </summary>
    public abstract class BusinessLogicBase
    {
        /// <summary>
        /// Max Quantity per Page
        /// </summary>
        public static int MaxQuantityPerPage
        {
            get
            {
                return Convert.ToInt32(ConfigurationManager.AppSettings["MaxRowPerPage"].ToString());
            }
        }

        /// <summary>
        /// Generate and Calculate the Number
        /// </summary>
        public static int FromRowNo(int PageNo)
        {
            if (PageNo == 1)
            {
                return 1;
            }
            else
            {
                return ((PageNo - 1) * MaxQuantityPerPage) + 1;
            }
        }

        /// <summary>
        /// Generate and Calculate the Number
        /// </summary>
        public static int ToRowNo(int PageNo)
        {
            if (PageNo == 1)
            {
                return MaxQuantityPerPage;
            }
            else
            {
                return ((PageNo - 1) * MaxQuantityPerPage) + MaxQuantityPerPage;
            }
        }
    }
}
