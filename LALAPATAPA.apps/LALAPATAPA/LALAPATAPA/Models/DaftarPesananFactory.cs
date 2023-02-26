using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class DaftarPesananFactory
    {
        public static IList<Selesai> Selesais { get; set; }

        static DaftarPesananFactory()
        {
            Selesais = new ObservableCollection<Selesai>() {
                new Selesai
                {
                    Status = "MENUNGGU INFO HARGA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "BELUM DIPROSES",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "MENUNGGU INFO HARGA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Selesai
                {
                    Status = "BELUM DIPROSES",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                }
            };
        }
    }
}
