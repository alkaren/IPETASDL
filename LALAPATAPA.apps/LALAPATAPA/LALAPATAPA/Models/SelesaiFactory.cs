using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class SelesaiFactory
    {
        public static IList<Selesai> Selesais { get; set; }

        static SelesaiFactory()
        {
            Selesais = new ObservableCollection<Selesai>() {
                new Selesai
                {
                    Status = "MENUNGGU PEMBAYARAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "DIBATALKAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "VALIDASI PEMBAYARAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "SUDAH DIBAYAR",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "MENUNGGU PEMBAYARAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "DIBATALKAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "VALIDASI PEMBAYARAN",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                }
            };
        }
    }
}
