using strange.extensions.context.impl;

namespace TeddyGame {
    public class StartContextView : ContextView {
        void Awake() {
            this.context = new StartContext(this);
        }
    }
}