#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine;

namespace Io.Karte {

    /// <summary>
    /// InAppMessagingManagerクラスは、アプリ内メッセージに関連する機能を提供します。
    /// </summary>
    public class InAppMessaging {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern bool KRTInAppMessaging_presenting ();
        [DllImport ("__Internal")]
        static extern void KRTInAppMessaging_dismiss ();
        [DllImport ("__Internal")]
        static extern void KRTInAppMessaging_suppress ();
        [DllImport ("__Internal")]
        static extern void KRTInAppMessaging_unsuppress ();
#endif

        /// <summary>
        /// 表示中のアプリ内メッセージをリセットします。
        /// </summary>
        public static void dismiss () {
#if UNITY_IOS && !UNITY_EDITOR
            KRTInAppMessaging_dismiss ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
            activity.Call ("runOnUiThread", new AndroidJavaRunnable (dismissAndroid));
#endif
        }

        private static void dismissAndroid () {
            AndroidJavaClass manager = new AndroidJavaClass ("io.karte.unity.UnityInAppMessaging");
            manager.CallStatic ("dismiss");
        }

        /// <summary>
        /// 接客が表示中であるか判定します。
        /// </summary>
        /// <returns>
        /// 判定結果を返します。アプリ内メッセージを表示中の場合はtrue、そうでない場合はfalseを返します。
        /// </returns>
        public static bool isPresenting {
            get {
#if UNITY_IOS && !UNITY_EDITOR
                return KRTInAppMessaging_presenting ();
#elif UNITY_ANDROID && !UNITY_EDITOR
                AndroidJavaClass manager = new AndroidJavaClass ("io.karte.unity.UnityInAppMessaging");
                return manager.CallStatic<bool> ("isPresenting");
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// アプリ内メッセージの表示を抑制します。
        /// </summary>
        public static void suppress () {
#if UNITY_IOS && !UNITY_EDITOR
            KRTInAppMessaging_suppress ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass manager = new AndroidJavaClass ("io.karte.unity.UnityInAppMessaging");
            manager.CallStatic ("suppress");
#endif
        }
        /// <summary>
        ///アプリ内メッセージの表示抑制を解除します。
        /// </summary>
        public static void unsuppress () {
#if UNITY_IOS && !UNITY_EDITOR
            KRTInAppMessaging_unsuppress ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass manager = new AndroidJavaClass ("io.karte.unity.UnityInAppMessaging");
            manager.CallStatic ("unsuppress");
#endif
        }
    }
}