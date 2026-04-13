using System;
using Microsoft.Extensions.Configuration;

namespace Library.Root.Other
{
    /// <summary>
    /// Handler the Common Business Logic Function
    /// -------------------------------------------
    /// C.C.Yeon    25 April 2011   Initial Version
    /// </summary>
    public abstract class BusinessLogicBase
    {
        private static IConfiguration _configuration;
        private static int _maxQuantityPerPage = 10; // default fallback

        /// <summary>
        /// Initialise from ASP.NET Core configuration (call once at startup).
        /// </summary>
        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            var value = _configuration["AppSettings:MaxRowPerPage"];
            if (!string.IsNullOrEmpty(value))
                _maxQuantityPerPage = Convert.ToInt32(value);
        }

        /// <summary>
        /// Max Quantity per Page
        /// </summary>
        public static int MaxQuantityPerPage
        {
            get
            {
                return _maxQuantityPerPage;
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
