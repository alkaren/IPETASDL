using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class ProsesFactory
    {
        public static IList<Proses> Proses { get; set; }

        static ProsesFactory()
        {
            Proses = new ObservableCollection<Proses>() {
                new Proses
                {
                    Status = "MENUNGGU PERSETUJUAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "DITOLAK",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "DISETUJUI",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "MENUNGGU PERSETUJUAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "DITOLAK",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "DISETUJUI",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                }
            };
        }

    }
}
