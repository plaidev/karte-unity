#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Io.Karte {
    /// <summary>
    /// <para>Trackerクラスは、主にイベントのトラッキング機能を提供します。</para>
    /// <para>KARTEにイベントを送信する場合、本クラスが提供する各種トラッキングメソッドを利用して行います。</para>
    /// </summary>
    public class Tracker {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern void KRTTracker_view (string viewName);
        [DllImport ("__Internal")]
        static extern void KRTTracker_viewWithTitle (string viewName, string title);
        [DllImport ("__Internal")]
        static extern void KRTTracker_viewWithTitleAndValues(string viewName, string title, string values);
        [DllImport ("__Internal")]
        static extern void KRTTracker_track (string eventName);
        [DllImport ("__Internal")]
        static extern void KRTTracker_trackWithValues (string eventName, string values);
        [DllImport ("__Internal")]
        static extern void KRTTracker_trackClick (string userInfo);
        [DllImport ("__Internal")]
        static extern void KRTTracker_identify (string values);
        [DllImport ("__Internal")]
        static extern void KRTTracker_identifyWithUserId (string userId, string values);
        [DllImport ("__Internal")]
        static extern void KRTTracker_attribute (string values);
#endif

        /// <summary>
        /// Viewイベントを送信します。
        /// </summary>
        /// <param name="viewName">画面名 </param>
        public static void View (string viewName) {
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_view (viewName);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("view", new object[] { viewName });
#endif
        }

        /// <summary>
        /// Viewイベントを送信します。
        /// </summary>
        /// <param name="viewName">画面名 </param>
        /// <param name="title">画面タイトル</param>
        public static void View (string viewName, string title) {
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_viewWithTitle (viewName, title);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("view", new object[] { viewName, title });
#endif
        }

        /// <summary>
        /// Viewイベントを送信します。
        /// </summary>
        /// <param name="viewName">画面名 </param>
        /// <param name="title">画面タイトル</param>
        /// <param name="values">イベントに紐付けるカスタムオブジェクト</param>
        public static void View (string viewName, string title, JObject values) {
            string serializedValues = values.ToString ();
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_viewWithTitleAndValues (viewName, title, serializedValues);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("view", new object[] { viewName, title, serializedValues });
#endif
        }

        // <summary>
        /// イベントを送信します。
        /// </summary>
        /// <param name="eventName">イベント名</param>
        public static void Track (string eventName) {
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_track (eventName);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("track", new object[] { eventName });
#endif
        }

        /// <summary>
        /// イベントを送信します。
        /// </summary>
        /// <param name="eventName">イベント名</param>
        /// <param name="values">イベントに紐付けるカスタムオブジェクト</param>
        public static void Track (string eventName, JObject values) {
            string serializedValues = values.ToString ();
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_trackWithValues (eventName, serializedValues);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("track", new object[] { eventName, serializedValues });
#endif
        }

        /// <summary>
        /// <para>Identifyイベント（ユーザー情報）を送信します。</para>
        /// <para>KARTEではユーザー情報もユーザー情報イベントとして、他のイベントと同じ形式で扱います。</para>
        /// </summary>
        /// <param name="values">ユーザーに紐付けるカスタムオブジェクト</param>
        public static void Identify (JObject values) {
            string serializedValues = values.ToString ();
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_identify (serializedValues);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("identify", serializedValues);
#endif
        }

        /// <summary>
        /// <para>Identifyイベント（ユーザー情報）を送信します。</para>
        /// <para>KARTEではユーザー情報もユーザー情報イベントとして、他のイベントと同じ形式で扱います。</para>
        /// </summary>
        /// <param name="userId">ユーザーを識別する一意なID</param>
        /// <param name="values">ユーザーに紐付けるカスタムオブジェクト</param>
        public static void Identify (string userId, JObject values) {
            string serializedValues = values.ToString ();
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_identifyWithUserId (userId, serializedValues);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("identify", new object[] { userId, serializedValues });
#endif
        }

        /// <summary>
        /// <para>Attributeイベント（ユーザー情報）を送信します。</para>
        /// </summary>
        /// <param name="values">ユーザーに紐付けるカスタムオブジェクト</param>
        public static void Attribute (JObject values) {
            string serializedValues = values.ToString ();
#if UNITY_IOS && !UNITY_EDITOR
            KRTTracker_attribute (serializedValues);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass tracker = new AndroidJavaClass ("io.karte.unity.UnityTracker");
            tracker.CallStatic ("attribute", serializedValues);
#endif
        }
    }
}