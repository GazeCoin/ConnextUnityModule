using UnityEngine;

namespace UnityAsyncAwaitUtil {
    /// <summary>
    /// Modified to support running in Editor without runtime playback
    /// </summary>
    public class AsyncCoroutineRunner : MonoBehaviour {
        static AsyncCoroutineRunner _instance;

        public static AsyncCoroutineRunner Instance {
            get {
                if (_instance == null) {
                    //Look for a game object first
                    _instance = FindObjectOfType<AsyncCoroutineRunner>();

                    //If nothing was found, create one
                    if (_instance == null) {
                        //Create the new game object
                        GameObject obj = new GameObject("AsyncCoroutineRunner");

                        //Don't show the object in the scene hierarchy
                        obj.hideFlags = HideFlags.HideAndDontSave;

                        //Add the coroutine runner component to it
                        _instance = obj.AddComponent<AsyncCoroutineRunner>();
                    }
                }

                return _instance;
            }
        }

        void Start() {
            //Check this is the singleton instance
            if (this == _instance) DontDestroyOnLoad(gameObject);

            //Otherwise, destroy it
            else Destroy(gameObject);
        }
    }
}
