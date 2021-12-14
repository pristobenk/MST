using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MitraSolusiTelematika.Helpers
{
    public class AjaxFormResponseJson
    {

        public bool success;

        public string title { get; set; }

        public string message { get; set; }

        public bool enforce;

        public string url { get; set; }
    }
}
