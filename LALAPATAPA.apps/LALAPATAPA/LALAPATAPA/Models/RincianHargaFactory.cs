using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class RincianHargaFactory
    {
        public static IList<RincianHarga> Rincians { get; set; }

        static RincianHargaFactory()
        {
            Rincians = new ObservableCollection<RincianHarga>() {
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungbadak",
                    HargaPeta = "Rp. 3.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Mekarwangi",
                    HargaPeta = "Rp. 25.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungwaringin",
                    HargaPeta = "Rp. 1.500.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungbadak",
                    HargaPeta = "Rp. 3.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Mekarwangi",
                    HargaPeta = "Rp. 25.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungwaringin",
                    HargaPeta = "Rp. 1.500.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungbadak",
                    HargaPeta = "Rp. 3.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Mekarwangi",
                    HargaPeta = "Rp. 25.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungwaringin",
                    HargaPeta = "Rp. 1.500.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungbadak",
                    HargaPeta = "Rp. 3.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Mekarwangi",
                    HargaPeta = "Rp. 25.000.000"
                },
                new RincianHarga
                {
                    NamaPeta = "Peta Kedungwaringin",
                    HargaPeta = "Rp. 1.500.000"
                }
            };
        }

    }
}
