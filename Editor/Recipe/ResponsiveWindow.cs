// ArchNet · Canvas Extension — Responsive UI tool (ArchNet Hub > Services > Responsive UI > Open tool)
// Preview the game on phones and tablets (Unity's Device Simulator), make a canvas of your own
// responsive, and choose which orientations the game allows on mobile.
// Docs: https://archnet-plateform.fr/#CanvasExtension

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArchNet.Extension.Editor
{
    public class ResponsiveWindow : EditorWindow
    {
        const string StylePath = "Packages/com.archnet.hub/Editor/UI/Hub.uss";

        public static void Open()
        {
            var window = GetWindow<ResponsiveWindow>();
            window.titleContent = new GUIContent("Responsive UI");
            window.minSize = new Vector2(420, 300);
        }

        void OnSelectionChange() => Rebuild();
        void OnFocus() => Rebuild();
        void CreateGUI() => Rebuild();

        void Rebuild()
        {
            VisualElement root = rootVisualElement;
            root.Clear();
            root.AddToClassList("archnet-hub");
            var style = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylePath);
            if (style != null) root.styleSheets.Add(style);
            root.style.paddingLeft = root.style.paddingRight = root.style.paddingTop = 10;

            var preview = new Button(() => EditorApplication.ExecuteMenuItem("Window/General/Device Simulator")) { text = "Preview on phones and tablets (Device Simulator)" };
            preview.AddToClassList("primary");
            root.Add(preview);
            root.Add(Intro("Then press Play and pick a device at the top of the Simulator tab; rotate it to check portrait and landscape."));

            var canvas = Selection.activeGameObject != null ? Selection.activeGameObject.GetComponentInParent<Canvas>(true) : null;
            var make = new Button(() =>
            {
                Undo.RegisterFullObjectHierarchyUndo(canvas.rootCanvas.gameObject, "Make canvas responsive");
                ResponsiveRecipe.MakeResponsive(canvas.rootCanvas);
                EditorUtility.SetDirty(canvas.rootCanvas.gameObject);
                Rebuild();
            }) { text = canvas != null ? $"Make \"{canvas.rootCanvas.name}\" responsive" : "Select a canvas of your own to make it responsive" };
            make.SetEnabled(canvas != null && canvas.rootCanvas.GetComponent<ResponsiveCanvas>() == null);
            root.Add(make);

            root.Add(Intro($"Mobile orientation now: {PlayerSettings.defaultInterfaceOrientation}."));
            root.Add(new Button(() => SetOrientation(true, true)) { text = "Allow portrait and landscape" });
            root.Add(new Button(() => SetOrientation(false, true)) { text = "Landscape only" });
            root.Add(new Button(() => SetOrientation(true, false)) { text = "Portrait only" });
        }

        static Label Intro(string text)
        {
            var label = new Label(text);
            label.AddToClassList("intro");
            label.style.marginTop = 8;
            return label;
        }

        static void SetOrientation(bool portrait, bool landscape)
        {
            PlayerSettings.defaultInterfaceOrientation = portrait && landscape ? UIOrientation.AutoRotation
                : landscape ? UIOrientation.LandscapeLeft : UIOrientation.Portrait;
            PlayerSettings.allowedAutorotateToPortrait = portrait;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
            PlayerSettings.allowedAutorotateToLandscapeLeft = landscape;
            PlayerSettings.allowedAutorotateToLandscapeRight = landscape;
            Debug.Log($"[ArchNet] Mobile orientation: {PlayerSettings.defaultInterfaceOrientation} (Project Settings > Player > Resolution and Presentation).");
        }
    }
}
