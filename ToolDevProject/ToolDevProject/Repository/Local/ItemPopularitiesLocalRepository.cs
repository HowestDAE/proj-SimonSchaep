using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository.Local
{
    internal class ItemPopularitiesLocalRepository : IItemPopularitiesRepository
    {
        public Task<ItemPopularities> GetItemPopularities(int heroId)
        {
            throw new NotImplementedException();
        }
    }
}
