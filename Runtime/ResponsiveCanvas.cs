// ArchNet · Canvas Extension — responsive canvas
// Put it next to a CanvasScaler: the whole design always fits the screen (portrait or
// landscape, any shape), and phones and tablets get a larger UI so texts stay readable.
// Docs: https://archnet-plateform.fr/#CanvasExtension

using System;
using UnityEngine;
using UnityEngine.UI;

namespace ArchNet.Extension
{
    [RequireComponent(typeof(CanvasScaler))]
    [AddComponentMenu("ArchNet/Responsive Canvas")]
    public class ResponsiveCanvas : MonoBehaviour
    {
        [Tooltip("How much larger the UI is on phones (1 = same size as on a computer).")]
        [SerializeField, Range(1f, 2f)] float _phoneScale = 1.3f;
        [Tooltip("How much larger the UI is on tablets.")]
        [SerializeField, Range(1f, 2f)] float _tabletScale = 1.1f;

        CanvasScaler _scaler;
        Vector2 _designResolution;
        Vector2Int _screen;

        /// <summary>Raised when the device kind or orientation was applied (rotation, window resize).</summary>
        public event Action<DeviceKind, bool> Changed;

        void Awake()
        {
            _scaler = GetComponent<CanvasScaler>();
            _designResolution = _scaler.referenceResolution;
        }

        void OnEnable() => _screen = Vector2Int.zero;

        void Update()
        {
            if (Screen.width == _screen.x && Screen.height == _screen.y) return;
            _screen = new Vector2Int(Screen.width, Screen.height);
            DeviceKind kind = DeviceInfo.Kind;
            float scale = kind == DeviceKind.Phone ? _phoneScale : kind == DeviceKind.Tablet ? _tabletScale : 1f;
            _scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            _scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            _scaler.referenceResolution = _designResolution / scale;
            Changed?.Invoke(kind, DeviceInfo.IsPortrait);
        }
    }
}
