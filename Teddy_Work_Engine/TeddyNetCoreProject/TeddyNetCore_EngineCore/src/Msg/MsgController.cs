using System;
using System.Collections.Generic;

namespace TeddyNetCore_EngineCore {
    public class MsgController {
        EngineBase _controller = null;
        
        public void init(EngineBase controller) {
            _controller = controller;
        }
    }
}
