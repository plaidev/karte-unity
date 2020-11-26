using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using NUnit.Framework;
using TestHelper;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class PlayModeTestHelper {
        private static MockServer mockServer;
#pragma warning disable 414
        private static bool isServerRunninng = false;

        private static GameObject mockServerObject;
        public static void StartMockServer (ExpectedEvent[] events = null) {
            if (!isServerRunninng) {
                isServerRunninng = true;
                mockServerObject = new GameObject ("mockServerObject");
                mockServerObject.AddComponent<MockServer> ();
            }
            if (events != null) {
                mockServerObject.GetComponent<MockServer> ().ExpectEvents (events);
            }
        }

        public static void StopMockServer () {
            mockServerObject.GetComponent<MockServer> ().Reset ();
        }

        public static bool IsEventSent (string e) {
            string[] events = new string[] { e };
            return IsEventSent (events);
        }

        public static bool IsEventSent (string[] events) {
            string[] receivedEvents;
            receivedEvents = mockServerObject.GetComponent<MockServer> ().ReceivedEvents ();

            foreach (var e in events) {
                if (Array.IndexOf (receivedEvents, e) < 0) {
                    return false;
                }
            }
            return true;
        }
    }
}