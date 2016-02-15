using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

using QLEX;

namespace QLEX
{
    /// <summary>
    /// Enum class
    /// </summary>
    public class QLEnum
    {
        public static Dictionary<string, string> EnumDictionary = new Dictionary<string, string>()
        {
            {"MF", "ModifiedFollowing" },
            {"Modified Following", "ModifiedFollowing" },
            {"F", "Following" },
            {"P", "Preceding" },
            {"MP", "ModifiedPreceding" },
            {"Modified Preceding", "ModifiedPreceding" }
        };
    } // end of class QLEnum
}
