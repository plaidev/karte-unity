using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly : InternalsVisibleTo ("EditModeTest")]
namespace Io.Karte {
    internal class CallbackReceiver : MonoBehaviour {
        private　 Dictionary<string, Action<bool>> variablesCallbacks = new Dictionary<string, Action<bool>> ();
        public static CallbackReceiver instance;

        [Serializable]
        private class VariablesCallbackArugment {
#pragma warning disable 649
            public string callbackId;
            public bool result;
        }

        public CallbackReceiver () {
            instance = this;
        }

        public void VariableCallback (string message) {
            VariablesCallbackArugment arg = JsonUtility.FromJson<VariablesCallbackArugment> (message);
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
    }
}