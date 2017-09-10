using strange.extensions.context.impl;

namespace Game {
    public class DemoContextView : ContextView {
        void Awake() {
            this.context = new DemoContext(this);
        }
    }
}