using System.Threading.Tasks;
using Io.Karte;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sample {
    public class Karte : MonoBehaviour {
        //private Firebase.FirebaseApp firebaseApp;

        void Start () {
            //SetupFirebase ();
            ShowAppKeyAndVisitorId ();

            InAppMessaging.OnOpenUrl += handleOpenUrl;
        }

        void handleOpenUrl(string url) {
            Debug.Log("handleOpenUrl: url=" + url);
        }

        void handleOpenUrlWithScene(string url, string sceneIdentifier) {
            Debug.Log("handleOpenUrlWithScene: url=" + url + ", sceneIdentifier=" + sceneIdentifier);
        }


        //プッシュ通知をテストする場合はコメントアウトを外してSetupFirebseを呼んでください。
        // private void SetupFirebase () {
        //     Firebase.FirebaseApp.CheckAndFixDependenciesAsync ().ContinueWith (task => {
        //         var dependencyStatus = task.Result;
        //         if (dependencyStatus == Firebase.DependencyStatus.Available) {
        //             // Create and hold a reference to your FirebaseApp,
        //             // where app is a Firebase.FirebaseApp property of your application class.
        //             firebaseApp = Firebase.FirebaseApp.DefaultInstance;

        //             // Set a flag here to indicate whether Firebase is ready to use by your app.
        //             Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
        //             Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
        //         } else {
        //             UnityEngine.Debug.LogError (System.String.Format (
        //                 "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        //             // Firebase Unity SDK is not safe to use here.
        //         }
        //     });
        // }
        // public void OnTokenReceived (object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
        //     Debug.Log ("token received:" + token.Token);
        //     App.RegisterFCMToken (token.Token);
        // }
        // public void OnMessageReceived (object sender, Firebase.Messaging.MessageReceivedEventArgs e) {
        //     if (e.Message != null) {
        //         Debug.Log ("message: " + e.Message);
        //         if (e.Message.Notification != null) {
        //             Debug.Log ("message.Title=" + e.Message.Notification.Title);
        //             Debug.Log ("message.Body=" + e.Message.Notification.Body);
        //         } else {
        //             Debug.Log ("e.Message.Notification is null");
        //         }
        //         string url = RemoteNotificationHandler.RetrieveURLFromUserInfo (e.Message.Data);
        //         Debug.Log ("url=" + url);
        //         bool handled = RemoteNotificationHandler.HandleRemoteNotification (e.Message.Data);
        //         Debug.Log ("handled=" + handled);
        //     } else {
        //         Debug.Log ("e.Message is null");
        //     }
        // }
        private void ShowAppKeyAndVisitorId () {
            Text visitorIdText = GetText ("VisitorIdText");
            string visitorId = App.GetVisitorId ();
            visitorIdText.text = "visitorId = " + visitorId;
        }
        public void OnViewClick () {
            InputField inputField = GetInputField ("ViewNameInputField1");
            string text = inputField.text;
            Tracker.View (text);
            inputField.text = "";
        }
        public void OnViewAndTitleClick () {
            InputField inputField = GetInputField ("ViewNameInputField2");
            InputField titleInputField = GetInputField ("TitleInputField");

            Tracker.View (inputField.text, titleInputField.text);
            inputField.text = "";
            titleInputField.text = "";
        }

        public void OnTrackWithValuesClick () {
            InputField keyInputField = GetInputField ("ValuesKeyInputField3");
            InputField valueInputField = GetInputField ("ValuesValueInputField3");
            JObject dict = new JObject ();
            dict.Add (keyInputField.text, valueInputField.text);

            InputField eventNameInputField = GetInputField ("EventNameInputField3");
            Tracker.Track (eventNameInputField.text, dict);
            keyInputField.text = "";
            valueInputField.text = "";
            eventNameInputField.text = "";
        }
        public void OnFetchVariablesClick () {
            Variables.FetchWithCompletion ((result) => {
                Text text = GetText ("FetchVariablesText");
                text.text = result ? "Fetch variables: success" : "Fetch variables: failed";
            });
        }

        public void OnGetVariableClick () {
            InputField inputField = GetInputField ("VariableNameInputField1");
            Variable var = Variables.GetVariable (inputField.text);
            Debug.Log ("Variable name =" + inputField.text + ", value=" + var.GetString (""));
        }

        public void OnTrackClickClick () {
            InputField variableNameInputField = GetInputField ("VariableNameInputField2");
            Variable var = Variables.GetVariable (variableNameInputField.text);
            Variables.TrackClick (new Variable[] { var });
            variableNameInputField.text = "";
        }

        public void OnTrackClickWithValueClick () {
            InputField inputField = GetInputField ("VariableNameInputField3");
            Variable var = Variables.GetVariable (inputField.text);

            InputField keyInputField = GetInputField ("ValuesKeyInputField1");
            InputField valueInputField = GetInputField ("ValuesValueInputField1");

            JObject dict = new JObject ();
            dict.Add (keyInputField.text, valueInputField.text);
            Variables.TrackClick (new Variable[] { var }, dict);
            inputField.text = "";
            keyInputField.text = "";
            valueInputField.text = "";
        }

        public void OnTrackOpenClick () {
            Debug.Log ("OnTrackOpen");
            InputField variableNameInputField = GetInputField ("TrackOpenVariableNameInputField1");
            Variable var = Variables.GetVariable (variableNameInputField.text);
            Variables.TrackOpen (new Variable[] { var });
            variableNameInputField.text = "";
        }

        public void OnTrackOpenWithValueClick () {
            Debug.Log ("OnTrackOpenWithValue");
            InputField inputField = GetInputField ("TrackOpenVariableNameINputField2");
            Variable var = Variables.GetVariable (inputField.text);

            InputField keyInputField = GetInputField ("TrackOpenKeyInputField");
            InputField valueInputField = GetInputField ("TrackOpenValueInputField");

            JObject dict = new JObject ();
            dict.Add (keyInputField.text, valueInputField.text);
            Variables.TrackOpen (new Variable[] { var }, dict);
            inputField.text = "";
            keyInputField.text = "";
            valueInputField.text = "";
        }

        public void OnIdentifyClick () {
            InputField keyInputField = GetInputField ("ValuesKeyInputField2");
            InputField valueInputField = GetInputField ("ValuesValueInputField2");

            JObject dict = new JObject ();
            dict.Add (keyInputField.text, valueInputField.text);
            Tracker.Identify (dict);
            keyInputField.text = "";
            valueInputField.text = "";
        }

        public void OnLogoutClick () {
            App.RenewVisitorId ();
        }

        public async void OnDismissClick () {
            await Task.Delay (10 * 1000);
            if (InAppMessaging.isPresenting) {
                InAppMessaging.dismiss ();
            }
        }

        public void onSupressClick () {
            InAppMessaging.suppress ();
        }

        public void onUnsupressClick () {
            InAppMessaging.unsuppress ();
        }

        private Text GetText (string objectName) {
            GameObject gameObject = GameObject.Find (objectName);
            return gameObject.GetComponent<Text> ();
        }

        private InputField GetInputField (string objectName) {
            GameObject gameObject = GameObject.Find (objectName);
            return gameObject.GetComponent<InputField> ();
        }
    }
}