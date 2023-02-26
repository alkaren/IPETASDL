using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class PesananLIAFactory
    {
        public static IList<Proses> Proses { get; set; }

        static PesananLIAFactory()
        {
            Proses = new ObservableCollection<Proses>() {
                new Proses
                {
                    Status = "BELUM DIPROSES",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "MENUNGGU INFO HARGA",
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
                    Status = "BELUM DIPROSES",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "MENUNGGU INFO HARGA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                },
                new Proses
                {
                    Status = "TERSEDIA",
                    Date = "14 Mei 2019",
                    CodeTrans = "#97523"
                }
            };
        }

    }
}
