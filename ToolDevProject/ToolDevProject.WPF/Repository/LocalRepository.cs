using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    internal class LocalRepository
    {
        private static List<Hero> _heroStats;

        public static List<Hero> GetHeroStats()
        {
            

            return _heroStats;
        }
    }
}
