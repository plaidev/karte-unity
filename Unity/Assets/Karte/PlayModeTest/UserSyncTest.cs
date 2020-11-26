using System.Collections;
using System.Collections.Generic;
using Io.Karte;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests {
    public class UserSyncTest {

        [Test]
        public void TestAppendUserSyncQueryParameter () {
            string url = "http://www.example.com";
            string urlWithQueryParameter = UserSync.AppendUserSyncQueryParameter(url);
            int separatorIndex = urlWithQueryParameter.IndexOf ("?");
            string queryParameter = urlWithQueryParameter.Substring (separatorIndex + 1);
            string[] parameters = queryParameter.Split ('&');
            string[] keyValue = parameters[0].Split ('=');
            Assert.AreEqual ("_k_ntvsync_b", keyValue[0]);
        }
    }
}