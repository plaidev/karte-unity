using UnityEngine;

namespace TestHelper {
    public class ExpectedEvent {
        public string eventName;
        public string responseJson;

        public ExpectedEvent (string eventName, string responseJson) {
            this.eventName = eventName;
            this.responseJson = responseJson;
        }
    }
}