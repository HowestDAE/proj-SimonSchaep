﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolDevProject.WPF.Model
{
    internal class DotaItem
    {
        [JsonProperty(PropertyName = "dname")]
        public string Name { get; set; }

        private string _imageUrl;
        [JsonProperty(PropertyName = "img")]
        public string ImageUrl
        {
            get => "http://cdn.dota2.com" + _imageUrl;
            set => _imageUrl = value;
        }
    }
}
