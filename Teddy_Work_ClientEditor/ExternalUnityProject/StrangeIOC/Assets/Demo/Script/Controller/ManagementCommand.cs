using TeddyFramwork;
using strange.extensions.command.impl;
 
namespace Game {
    public class ManagementCommand : Command {
        [Inject]
        public IManager manager { get; set; }

        public override void Execute() {
            manager.DoManagement();
        }
    }
}