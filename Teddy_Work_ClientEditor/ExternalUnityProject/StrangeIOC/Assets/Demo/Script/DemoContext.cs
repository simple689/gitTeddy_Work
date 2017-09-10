using UnityEngine;
using TeddyFramwork;
using strange.extensions.signal.impl;

namespace Game {
    public class DemoContext : SignalContext {
        public DemoContext(MonoBehaviour contextView) : base(contextView) {
        }

        protected override void mapBindings() {
            base.mapBindings();
            // we bind a command to StartSignal since it is invoked by SignalContext (the parent class) on Launch()
            commandBinder.Bind<DemoSignal>().To<DemoCommand>().Once();
            // bind our view to its mediator
            mediationBinder.Bind<DemoView>().To<DemoMediator>();
            // bind our interface to a concrete implementation
            //injectionBinder.Bind<IManager>().To<DemoManager>().ToSingleton();
            // bind the manager implemented as a MonoBehaviour
            DemoMonoManager manager = GameObject.Find("Manager").GetComponent<DemoMonoManager>();
            injectionBinder.Bind<IManager>().ToValue(manager);

            commandBinder.Bind<ManagementSignal>().To<ManagementCommand>().Pooled(); // THIS IS THE NEW MAPPING!!!

        }

        public override void Launch() {
            base.Launch();
            Signal startSignal = injectionBinder.GetInstance<DemoSignal>();
            startSignal.Dispatch();
        }
    }
}