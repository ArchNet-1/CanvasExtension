// ArchNet · Canvas Extension — safe area
// Put it on a full-screen RectTransform inside a Canvas: it shrinks to the part of the screen
// that notches, rounded corners and home bars leave free. Keep backgrounds outside it and
// buttons, texts and HUD inside. Follows rotations.
// Docs: https://archnet-plateform.fr/#CanvasExtension

using UnityEngine;

namespace ArchNet.Extension
{
    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu("ArchNet/Safe Area")]
    public class SafeArea : MonoBehaviour
    {
        Rect _applied;
        Vector2Int _screen;

        void OnEnable() => Apply();

        void Update()
        {
            if (Screen.safeArea != _applied || Screen.width != _screen.x || Screen.height != _screen.y) Apply();
        }

        void Apply()
        {
            _applied = Screen.safeArea;
            _screen = new Vector2Int(Screen.width, Screen.height);
            (Vector2 min, Vector2 max) = ToAnchors(_applied, _screen);
            var rect = (RectTransform)transform;
            rect.anchorMin = min;
            rect.anchorMax = max;
            rect.offsetMin = rect.offsetMax = Vector2.zero;
        }

        /// <summary>The safe area in pixels as anchors (0–1) of the whole screen.</summary>
        public static (Vector2 min, Vector2 max) ToAnchors(Rect safeArea, Vector2 screen)
        {
            if (screen.x <= 0f || screen.y <= 0f) return (Vector2.zero, Vector2.one);
            var min = new Vector2(safeArea.xMin / screen.x, safeArea.yMin / screen.y);
            var max = new Vector2(safeArea.xMax / screen.x, safeArea.yMax / screen.y);
            return (Vector2.Max(Vector2.zero, min), Vector2.Min(Vector2.one, max));
        }
    }
}
