// ArchNet · Canvas Extension — device info
// What kind of screen the game runs on: phone, tablet or desktop (a computer or a console),
// and its orientation. Phones and tablets are told apart by the screen's size in inches.
// Docs: https://archnet-plateform.fr/#CanvasExtension

using UnityEngine;

namespace ArchNet.Extension
{
    public enum DeviceKind { Phone, Tablet, Desktop }

    public static class DeviceInfo
    {
        /// <summary>Screens of this diagonal or more count as tablets.</summary>
        public const float TabletInches = 7f;

        public static DeviceKind Kind => Classify(Screen.width, Screen.height, Screen.dpi, Application.isMobilePlatform);

        public static bool IsPortrait => Screen.height > Screen.width;

        public static DeviceKind Classify(int width, int height, float dpi, bool mobile)
        {
            if (!mobile) return DeviceKind.Desktop;
            if (width <= 0 || height <= 0) return DeviceKind.Phone;
            if (dpi <= 0f) // unknown density: long, thin screens are phones
                return Mathf.Max(width, height) / (float)Mathf.Min(width, height) >= 1.7f ? DeviceKind.Phone : DeviceKind.Tablet;
            float inches = Mathf.Sqrt(width * (float)width + height * (float)height) / dpi;
            return inches >= TabletInches ? DeviceKind.Tablet : DeviceKind.Phone;
        }
    }
}
