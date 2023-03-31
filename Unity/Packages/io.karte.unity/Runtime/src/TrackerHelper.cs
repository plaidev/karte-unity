using System;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Io.Karte
{
    /// <summary>
    /// <para>計測処理に関わるヘルパーメソッドなどを定義したクラスです。</para>
    /// </summary>
    public class TrackerHelper
    {
        private TrackerHelper()
        {
        }

        /// <summary>
        /// <para>計測で送信されるデータの正規化を行います。</para>
        /// </summary>
        /// <param name="token">正規化するデータ</param>
        /// <returns>正規化済みのデータを返します。</returns>
        public static JToken Normalize(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Array:
                    var newArray = new JArray();
                    foreach (var element in token)
                    {
                        newArray.Add(Normalize(element));
                    }
                    return newArray;
                case JTokenType.Object:
                    var newObject = new JObject();
                    foreach (var pair in (token as JObject))
                    {
                        newObject.Add(pair.Key, Normalize(pair.Value));
                    }
                    return newObject;
                case JTokenType.Date:
                    return ((DateTimeOffset)token).ToUnixTimeSeconds();
                default:
                    return token;
            }
        }
    }
}
