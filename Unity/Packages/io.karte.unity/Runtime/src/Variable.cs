#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine;

[assembly : InternalsVisibleTo ("EditModeTest")]
namespace Io.Karte {
    /// <summary>
    /// <para>Variableクラスは、設定値配信に関連する機能で、設定値と配信元の接客サービスの情報を保持する機能を提供します。</para>
    /// <para>Variablesクラスを経由して初期化されるため、個別で初期化して使用することはありません。</para>
    /// </summary>
    [Serializable]
    public class Variable {

#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        static extern string KRTVariables_string (string key);
        [DllImport ("__Internal")]
        static extern int KRTVariables_integer (string key, int defaultValue);
        [DllImport ("__Internal")]
        static extern double KRTVariables_double (string key, double defaultValue);
        [DllImport ("__Internal")]
        static extern bool KRTVariables_bool (string key, bool defaultValue);
        [DllImport ("__Internal")]
        static extern string KRTVariables_array (string key);
        [DllImport ("__Internal")]
        static extern string KRTVariables_object (string key);
        [DllImport ("__Internal")]
        static extern bool KRTVariables_isDefined (string key);
#endif

        [SerializeField]
        [JsonProperty]
        internal string name;

        internal Variable (string name) {
            this.name = name;
        }

        /// <summary>
        /// <para>設定値を文字列値として返します。</para>
        /// </summary>
        /// <param name="defaultValue">設定値が未設定の場合に利用する値</param>
        /// <returns>
        /// <para>設定値を文字列値として返します。</para>
        /// <para>接客サービス側で設定値が未設定の場合は、引数として指定したデフォルト値を返します。</para>
        /// </returns>
        public string GetString (string defaultValue) {
#if UNITY_IOS && !UNITY_EDITOR
            return KRTVariables_string(name);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            return variables.CallStatic<string> ("getString", new object[] { name, defaultValue });
#else
#endif
            return defaultValue;
        }

        /// <summary>
        /// <para>設定値を整数値として返します。</para>
        /// </summary>
        /// <param name="defaultValue">設定値が未設定の場合に利用する値</param>
        /// <returns>
        /// <para>設定値を整数値として返します。</para>
        /// <para>値が浮動小数点数の場合、浮動小数点以下は切り捨てられます。</para>
        /// <para>なお以下の場合は、デフォルト値を返します。</para>
        /// <para>
        /// <list type="bullet">
        /// <item><term>接客サービス側で設定値が未設定</term></item>
        /// <item><term>設定値が数値として扱えない</term></item>
        /// </list>
        /// </para>
        /// </returns>
        public int GetInt (int defaultValue) {
#if UNITY_IOS && !UNITY_EDITOR
            return KRTVariables_integer(name, defaultValue);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            return variables.CallStatic<int> ("getLong", new object[] { name, defaultValue });
#else
#endif
            return defaultValue;
        }
        /// <summary>
        /// <para>設定値を浮動小数点数として返します。</para>
        /// </summary>
        /// <param name="defaultValue">設定値が未設定の場合に利用する値</param>
        /// <returns>
        /// <para>設定値を浮動小数点値として返します。</para>
        /// <para>なお以下の場合は、デフォルト値を返します。</para>
        /// <para>
        /// <list type="bullet">
        /// <item><term>接客サービス側で設定値が未設定</term></item>
        /// <item><term>設定値が数値として扱えない</term></item>
        /// </list>
        /// </para>
        /// </returns>
        public double GetDouble (double defaultValue) {
#if UNITY_IOS && !UNITY_EDITOR
            return KRTVariables_double(name, defaultValue);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            return variables.CallStatic<double> ("getDouble", new object[] { name, defaultValue });
#else
#endif
            return defaultValue;
        }
        /// <summary>
        /// <para>設定値を真偽値として返します。</para>
        /// </summary>
        /// <param name="defaultValue">設定値が未設定の場合に利用する値</param>
        /// <returns>
        /// <para>設定値を真偽値として返します。</para>
        /// <para>なお以下の場合は、デフォルト値を返します。</para>
        /// <para>
        /// <list type="bullet">
        /// <item><term>接客サービス側で設定値が未設定</term></item>
        /// </list>
        /// </para>
        /// </returns>
        public bool GetBool (bool defaultValue) {
#if UNITY_IOS && !UNITY_EDITOR
            return KRTVariables_bool(name, defaultValue);
#elif UNITY_ANDROID && !UNITY_EDITOR
#else
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            return variables.CallStatic<bool> ("getBoolean", new object[] { name, defaultValue });
#endif
            return defaultValue;
        }

        /// <summary>
        /// <para>設定値をobjectの配列として返します。</para>
        /// </summary>
        /// <param name="defaultValue">設定値が未設定の場合に利用する値</param>
        /// <returns>
        /// <para>設定値をobjectの配列として返します。</para>
        /// <para>なお以下の場合は、デフォルト値を返します。</para>
        /// <para>
        /// <list type="bullet">
        /// <item><term>接客サービス側で設定値が未設定</term></item>
        /// <item><term>設定値がobjectの配列として扱えない</term></item>
        /// </list>
        /// </para>
        /// </returns>
        public object[] GetJsonArray (object[] defaultValue) {
            string serializedValue = null;
#if UNITY_IOS && !UNITY_EDITOR
            serializedValue = KRTVariables_array(name);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            serializedValue = variables.CallStatic<string> ("getArray", new object[] { name });
#else
#endif
            if (serializedValue == null) {
                return defaultValue;
            } else {
                try {
                    return JsonConvert.DeserializeObject<object[]> (serializedValue);
                } catch (JsonException) {
                    return defaultValue;
                }
            }
        }

        /// <summary>
        /// 設定値をJObject値として返します。
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns>
        /// <para>設定値をJObject値として返します。</para>
        /// <para>なお以下の場合は、デフォルト値を返します。</para>
        /// <para>
        /// <list type="bullet">
        /// <item><term>接客サービス側で設定値が未設定</term></item>
        /// <item><term>設定値がJObject値として扱えない</term></item>
        /// </list>
        /// </para>
        /// </returns>
        public JObject GetJsonObject (JObject defaultValue) {
            string serializedValue = null;
#if UNITY_IOS && !UNITY_EDITOR
            serializedValue = KRTVariables_object(name);
#elif UNITY_ANDROID && !UNITY_EDITOR
            AndroidJavaClass variables = new AndroidJavaClass ("io.karte.unity.UnityVariables");
            serializedValue = variables.CallStatic<string> ("getObject", new object[] { name });
#else
#endif
            if (serializedValue == null) {
                return defaultValue;
            } else {
                try {
                    return JObject.Parse (serializedValue);

                } catch (JsonException) {
                    return defaultValue;
                }
            }
        }
    }
}