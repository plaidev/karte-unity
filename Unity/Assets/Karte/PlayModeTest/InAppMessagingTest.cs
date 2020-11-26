using Io.Karte;
using NUnit.Framework;
using UnityEngine.TestTools;

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
    }
}