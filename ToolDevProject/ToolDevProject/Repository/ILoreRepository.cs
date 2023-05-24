using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Repository
{
    internal interface ILoreRepository
    {
        Task LoadLore();

        string GetLore(string heroName);
    }
}
