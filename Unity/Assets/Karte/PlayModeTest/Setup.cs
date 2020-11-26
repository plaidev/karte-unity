using System.IO;
using NUnit.Framework;
using UnityEngine;

namespace Tests {
    [TestFixture]
    [SetUpFixture]
    public class Setup : MonoBehaviour {

        [OneTimeSetUp]
        public void OnceBeforeRunTest () { }

        [OneTimeTearDown]
        public void OnceAfterRunTest () { }
    }
}