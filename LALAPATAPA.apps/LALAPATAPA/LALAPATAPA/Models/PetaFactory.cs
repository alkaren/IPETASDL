using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace LALAPATAPA.Models
{
    public static class PetaFactory
    {
        public static IList<Peta> peta { get; set; }

        static PetaFactory()
        {
            peta = new ObservableCollection<Peta>() {
                new Peta
                {
                    JenisPeta = "Arahan Komoditas"
                },
                new Peta
                {
                    JenisPeta = "Kesesuaian Lahan"
                },
                new Peta
                {
                    JenisPeta = "Tanah"
                },
                new Peta
                {
                    JenisPeta = "Lain-lain"
                }
            };
        }

    }
    public static class SkalaFactory
    {
        public static IList<SkalaPeta> skalaPeta { get; set; }

        static SkalaFactory()
        {
            skalaPeta = new ObservableCollection<SkalaPeta>() {
                new SkalaPeta
                {
                    Skala = "1 : 50.000"
                },
                new SkalaPeta
                {
                    Skala = "1 : 250.000"
                },
                new SkalaPeta
                {
                    Skala = "1 : 1.000.000"
                }
            };
        }

    }
}
