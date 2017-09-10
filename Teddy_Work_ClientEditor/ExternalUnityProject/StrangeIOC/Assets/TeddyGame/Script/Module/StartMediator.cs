using TeddyFramwork;
using strange.extensions.mediation.impl;

namespace TeddyGame {
    public class StartMediator : Mediator {
        [Inject]
        public StartView view { get; set; }

        [Inject]
        public IManager manager { get; set; }

        //[Inject]
        //public ManagementSignal management { get; set; }

        public override void OnRegister() {
            view.buttonClicked.AddListener(delegate () {
                manager.DoManagement();
            });
            //view.buttonClicked.AddListener(management.Dispatch);
        }
    }
}
