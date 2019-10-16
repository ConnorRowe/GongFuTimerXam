using System;
using System.Collections.Generic;
using System.Text;

namespace GongFuTimer.Models
{
    public class Tea
    {
        public enum TeaType
        {
            White,
            Green,
            Matcha,
            Yellow,
            Oolong,
            Black,
            RawPuerh,
            Ripened,
            Tisanes,
            MedicinalHerbs
        };

        public String Name { get; set; }
        public String AltName { get; set; }
        public TeaType Type { get; set; }
        public ushort BaseSeconds { get; set; }
        public ushort PlusSeconds { get; set; }
        public ushort Temp { get; set; }
        public ushort MaxInfusions { get; set; }

        public Tea()
        {
            this.Name = "";
            this.AltName = "";
            this.Type = TeaType.White;
            this.BaseSeconds = 0;
            this.PlusSeconds = 0;
            this.Temp = 0;
            this.MaxInfusions = 0;
        }

        public Tea(String name, String altname, TeaType type, ushort baseseconds, ushort plusseconds, ushort temp, ushort maxinfusions)
        {
            this.Name = name;
            this.AltName = altname;
            this.Type = type;
            this.BaseSeconds = baseseconds;
            this.PlusSeconds = plusseconds;
            this.Temp = temp;
            this.MaxInfusions = maxinfusions;
        }

        public static List<Tea> getTestTeaList()
        {
            List<Tea> test = new List<Tea>();
            test.Add(new Tea("Souchong Liquor", "Tong Mu Zhengshan Xiaozhong", TeaType.Black, 15, 5, 90, 8));
            test.Add(new Tea("Other One", "peepee", TeaType.Yellow, 15, 5, 90, 8));

            return test;
        }
    }
}
