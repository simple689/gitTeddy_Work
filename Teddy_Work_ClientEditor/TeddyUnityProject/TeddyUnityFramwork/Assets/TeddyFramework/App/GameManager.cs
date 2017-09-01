using System.Collections;

namespace Teddy {
    public class GameManager : Singleton<GameManager> {
        private GameManager() { }

        public IEnumerator launch() {
            Log.instance.print("GameManager");
            yield return null;
        }
    }
}