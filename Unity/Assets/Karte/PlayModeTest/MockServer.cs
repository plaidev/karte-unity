using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Io.Karte;
using Newtonsoft.Json.Linq;
using TestHelper;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Tests {
    [System.Serializable]
    public class OnRequestEvent : UnityEvent<HttpListenerContext> { }

    public class MockServer : MonoBehaviour {
        private const int port = 8010;
        private HttpListener httpListener = new HttpListener ();
        private OnRequestEvent OnRequest;
        private List<string> receivedEvents = new List<string> ();
        private ExpectedEvent[] expectedEvents = new ExpectedEvent[] { };

        void Start () {
            httpListener.Prefixes.Add ("http://127.0.0.1:" + port + "/");
            OnRequest = new OnRequestEvent ();
            OnRequest.AddListener (HandleRequest);
            StartServer ();
        }

        // リクエストの受け付けを開始する
        public async void StartServer () {
            if (!httpListener.IsListening) {
                httpListener.Start ();
                Task.Factory.StartNew(async () => {
                    while(true) {
                        await Listen(httpListener);
                    }
                }, TaskCreationOptions.LongRunning);
            }
        }

        private async Task Listen(HttpListener listener) {
            try {
                var context = await listener.GetContextAsync ();
                Debug.Log ("Request path: " + context.Request.RawUrl);
                OnRequest.Invoke (context);
            } catch (HttpListenerException) {
                Debug.Log ("HttpListenerException");
            }
        }

        // サーバーを停止する
        public void StopServer () {
            if (httpListener.IsListening) {
                httpListener.Stop ();
            }
        }

        // 破棄時にサーバーを止める
        void OnDestroy () {
            StopServer ();
        }

        public string[] ReceivedEvents () {
            return receivedEvents.ToArray ();
        }

        public void ExpectEvents (ExpectedEvent[] events) {
            expectedEvents = events;
        }

        public void Reset () {
            receivedEvents = new List<string> ();
            expectedEvents = new ExpectedEvent[] { };
        }

        public void HandleRequest (HttpListenerContext context) {
            Debug.Log (context.Request.Url.LocalPath);
            if (context.Request.Url.LocalPath == "/v0/native/overlay") {
                context.Response.StatusCode = 200;
                context.Response.AddHeader ("Content-Type", "text/html");
                context.Response.Close (System.Text.Encoding.UTF8.GetBytes (""), false);
                return;
            }
            StreamReader reader = new StreamReader (context.Request.InputStream);
            string body = reader.ReadToEnd ();
            JObject json = JObject.Parse (body);
            JArray events = json["events"] as JArray;
            foreach (JObject item in events.Children ()) {
                string eventName = item["event_name"].ToString ();
                receivedEvents.Add (eventName);
            }
            if (expectedEvents.Length > 0) {
                ExpectedEvent expected = null;
                foreach (ExpectedEvent e in expectedEvents) {
                    if (receivedEvents.IndexOf (e.eventName) >= 0) {
                        expected = e;
                        break;
                    }
                }
                if (expected != null) {
                    var responseJson = System.Text.Encoding.UTF8.GetBytes (expected.responseJson);
                    context.Response.StatusCode = 200;
                    context.Response.Close (responseJson, false);
                    return;
                }
            }

            var data = System.Text.Encoding.UTF8.GetBytes ("");
            context.Response.StatusCode = 200;
            context.Response.Close (data, false);
        }
    }
}