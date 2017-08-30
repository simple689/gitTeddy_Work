using System;
using TeddyNetCore_EngineEnum;

namespace TeddyNet_EngineClient {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine(ResSubDir.Config_Base.ToString());
            Console.WriteLine(EnumUtil<ResSubDir>.getDisplayValue(ResSubDir.Config_Base));
        }
    }
}
