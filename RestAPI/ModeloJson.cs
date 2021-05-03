using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPI
{
    class ModeloJson
    {
        public int? id { get; set; }
        public int userId { get; set; }
        
        public String title { get; set; }
        public bool completed { get; set; }

    }

    class ChuckNoris
    {
        public String icon_url { get; set; }
        public String id { get; set; }
        public String url { get; set; }
        public String value { get; set; }

    }
}
