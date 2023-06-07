using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    internal interface IItemPopularitiesRepository
    {
        Task<ItemPopularities> GetItemPopularities(int heroId);


    }
}
