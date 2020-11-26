using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using Newtonsoft.Json;
using Io.Karte;
using NUnit.Framework;
using TestHelper;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class VariablesTest {
        [UnityTest]
        [Timeout (1000)]
        public IEnumerator TestFetch () {
            PlayModeTestHelper.StartMockServer ();

            Variables.Fetch ();

            bool eventSent = false;
            while (true) {
                eventSent = PlayModeTestHelper.IsEventSent ("_fetch_variables");
                if (eventSent) {
                    break;
                }
                yield return null;
            }
            Assert.True (eventSent);

            PlayModeTestHelper.StopMockServer ();
        }

        [UnityTest]
        [Timeout (5000)]
        public IEnumerator TestFetchWithCompletion () {
            string json = @"
{
    ""response"": {
        ""messages"": [
            {
                ""action"": {
                    ""campaign_id"": ""sample_campaign"",
                    ""shorten_id"": ""sample_shorten"",
                    ""content"": {
                        ""inlined_variables"": [
                            {""name"": ""strvar"", ""value"": ""foo""}
                        ]
                    },
                    ""plugin_type"": ""remote_config""
                },
                ""campaign"": {
                    ""_id"": ""sample_campaign"",
                    ""service_action_type"": ""remote_config""
                }
            }
        ]
    }
}";
            ExpectedEvent e = new ExpectedEvent ("_fetch_variables", json);
            ExpectedEvent[] events = new ExpectedEvent[] { e };
            PlayModeTestHelper.StartMockServer (events);

            bool done = false;
            Variables.FetchWithCompletion ((result) => {
                Assert.True (result);
                Variable var = Variables.GetVariable ("strvar");
                Assert.AreEqual ("foo", var.GetString ("bar"));
                done = true;
            });

            bool eventSent = false;
            while (!eventSent || !done) {
                eventSent = PlayModeTestHelper.IsEventSent ("_message_ready");
                yield return null;
            }
            Assert.True (eventSent);
            Assert.True (done);

            PlayModeTestHelper.StopMockServer ();
        }
    }
}