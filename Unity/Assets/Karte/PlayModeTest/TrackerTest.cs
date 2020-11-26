using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Io.Karte;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class TrackerTest {
        [UnityTest]
        [Timeout (1000)]
        public IEnumerator TestView () {
            PlayModeTestHelper.StartMockServer ();

            Tracker.View ("main", null, null);

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("view");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }

        [UnityTest]
        [Timeout (1000)]
        public IEnumerator TestViewWithTitle () {
            PlayModeTestHelper.StartMockServer ();

            Tracker.View ("main", "title", null);

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("view");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }
/*
        [UnityTest]
        [Timeout (1000)]
        public IEnumerator TestViewWithValues () {
            PlayModeTestHelper.StartMockServer ();

            JObject values = new JObject ();
            values.Add ("value1", "foo");
            Tracker.View ("main", values);

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("view");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }
        */

        [UnityTest]
        [Timeout (1000)]
        public IEnumerator TestViewWithTitleAndValues () {
            PlayModeTestHelper.StartMockServer ();

            JObject values = new JObject ();
            values.Add ("value1", "foo");
            Tracker.View ("main", "title", values);

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("view");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }

        [Test]
        public void TestGetVisitorId () {
            string visitorId = App.GetVisitorId ();
            Assert.NotNull (visitorId);
            Assert.True (visitorId.Length > 0);
        }

        [UnityTest]
        [Timeout (1000)]
        public IEnumerator TestRegisterFCMToken () {
            PlayModeTestHelper.StartMockServer ();

            App.RegisterFCMToken ("fmctoken");

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("plugin_native_app_identify");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }

        [UnityTest]
        [Timeout (10000)]
        public IEnumerator TestIdentify () {
            PlayModeTestHelper.StartMockServer ();

            JObject userInfo = new JObject ();
            userInfo.Add ("str", "foo");
            Tracker.Identify (userInfo);

            yield return new WaitForSeconds (5);

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("identify");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }

        [UnityTest]
        [Timeout (1000)]
        public IEnumerator TestLogout () {
            PlayModeTestHelper.StartMockServer ();

            App.RenewVisitorId ();

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("plugin_native_app_identify");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }

    }
}