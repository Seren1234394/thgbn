﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class TempInfo
    {
        public decimal Temp { get; set; }
        [JsonProperty("feels_like")]
        public decimal FeelsLike { get; set; }
        [JsonProperty("temp_min")]
        public decimal TempMin { get; set; }
        [JsonProperty("temp_max")]
        public decimal TempMax { get; set; }
    }
}
