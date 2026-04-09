using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Control
{
    /// <summary>
    /// Retrieve the value from the resource page
    /// -----------------------------------------
    /// C.C.Yeon    25 April 2011   initial Version
    /// </summary>
    public class Resource
    {
        public static string RetrieveValue(string resource, string field)
        {
            var manager = new ResourceManager(resource, Assembly.GetExecutingAssembly());
            return manager.GetString(field);
        }
    }
}