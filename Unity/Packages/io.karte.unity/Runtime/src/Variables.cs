#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using System;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Io.Karte
{
    /// <summary>
    /// <para>Variablesクラスは、設定値配信に関連するクラスで、以下の機能を提供します。
    /// <list type="bullet">
    /// <item><term>設定値の取得</term></item>
    /// <item><term>設定値の保持・管理</term></item>
    /// <item><term>効果測定用のイベントの送信</term></item>
    /// </list>
    /// </para>
    /// <para>なおVariablesクラスを利用するためには、事前にSDKの初期化が必要です。</para>
    /// <para>初期化が行われていない場合、例外が発生する可能性があります。</para>
    /// </summary>
    public class Variables
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern void KRTVariables_fetch ();
        [DllImport ("__Internal")]
        static extern void KRTVariables_fetchWithCompletionBlock (string callbackTarget, string callbackHash);
        [DllImport ("__Internal")]
        static extern void KRTVariables_trackOpen (string serializedVariables);
        [DllImport ("__Internal")]
        static extern void KRTVariables_trackOpenWithValues (string serializedVariables, string serializedValues);
        [DllImport ("__Internal")]
        static extern void KRTVariables_trackClick (string serializedVariables);
        [DllImport ("__Internal")]
        static extern void KRTVariables_trackClickWithValues (string serializedVariables, string serializedValues);
        [DllImport ("__Internal")]
        static extern int KRTVariables_lastFetchTime ();
        [DllImport ("__Internal")]
        static extern int KRTVariables_lastFetchStatus ();
        [DllImport ("__Internal")]
        static extern bool KRTVariables_hasSuccessfulLastFetch (int seconds);
#endif


        /// <summary>
        /// <para>設定値を取得します。</para>
        /// <para>取得は非同期で行われるため、設定値の取得完了をトリガーに処理を行いたい場合は、<c>FetchWithCompletion</c>を利用してください。</para>
        /// <para>※事前にトラッカーの初期化が必要です。</para>
        /// <para>初期化時に指定したアプリケーションキーに対応するSDKの初期化が行われていない場合は、例外が発生します。</para>
        /// </summary>
        public static void Fetch()
        {
#if UNITY_IOS && !UNITY_EDITOR
            KRTVariables_fetch ();
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            variables.CallStatic ("fetch");
#endif
        }
        /// <summary>
        /// <para>設定値を取得します。</para>
        /// <para>設定値の取得が完了したタイミングで、引数に指定したクロージャにコールバックされます。</para>
        /// <example>
        /// <code>
        /// Variables.FetchWithCompletion ((result) => {
        ///     string str = result ? "取得成功" : "取得失敗";
        /// });
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="callback">取得完了通知クロージャ</param>
        public static void FetchWithCompletion(Action<bool> callback)
        {
            string callbackId = CallbackReceiver.GenerateUniqueCallbackID();
            CallbackReceiver.Instance.AddVariablesCallback(callbackId, callback);
#if UNITY_IOS && !UNITY_EDITOR
            KRTVariables_fetchWithCompletionBlock (CallbackReceiver.CallbackTargetName, callbackId);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            variables.CallStatic ("fetchWithCompletionBlock", new object[] { CallbackReceiver.CallbackTargetName, callbackId });
#endif
        }

        /// <summary>
        /// <para>キーに紐付くVariableオブジェクトを返します。</para>
        /// <para>接客サービス側で設定値を設定していない場合であってもオブジェクトは返ります。</para>
        /// <para>設定値の設定有無はVariableクラスの <c>isDefined</c>プロパティを使って判定します。</para>
        /// </summary>
        /// <param name="key">設定値キー</param>
        /// <returns>Variableオブジェクトを返します。</returns>
        public static Variable GetVariable(string key)
        {
            return new Variable(key);
        }

        /// <summary>
        /// 指定された設定値に関連するキャンペーン情報を元に効果測定用のイベント（message_open）を発火します。
        /// </summary>
        /// <param name="vars">設定値の配列</param>
        public static void TrackOpen(Variable[] vars)
        {
            string[] variableNames = vars.Select(v => v.name).ToArray();
            string serialzedNames = JsonConvert.SerializeObject(variableNames);

#if UNITY_IOS && !UNITY_EDITOR
            KRTVariables_trackOpen (serialzedNames);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            variables.CallStatic ("trackOpen", new object[] { serialzedNames });
#endif
        }

        /// <summary>
        /// 指定された設定値に関連するキャンペーン情報を元に効果測定用のイベント（message_open）を発火します。
        /// </summary>
        /// <param name="vars">設定値の配列</param>
        /// <param name="values">イベントに紐付けるカスタムオブジェクト</param>
        public static void TrackOpen(Variable[] vars, JObject values)
        {
            string[] variableNames = vars.Select(v => v.name).ToArray();
            string serialzedNames = JsonConvert.SerializeObject(variableNames);
            string serializedValues = JsonConvert.SerializeObject(TrackerHelper.Normalize(values));
#if UNITY_IOS && !UNITY_EDITOR
            KRTVariables_trackOpenWithValues (serialzedNames, serializedValues);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            variables.CallStatic ("trackOpen", new object[] { serialzedNames, serializedValues });
#endif
        }

        /// <summary>
        /// 指定された設定値に関連するキャンペーン情報を元に効果測定用のイベント（message_click）を発火します。
        /// </summary>
        /// <param name="vars">設定値の配列</param>
        public static void TrackClick(Variable[] vars)
        {
            string[] variableNames = vars.Select(v => v.name).ToArray();
            string serialzedNames = JsonConvert.SerializeObject(variableNames);
#if UNITY_IOS && !UNITY_EDITOR
            KRTVariables_trackClick (serialzedNames);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            variables.CallStatic ("trackClick", new object[] { serialzedNames });
#endif
        }

        /// <summary>
        /// 指定された設定値に関連するキャンペーン情報を元に効果測定用のイベント（message_click）を発火します。
        /// </summary>
        /// <param name="vars">設定値の配列</param>
        /// <param name="values">イベントに紐付けるカスタムオブジェクト</param>
        public static void TrackClick(Variable[] vars, JObject values)
        {
            string[] variableNames = vars.Select(v => v.name).ToArray();
            string serialzedNames = JsonConvert.SerializeObject(variableNames);
            string serializedValues = JsonConvert.SerializeObject(TrackerHelper.Normalize(values));
#if UNITY_IOS && !UNITY_EDITOR
            KRTVariables_trackClickWithValues (serialzedNames, serializedValues);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            variables.CallStatic ("trackClick", new object[] { serialzedNames, serializedValues });
#endif
        }

        /// <summary>
        /// <para>最終フェッチ完了時間を返します。</para>
        /// <para>未フェッチな場合は nil を返します。</para>
        /// <para>この機能はiOSのみで提供されています。</para>
        /// <para>Androidでは常にnullを返します。</para>
        /// </summary>
        public static DateTime? LastFetchTime()
        {
            int lastFetchTime = 0;
#if UNITY_IOS && !UNITY_EDITOR
            lastFetchTime = KRTVariables_lastFetchTime ();
#elif UNITY_ANDROID && !UNITY_EDITOR
#endif
            if (lastFetchTime == 0)
            {
                return null;
            }

            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
            dt = dt.AddSeconds(lastFetchTime);
            return dt;
        }

        /// <summary>
        /// <para>最終フェッチ完了ステータスを返します。</para>
        /// <para>この機能はiOSのみで提供されています。</para>
        /// <para>Androidでは常に0を返します。</para>
        /// </summary>
        public static int LastFetchStatus()
        {
            int lastFetchStatus = 0;
#if UNITY_IOS && !UNITY_EDITOR
            lastFetchStatus = KRTVariables_lastFetchStatus ();
#elif UNITY_ANDROID && !UNITY_EDITOR
#endif
            return lastFetchStatus;
        }

        /// <summary>
        /// <para>直近指定秒以内に成功したフェッチ結果があるかどうかを返します。</para>
        /// <para>この機能はiOSのみで提供されています。</para>
        /// <para>Androidでは常にfalseを返します。</para>
        /// </summary>
        public static bool HasSuccessfulLastFetchIn(int seconds)
        {
            bool hasSuccessfulLastFetchIn = false;
#if UNITY_IOS && !UNITY_EDITOR
            hasSuccessfulLastFetchIn = KRTVariables_hasSuccessfulLastFetch (seconds);
#elif UNITY_ANDROID && !UNITY_EDITOR
#endif
            return hasSuccessfulLastFetchIn;
        }
    }
}