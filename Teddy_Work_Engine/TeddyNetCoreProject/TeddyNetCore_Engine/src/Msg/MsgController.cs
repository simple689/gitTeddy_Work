using System;
using System.Collections.Generic;

namespace TeddyNetCore_Engine {
    public class MsgController {
        EngineBase _controller = null;
        
        public void init(EngineBase controller) {
            _controller = controller;
        }
    }
}
