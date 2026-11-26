using NUnit.Framework;
using UnityEngine;

namespace ArchNet.Extension.Tests
{
    public class ResponsiveTests
    {
        [TestCase(1170, 2532, 460f, true, DeviceKind.Phone)]    // 6.1" phone
        [TestCase(2048, 2732, 264f, true, DeviceKind.Tablet)]   // 12.9" tablet
        [TestCase(1080, 2400, 0f, true, DeviceKind.Phone)]      // no dpi, long screen
        [TestCase(1536, 2048, 0f, true, DeviceKind.Tablet)]     // no dpi, 4:3 screen
        [TestCase(1920, 1080, 96f, false, DeviceKind.Desktop)]
        public void TellsPhonesTabletsAndComputersApart(int width, int height, float dpi, bool mobile, DeviceKind expected) =>
            Assert.AreEqual(expected, DeviceInfo.Classify(width, height, dpi, mobile));

        [Test]
        public void TurnsTheSafeAreaIntoAnchors()
        {
            // A 1000×2000 portrait screen with a 100 px notch on top and a 50 px bar at the bottom.
            (Vector2 min, Vector2 max) = SafeArea.ToAnchors(new Rect(0, 50, 1000, 1850), new Vector2(1000, 2000));

            Assert.AreEqual(new Vector2(0f, 0.025f), min);
            Assert.AreEqual(new Vector2(1f, 0.95f), max);
        }

        [Test]
        public void AnUnknownScreenKeepsTheWholeArea()
        {
            (Vector2 min, Vector2 max) = SafeArea.ToAnchors(new Rect(0, 0, 10, 10), Vector2.zero);
            Assert.AreEqual(Vector2.zero, min);
            Assert.AreEqual(Vector2.one, max);
        }
    }
}
