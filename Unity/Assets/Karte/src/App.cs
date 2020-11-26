#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Io.Karte {
    /// <summary>
    /// <para>SDK全体に影響のある機能を扱うクラスです。</para>
    /// </summary>
    public class App {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern string KRTApp_getVisitorId ();
        [DllImport ("__Internal")]
        static extern string KRTApp_getAppKey ();
        [DllImport ("__Internal")]
        static extern void KRTApp_registerFCMToken (string token);
        [DllImport ("__Internal")]
        static extern void KRTApp_renewVisitorId ();
        [DllImport ("__Internal")]
        static extern void KRTTracker_identify (string values);
        [DllImport ("__Internal")]
        static extern void KRTApp_optOut ();
        [DllImport ("__Internal")]
        static extern void KRTApp_optIn ();
#endif

        /// <summary>
        /// ビジターIDを返します。
        /// </summary>
        /// <returns>ビジターIDを返します。</returns>
        public static string GetVisitorId () {
            string visitorId = "";
#if UNITY_IOS && !UNITY_EDITOR
            visitorId = KRTApp_getVisitorId ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityKarteApp");
            visitorId = tracker.CallStatic<string> ("getVisitorId");
#endif
            return visitorId;
        }

        /// <summary>
        /// <para>FCMトークンをKARTEに登録します。</para>
        /// <para>なお登録時に plugin_native_app_identify イベントを発行します。</para>
        /// </summary>
        /// <param name="token">FCMトークン </param>
        public static void RegisterFCMToken (string token) {
#if UNITY_IOS && !UNITY_EDITOR
            KRTApp_registerFCMToken (token);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityKarteApp");
            tracker.CallStatic ("registerFcmToken", token);
#endif
        }

    /// <summary>
        /// <para>ビジターIDの再生成処理を行います。</para>
        /// <para>なお内部では、以下の処理が行われます。
        /// <list type="bullet">
        /// <item><term>プッシュ通知の配信許可フラグ (plugin_native_app_identity.subscribe) を 非許可 (false) に変更</term></item>
        /// <item><term>端末に保存されている設定値の削除</term></item>
        /// <item><term>visitor_id の再発行</term></item>
        /// <item><term>新たに生成された visitor に対してFCMトークンを紐付け</term></item>
        /// </list>
        /// </para>
        /// </summary>
        public static void RenewVisitorId () {
#if UNITY_IOS && !UNITY_EDITOR
            KRTApp_renewVisitorId ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityKarteApp");
            tracker.CallStatic ("renewVisitorId");
#endif
        }

        /// <summary>
        /// <para>オプトアウト処理を行います。</para>
        /// <para>オプトアウト実行後、計測をはじめとしたSDKの内部処理は全て無効化されます。</para>
        /// <para>optInを実行することでオプトアウト状態を解除できます。</para>
        /// </summary>
        public static void OptOut () {
#if UNITY_IOS && !UNITY_EDITOR
            KRTApp_optOut ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityKarteApp");
            tracker.CallStatic ("optOut");
#endif
        }

        /// <summary>
        /// オプトアウト状態を解除します。
        /// </summary>
        public static void OptIn () {
#if UNITY_IOS && !UNITY_EDITOR
            KRTApp_optIn ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityKarteApp");
            tracker.CallStatic ("optIn");
#endif
        }
    }
}