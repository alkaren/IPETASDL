using System;
using System.Collections.Generic;
using System.Text;

namespace LALAPATAPA.Models
{
    public class Proses
    {
        public string CodeTrans { get; set; }

        public string Status { get; set; }
        public string Date { get; set; }
    }
    public class Selesai
    {
        public string CodeTrans { get; set; }

        public string Status { get; set; }
        public string Date { get; set; }
    }
    public class RincianHarga
    {
        public string NamaPeta { get; set; }

        public string HargaPeta { get; set; }
    }
    public class Peta
    {
        public string JenisPeta { get; set; }
    }
    public class SkalaPeta
    {
        public string Skala { get; set; }
    }
}
