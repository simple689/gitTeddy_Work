using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

namespace TeddyFramwork {
    // 预构建的context在Strange中称为MVCSContext，MVCSContext默认使用event机制，创建使用signal机制的context，游戏中其他的context要继承这个SignalContext。
    public class SignalContext : MVCSContext {
        public SignalContext(MonoBehaviour contextView) : base(contextView) {
        }

        protected override void addCoreComponents() {
            base.addCoreComponents();
            // bind signal command binder
            injectionBinder.Unbind<ICommandBinder>();
            injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        }
    }
}