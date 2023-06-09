﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolDevProject.WPF.Model;

namespace ToolDevProject.WPF.Repository
{
    public interface IHeroesRepository
    {
        Task<List<BaseHero>> GetHeroes();
        Task<List<string>> GetAttributes();
        Task<List<string>> GetRoles();
        Task<List<BaseHero>> GetHeroes(string attribute, string role, string nameContains);
    }
}
