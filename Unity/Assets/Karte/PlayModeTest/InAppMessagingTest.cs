using Io.Karte;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine;

namespace Tests {
    public class InAppMessagingTest {

        [Test]
        public void TestIsPresenting () {
            bool isPresenting = InAppMessaging.isPresenting;
            Assert.False (isPresenting);
        }

        [Test]
        public void TestDismiss () {
            InAppMessaging.dismiss ();
        }

        [Test]
        public void TestOpenUrlCallback () {
            InAppMessaging.OnOpenUrl += handleOpenUrl;
            InAppMessaging.InvokeOpenUrlHandler("https://example.com/1");
            InAppMessaging.OnOpenUrl -= handleOpenUrl;
            InAppMessaging.InvokeOpenUrlHandler("https://example.com/2");
        }

        [Test]
        public void TestOpenUrlWithSceneCallback () {
            InAppMessaging.OnOpenUrlWithScene += HandleOpenUrlWithScene;
            InAppMessaging.InvokeOpenUrlWithSceneHandler("https://example.com/1", "scene1");
            InAppMessaging.OnOpenUrlWithScene -= HandleOpenUrlWithScene;
            InAppMessaging.InvokeOpenUrlWithSceneHandler("https://example.com/2", "scene2");
        }

        void handleOpenUrl(string url) {
            Debug.Log("handleOpenUrl: url=" + url);
        }

        void HandleOpenUrlWithScene(string url, string sceneIdentifier) {
            Debug.Log("handleOpenUrlWithScene: url,sceneIdentifier=" + url + "," + sceneIdentifier);
        }
    }
}