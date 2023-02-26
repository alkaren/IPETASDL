using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class ProsesLIAFactory
    {
        public static IList<Proses> Proses { get; set; }

        static ProsesLIAFactory()
        {
            Proses = new ObservableCollection<Proses>() {
                new Proses
                {
                    Status = "MENUNGGU PEMBAYARAN SELESAI",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "SELESAI",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "DIKIRIM VIA KURIR",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "DIAMBIL DILOKASI",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "MENUNGGU PEMBAYARAN SELESAI",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "SELESAI",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "DIKIRIM VIA KURIR",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                }
            };
        }

    }
}
