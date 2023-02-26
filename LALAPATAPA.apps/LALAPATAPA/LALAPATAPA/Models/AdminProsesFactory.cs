using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class AdminProsesFactory
    {
        public static IList<Proses> Proses { get; set; }

        static AdminProsesFactory()
        {
            Proses = new ObservableCollection<Proses>() {
                new Proses
                {
                    Status = "TERSEDIA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "TIDAK TERSEDIA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "MENUNGGU KONFIRMASI PEMESANAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "PEMESAN SETUJU",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "TERSEDIA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "TIDAK TERSEDIA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "MENUNGGU KONFIRMASI PEMESANAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                }
            };
        }

    }
}
