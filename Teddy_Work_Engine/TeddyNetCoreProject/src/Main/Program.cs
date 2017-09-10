using System;
using TeddyNetCore_EngineCore;
using TeddyNetCore_EngineData;
using TeddyNetCore_EngineEnum;

namespace TeddyNet_EngineClient {
    class Program {
        static void Main(string[] args) {
            DataFile_PlotSection a = new DataFile_PlotSection();

            DataBase_PlotStep b = new DataBase_PlotStep();
            DataBase_PlotStep c = new DataBase_PlotStep();
            DataBase_PlotStep d = new DataBase_PlotStep();
            DataBase_PlotStep e = new DataBase_PlotStep();
            DataBase_PlotStep f = new DataBase_PlotStep();

            DataBase_Plot aa = new DataBase_Plot();
            DataBase_PlotUI bb = new DataBase_PlotUI();
            DataBase_PlotSound cc = new DataBase_PlotSound();

            a._plotStep._plotList.Add(aa);
            a._plotStep._plotList.Add(bb);
            a._plotStep._plotList.Add(cc);

            a._plotStep._plotStepDict.Add("_plotStep_0", b);
            a._plotStep._plotStepDict.Add("_plotStep_1", c);

            b._plotStepDict.Add("_plotStep_0", d);
            d._plotStepDict.Add("_plotStep_0", e);
            e._plotStepDict.Add("_plotStep_0", f);

            JsonController _jsonController = new JsonController();
            string str = _jsonController.serializeObject(a);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(str);
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine(ResSubDir.Config_Base.ToString());
            Console.WriteLine(EnumUtil<ResSubDir>.getDisplayValue(ResSubDir.Config_Base));
            Console.Read();
        }
    }
}
