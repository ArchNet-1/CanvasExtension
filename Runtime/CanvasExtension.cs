// ArchNet · Canvas Extension
// Extension methods for Canvas and CanvasGroup: bring a canvas to the front, reset its
// sorting, show or hide a group, rebuild a layout after its content changed.
// Docs: https://archnet-plateform.fr/#CanvasExtension

using UnityEngine;
using UnityEngine.UI;

namespace ArchNet.Extension
{
    public static class CanvasExtension
    {
        // Each BringToFront call goes above the previous one.
        static int _frontOrder = 1;

        /// <summary>Draws this canvas above every canvas brought to the front before it.</summary>
        public static void BringToFront(this Canvas canvas)
        {
            if (canvas == null) return;
            canvas.overrideSorting = true;
            canvas.sortingOrder = _frontOrder++;
        }

        /// <summary>Gives the canvas back its parent's sorting order.</summary>
        public static void ResetSorting(this Canvas canvas)
        {
            if (canvas == null) return;
            canvas.overrideSorting = false;
            canvas.sortingOrder = 0;
        }

        /// <summary>Hides the group and stops it from receiving clicks.</summary>
        public static void Hide(this CanvasGroup group) => group.SetVisible(false);

        /// <summary>Shows the group and lets it receive clicks.</summary>
        public static void Show(this CanvasGroup group) => group.SetVisible(true);

        public static void SetVisible(this CanvasGroup group, bool visible)
        {
            if (group == null) return;
            group.alpha = visible ? 1f : 0f;
            group.interactable = visible;
            group.blocksRaycasts = visible;
        }

        /// <summary>
        /// Rebuilds the layout of the canvas and its children now, e.g. after a text changed
        /// size and its background must follow.
        /// </summary>
        public static void RebuildLayout(this Canvas canvas)
        {
            if (canvas != null && canvas.transform is RectTransform rect)
                LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
        }
    }
}
