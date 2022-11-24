#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using UnityEngine;

namespace Io.Karte {
    /// <summary>
    /// <para>WebView 連携するためのクラスです。</para>
    /// <para>WebページURLに連携用のクエリパラメータを付与した状態で、URLをWebViewで開くことでWebとAppのユーザーの紐付けが行われます。</para>
    /// <para>なお連携を行うためにはWebページに、KARTEのタグが埋め込まれている必要があります。</para>
    /// </summary>
    public class UserSync {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern string KRTUserSync_stringByAppendingUserSyncQueryParameter (string url);
        [DllImport ("__Internal")]
        static extern string KRTUserSync_getUserSyncScript ();
#endif
        /// <summary>
        /// 指定されたURL文字列にWebView連携用のクエリパラメータを付与します。
        /// </summary>
        /// <param name="urlString"> 連携するページのURL文字列</param>
        /// <returns>
        /// <para>連携用のクエリパラメータを付与したURL文字列を返します。</para>
        /// <para>指定されたURL文字列の形式が正しくない場合、またはSDKの初期化が行われていない場合は、引数に指定したURL文字列を返します。</para>
        /// </returns>
        public static string AppendUserSyncQueryParameter (string urlString) {
#if UNITY_IOS && !UNITY_EDITOR
            return KRTUserSync_stringByAppendingUserSyncQueryParameter (urlString);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass util = new AndroidJavaClass ("io.karte.unity.UnityUserSync");
            return util.CallStatic<string> ("appendUserSyncQueryParameter", urlString);
#else
            return null;
#endif
        }

        /// <summary>
        /// <para>WebViewに連携するためのスクリプトを生成します。</para>
        /// <para>スクリプトをユーザースクリプトとしてWebViewに設定することで連携できます。</para>
        /// <para>なおSDKの初期化が行われていない場合はnilが返却されます。</para>
        /// </summary>
        /// <returns>
        /// <para>生成したスクリプト文字列を返します。</para>
        /// <para>指定されたURL文字列の形式が正しくない場合、またはSDKの初期化が行われていない場合は、引数に指定したURL文字列を返します。</para>
        /// </returns>
        public static string getUserSyncScript () {
#if UNITY_IOS && !UNITY_EDITOR
            return KRTUserSync_getUserSyncScript ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass util = new AndroidJavaClass ("io.karte.unity.UnityUserSync");
            return util.CallStatic<string> ("getUserSyncScript");
#else
            return null;
#endif
        }

    }
}