using UnityEngine;
using TeddyFramwork;

namespace TeddyGame {
    public class StartContext : SignalContext {
        public StartContext(MonoBehaviour contextView) : base(contextView) {
        }

        protected override void mapBindings() {
            base.mapBindings();
            //commandBinder.Bind<StartSignal>().To<StartCommand>().Once();

            mediationBinder.Bind<StartView>().To<StartMediator>();
            injectionBinder.Bind<IManager>().To<EnemyManager>().ToSingleton();

        }
    }
}