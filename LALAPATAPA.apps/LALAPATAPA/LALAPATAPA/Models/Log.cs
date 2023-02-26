using System;
using System.Collections.Generic;

namespace LALAPATAPA.Models
{
    public partial class Log
    {
        public int IdLog { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public DateTime? Time { get; set; }
    }
}
