using System;
using System.Threading.Tasks;
using UnityEngine;

// TODO: Remove the allocs here, use a static memory pool?
public static class Awaiters {
    readonly static WaitForUpdate _waitForUpdate = new WaitForUpdate();
    readonly static WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    readonly static WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    /// <summary>
    /// This yielding functionality should only be used by operations that will be running during runtime only. Calls to these functions 
    /// during non-playmode editor scripts will through exceptions. Use the following 'Editor / Runtime Compatible' functionality instead
    /// </summary>
    public static class Runtime {
        public static WaitForUpdate NextFrame {
            get {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    throw new OperationCanceledException("The 'NextFrame' operation can't be completed outside of play mode. Asynchronous editor functionality shouldn't rely on this functionality!");
#endif
                return _waitForUpdate;
            }
        }

        public static WaitForFixedUpdate FixedUpdate {
            get {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    throw new OperationCanceledException("The 'FixedUpdate' operation can't be completed outside of play mode. Asynchronous editor functionality shouldn't rely on this functionality!");
#endif
                return _waitForFixedUpdate;
            }
        }

        public static WaitForEndOfFrame EndOfFrame {
            get {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    throw new OperationCanceledException("The 'EndOfFrame' operation can't be completed outside of play mode. Asynchronous editor functionality shouldn't rely on this functionality!");
#endif
                return _waitForEndOfFrame;
            }
        }

        public static WaitForSeconds Seconds(float seconds) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                throw new OperationCanceledException("The 'Seconds' operation can't be completed outside of play mode. Asynchronous editor functionality shouldn't rely on this functionality!");
#endif
            return new WaitForSeconds(seconds);
        }

        public static WaitForSecondsRealtime SecondsRealtime(float seconds) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                throw new OperationCanceledException("The 'SecondsRealtime' operation can't be completed outside of play mode. Asynchronous editor functionality shouldn't rely on this functionality!");
#endif
            return new WaitForSecondsRealtime(seconds);
        }

        public static WaitUntil Until(Func<bool> predicate) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                throw new OperationCanceledException("The 'Until' operation can't be completed outside of play mode. Asynchronous editor functionality shouldn't rely on this functionality!");
#endif
            return new WaitUntil(predicate);
        }

        public static WaitWhile While(Func<bool> predicate) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                throw new OperationCanceledException("The 'While' operation can't be completed outside of play mode. Asynchronous editor functionality shouldn't rely on this functionality!");
#endif
            return new WaitWhile(predicate);
        }
    }

    /// <summary>
    /// This yielding functionality will dynamically choose built in Unity objects and .Net Task equivalents to allow for use during 
    /// </summary>
    public static class Hybrid {
        public static async Task Seconds(float seconds) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                await Task.Delay(TimeSpan.FromSeconds(seconds));
            else
#endif
                await new WaitForSeconds(seconds);
        }

        public static async Task SecondsRealtime(float seconds) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                await Task.Delay(TimeSpan.FromSeconds(seconds));
            else
#endif
                await new WaitForSecondsRealtime(seconds);
        }

        public static async Task While(Func<bool> predicate) {
#if UNITY_EDITOR
            if (Application.isPlaying) {
                while (predicate())
                    await Task.Yield();
            } else
#endif
                await new WaitWhile(predicate);
        }

        public static async Task Until(Func<bool> predicate) {
#if UNITY_EDITOR
            if (!Application.isPlaying) {
                while (!predicate())
                    await Task.Yield();
            } else
#endif
                await new WaitUntil(predicate);
        }
    }
}
