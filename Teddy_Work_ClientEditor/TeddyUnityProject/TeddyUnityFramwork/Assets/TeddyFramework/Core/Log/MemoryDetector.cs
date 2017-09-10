using UnityEngine;

namespace Teddy {
    public class MemoryDetector { // 内存检测器，目前只是输出Profiler信息
        private readonly static string _totalAllocMemroyFormation = "Alloc Memory : {0}M";
        private readonly static string _totalReservedMemoryFormation = "Reserved Memory : {0}M";
        private readonly static string _totalUnusedReservedMemoryFormation = "Unused Reserved: {0}M";
        private readonly static string _monoHeapFormation = "Mono Heap : {0}M";
        private readonly static string _monoUsedFormation = "Mono Used : {0}M";

        private float _byteToM = 0.000001f; // 字节到兆

        private Rect _allocMemoryRect;
        private Rect _reservedMemoryRect;
        private Rect _unusedReservedMemoryRect;
        private Rect _monoHeapRect;
        private Rect _monoUsedRect;

        private int _x = 0;
        private int _y = 0;
        private int _w = 0;
        private int _h = 0;

        public MemoryDetector(LogOutputConsole console) {
            _x = 10;
            _y = 70;
            _w = 200;
            _h = 20;

            _allocMemoryRect = new Rect(_x, _y, _w, _h);
            _reservedMemoryRect = new Rect(_x, _y + _h, _w, _h);
            _unusedReservedMemoryRect = new Rect(_x, _y + 2 * _h, _w, _h);
            _monoHeapRect = new Rect(_x, _y + 3 * _h, _w, _h);
            _monoUsedRect = new Rect(_x, _y + 4 * _h, _w, _h);

            console._onGUI += onGUI;
        }

        void onGUI() {
            GUI.Label(_allocMemoryRect,
                string.Format(_totalAllocMemroyFormation, UnityEngine.Profiling.Profiler.GetTotalAllocatedMemory() * _byteToM));
            GUI.Label(_reservedMemoryRect,
                string.Format(_totalReservedMemoryFormation, UnityEngine.Profiling.Profiler.GetTotalReservedMemory() * _byteToM));
            GUI.Label(_unusedReservedMemoryRect,
                string.Format(_totalUnusedReservedMemoryFormation, UnityEngine.Profiling.Profiler.GetTotalUnusedReservedMemory() * _byteToM));
            GUI.Label(_monoHeapRect,
                string.Format(_monoHeapFormation, UnityEngine.Profiling.Profiler.GetMonoHeapSize() * _byteToM));
            GUI.Label(_monoUsedRect,
                string.Format(_monoUsedFormation, UnityEngine.Profiling.Profiler.GetMonoUsedSize() * _byteToM));
        }
    }
}