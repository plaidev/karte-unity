using System;
using System.Collections;
using System.Collections.Generic;
//using Io.Karte;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class VariableTest {
        /*
        [Test]
        public void TestDeserialization () {
            string serializedVar = "{\"campaignId\":\"campaignId\",\"shortenId\":\"shortenId\",\"value\":\"foo\"}";
            Variable var = JsonUtility.FromJson<Variable> (serializedVar);
            Assert.NotNull (var);
            Assert.AreEqual ("campaignId", var.CampaignId);
            Assert.AreEqual ("shortenId", var.ShortenId);
            Assert.AreEqual ("foo", var.GetString (""));
        }

        [Test]
        public void TestInt () {
            Variable variable = new Variable ("1", "", "");
            int i = variable.GetInt (999);
            Assert.AreEqual (1, i);
        }

        [Test]
        public void TestIntWithInvalidString () {
            Variable variable = new Variable ("a", "", "");
            int i = variable.GetInt (999);
            Assert.AreEqual (999, i);
        }

        [Test]
        public void TestDouble () {
            Variable variable = new Variable ("1.1", "", "");
            double d = variable.GetDouble (999.0);
            Assert.AreEqual (1.1, d);
        }

        [Test]
        public void TestDoubleWithInvalidString () {
            Variable variable = new Variable ("a", "", "");
            double d = variable.GetDouble (999.0);
            Assert.AreEqual (999.0, d);
        }

        [Test]
        public void TestBool () {
            Variable variable = new Variable ("true", "", "");
            bool b = variable.GetBool (false);
            Assert.AreEqual (true, b);
        }

        [Test]
        public void TestBoolWithInvalidString () {
            Variable variable = new Variable ("1", "", "");
            bool b = variable.GetBool (false);
            Assert.AreEqual (false, b);
        }

        [Test]
        public void TestArray () {
            Variable variable = new Variable ("[\"aaa\",\"bbb\"]", "", "");
            object[] arr = variable.GetJsonArray (null);
            Assert.NotNull (arr);
            Assert.AreEqual (2, arr.Length);
            Assert.AreEqual ("aaa", arr.GetValue (0) as string);
        }

        [Test]
        public void TestArrayWithInvalidJSON () {
            Variable variable = new Variable ("a", "", "");
            object[] arr = variable.GetJsonArray (null);
            Assert.Null (arr);
        }

        [Test]
        public void TestJsonObject () {
            Variable variable = new Variable ("{\"foo\": \"aaa\", \"bar\": {\"bbb\":\"ccc\"}, \"arr\": ['1','2','3']}", "", "");
            dynamic obj = variable.GetJsonObject (null);
            Assert.NotNull (obj);

            Assert.AreEqual ("aaa", (string) obj["foo"]);
            Assert.AreEqual ("aaa", (string) obj.foo);
            Assert.AreEqual ("ccc", (string) obj["bar"]["bbb"]);
            Assert.AreEqual ("1", (string) obj["arr"][0]);
            Assert.AreEqual (3, obj["arr"].Count);
        }

        [Test]
        public void TestJsonObjectWithInvalidJSON () {
            Variable variable = new Variable ("{\"foo\": \"aaa\", \"bar\": {\"bbb\":\"ccc\"}, \"arr\": ['1','2','3']", "", "");
            dynamic obj = variable.GetJsonObject (null);
            Assert.Null (obj);
        }

        [Test]
        public void TestSerializeArrayOfVariables () {
            Variable var1 = new Variable ("{\"foo\": \"aaa\", \"bar\": {\"bbb\":\"ccc\"}, \"arr\": ['1','2','3']}", "campaign1", "FOO");
            Variable var2 = new Variable ("{\"bar\": \"aaa\"}", "campaign2", "BAR");
            Variable[] variables = new Variable[] { var1, var2 };
            string serializedVariables = JsonConvert.SerializeObject (variables);
            Assert.NotNull (serializedVariables);
            Assert.AreNotEqual ("", serializedVariables);
        }

        [Test]
        public void TestSerializeArrayOfVariables2 () {
            Variable var1 = new Variable ("{\"foo\": \"aaa\", \"bar\": {\"bbb\":\"ccc\"}, \"arr\": ['1','2','3']}", "campaign1", "FOO");
            Variable var2 = new Variable ("{\"bar\": \"aaa\"}", "campaign2", "BAR");
            Variable[] variables = new Variable[] { var1, var2 };
            string serializedVariables = JsonConvert.SerializeObject (variables);
            Assert.NotNull (serializedVariables);
            Assert.AreNotEqual ("{}", serializedVariables);
        }

        [Test]
        public void TestDeserializeObject () {
            string json = "{\"app_info\":{ \"system_info\":{ \"bundle_id\":\"Io.Karte.sample\",\"os\":\"iOS\",\"device\":\"iPhone\",\"os_version\":\"12.2\",\"model\":\"x86_64\",\"idfv\":\"AD3C3DD6-5325-4AA7-961B-131A9E1FFC3D\" }, \"version_name\":\"0.1\",\"karte_sdk_version\":\"1.5.6\",\"version_code\":\"0\" }, \"visitor_id\":\"C1A32161-48C7-427E-8E5B-088CF7B0BB70\"}";
            Dictionary<string, string> dict = JsonUtility.FromJson<Dictionary<string, string>> (json);
            Assert.NotNull (dict);
            string[] keys = new string[] { "app_info", "version_name", "karte_sdk_version", "version_code" };
            foreach (string key in dict.Keys) {
                Assert.True (Array.IndexOf (keys, key) > 0);
            }
        }

        [Test]
        public void TestSerializeDictionaries () {
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
                    ""_service_action_type"": ""remote_config""
                }
            }
        ]
    }
}";
            Dictionary<string, string> dict = new Dictionary<string, string> () { { "event_name", "_fetch_variables" }, { "response", json }
            };
            Dictionary<string, string>[] events = { dict };
            string serializedEvents = JsonConvert.SerializeObject (events);
            Assert.NotNull (serializedEvents);
        }
        */
    }
}