using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEditor;
using System.Text;

[assembly : InternalsVisibleTo ("EditModeTest")]
namespace Io.Karte {
    internal class CallbackReceiver : MonoBehaviour {
        private　 Dictionary<string, Action<bool>> variablesCallbacks = new Dictionary<string, Action<bool>> ();
        public static CallbackReceiver Instance;
        public static string CallbackTargetName = "KarteCallbackReceiverObject";
        private static GameObject attachedGameObject;

        [Serializable]
        private class VariablesCallbackArgument {
#pragma warning disable 649
            public string callbackId;
            public bool result;
        }

         [Serializable]
        private class OpenUrlWithSceneCallbackArgument {
#pragma warning disable 649
            public string url;
            public string sceneIdentifier;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init(){
            SetupCallbackReceiver();
        }

        public CallbackReceiver () {
            Instance = this;
        }

        public void VariableCallback (string message) {
            VariablesCallbackArgument arg = JsonUtility.FromJson<VariablesCallbackArgument> (message);
            Action<bool> callback;
            try {
                callback = variablesCallbacks[arg.callbackId];
                variablesCallbacks.Remove (arg.callbackId);
                callback (arg.result);
            } catch (KeyNotFoundException) {
                return;
            }
        }

        internal void AddVariablesCallback (string callbackId, Action<bool> callback) {
            variablesCallbacks.Add (callbackId, callback);
        }

        public void InAppMessagingOpenUrlCallback(string message) {
            InAppMessaging.InvokeOpenUrlHandler(message);
        }

        public void InAppMessagingOpenUrlWithSceneCallback(string message) {
            OpenUrlWithSceneCallbackArgument arg = JsonUtility.FromJson<OpenUrlWithSceneCallbackArgument> (message);
            InAppMessaging.InvokeOpenUrlWithSceneHandler(arg.url, arg.sceneIdentifier);
        }

        private static void SetupCallbackReceiver () {
            if (attachedGameObject) {
                return;
            }
            attachedGameObject = new GameObject (CallbackTargetName);
            attachedGameObject.AddComponent<CallbackReceiver> ();
            DontDestroyOnLoad(attachedGameObject);
        }

        public static string GenerateUniqueCallbackID () {
            StringBuilder sb = new StringBuilder ("", 16);
            const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
            for (int i = 0; i < 16; i++) {
                sb.Append (glyphs[UnityEngine.Random.Range (0, glyphs.Length)]);
            }
            return sb.ToString ();
        }
    }
}