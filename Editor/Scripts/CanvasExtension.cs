using UnityEngine;
using UnityEngine.UI;

namespace ArchNet.Extension.Canvases
{
    /// <summary>
    /// [EXTENSION] - [ARCH NET] - [CANVAS] Canvas Extension
    /// author : Louis PAKEL
    /// </summary>
    public static class CanvasExtension
    {
        private static int higherSortingOrder = 1;
        
        /// <summary>
        /// Put Canvas to higher layer (foreground)
        /// </summary>
        /// <param name="canvas"></param>
        public static void SetCanvasToForeground(this Canvas canvas)
        {
            if (canvas == null)
                return;

            canvas.overrideSorting = true;
            canvas.sortingOrder = higherSortingOrder;
            higherSortingOrder++;
        }

        /// <summary>
        /// Put Canvas to default layer (0)
        /// </summary>
        /// <param name="canvas"></param>
        public static void SetCanvasToDefault(this Canvas canvas)
        {
            if (canvas == null)
                return;
            
            canvas.overrideSorting = false;
            canvas.sortingOrder = 0;

            GraphicRaycaster graphicRaycaster = canvas.gameObject.GetComponent<GraphicRaycaster>();
            if (graphicRaycaster != null)
                Object.Destroy(graphicRaycaster);
        }

        
        /// <summary>
        /// Modify CanvasGroup to be invisible and not interactable
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void SetInvisible(this CanvasGroup canvasGroup)
        {
            if (canvasGroup == null)
                return;

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }


        /// <summary>
        /// Modify CanvasGroup to be visible and  interactable
        /// </summary>
        /// <param name="canvasGroup"></param>
        public static void SetVisible(this CanvasGroup canvasGroup)
        {
            if (canvasGroup == null)
                return;

            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// Refresh a full canvas layouts (included children)
        /// This can be useful for adjusting background of a Button if dynamic text change (localize, data etc)
        /// </summary>
        /// <param name="canvas"></param>
        public static void RefreshCanvas(this Canvas canvas)
        {
            if (canvas == null)
                return;

            RectTransform rectTransform = canvas.gameObject.GetComponent<RectTransform>();
            if (rectTransform)
                LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}
