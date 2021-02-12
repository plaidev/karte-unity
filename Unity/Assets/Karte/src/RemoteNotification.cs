#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Io.Karte {
    /// <summary>
    /// RemoteNotificationHandlerクラスは、KARTEから送信された通知メッセージを処理する機能を提供します。
    /// </summary>
    public class RemoteNotificationHandler {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern bool KRTRemoteNotification_handle (string userInfo);
        [DllImport ("__Internal")]
        static extern string KRTRemoteNotification_url (string userInfo);
#endif

        /// <summary>
        /// KARTE経由で送信された通知メッセージを処理します。処理内容はプラットフォームによって異なります。
        /// <para>iOS: 通知メッセージに含まれるディープリンクURLを開きます。</para>
        /// <para>Android: 通知メッセージから、通知を作成します。</para>
        /// </summary>
        /// <param name="userInfo">
        /// 通知メッセージ
        /// </param>
        /// <returns>
        /// <para>処理結果を返します。</para>
        /// <para>処理できた場合は true、できなかった場合は false を返します。</para>
        /// </returns>
        public static bool HandleRemoteNotification (IDictionary<string, string> userInfo) {
            //Unity 2020系でFirebaseから渡ってきたDictionaryをシリアライズするとクラッシュする不具合のワークアラウンド
            Dictionary<string,string> dict = new Dictionary<string, string>();
            foreach(string key in userInfo.Keys) {
               dict.Add(key, userInfo[key]);
            }
            string userInfoStr = JsonConvert.SerializeObject (dict);
#if UNITY_IOS && !UNITY_EDITOR
            return KRTRemoteNotification_handle (userInfoStr);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass messageHandler = new AndroidJavaClass ("io.karte.unity.UnityNotifications");
            return messageHandler.CallStatic<bool> ("handleMessage", userInfoStr);
#else
            return false;
#endif
        }

        /// <summary>
        /// KARTE経由で送信された通知メッセージに含まれるURLを返します。
        /// </summary>
        /// <param name="userInfo">
        /// 通知メッセージ
        /// </param>
        /// <returns>
        /// ディープリンクURLを返します。
        /// </returns>
        public static string RetrieveURLFromUserInfo (IDictionary<string, string> userInfo) {
            //Unity 2020系でFirebaseから渡ってきたDictionaryをシリアライズするとクラッシュする不具合のワークアラウンド
            Dictionary<string,string> dict = new Dictionary<string, string>();
            foreach(string key in userInfo.Keys) {
               dict.Add(key, userInfo[key]);
            }
            string userInfoStr = JsonConvert.SerializeObject (dict);
#if UNITY_IOS && !UNITY_EDITOR
            return KRTRemoteNotification_url (userInfoStr);
#elif UNITY_ANDROID && !UNITY_EDITOR
            string attributesStr = userInfo["krt_attributes"];
            JObject json = JObject.Parse (attributesStr);
            string link = json.Value<string> ("url");
            return link;
#else
            return "";
#endif
        }

        /// <summary>
        /// 通知メッセージからSDKが自動で処理するデータを取り出し、KarteAttribtuesインスタンスを返します。
        /// </summary>
        /// <param name="userInfo">
        /// 通知メッセージ
        /// </param>
        /// <returns>
        /// KarteAttributesインスタンスを返します。
        /// </returns>
        public static KarteAttributes ExtractKarteAttributes (IDictionary<string, string> userInfo) {
            string attributesStr = userInfo["krt_attributes"];
            JObject json = JObject.Parse (attributesStr);
            KarteAttributes attrs = new KarteAttributes (
                json.Value<string> ("title"),
                json.Value<string> ("body"),
                json.Value<bool> ("sound"),
                json.Value<string> ("channel"),
                json.Value<string> ("url"),
                json.Value<string> ("type"),
                json.Value<string> ("fileUrl")
            );
            return attrs;
        }
    }
}