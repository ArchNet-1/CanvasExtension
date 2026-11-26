// ArchNet · Canvas Extension — Responsive UI recipe (ArchNet Hub > Services > Responsive UI)
// Makes every ArchNet screen fit phones, tablets and computers: each canvas of the prefabs and
// scenes under Assets/ArchNet gets a ResponsiveCanvas (whole design visible on any screen
// shape, larger UI on phones and tablets) and a "Safe Area" holding its content, away from
// notches and rounded corners (backgrounds stay full screen). Run it again (Update) after
// adding screens; canvases already done are skipped.
// This assembly only compiles when the ArchNet Hub (com.archnet.hub 2.0.0+) is installed.
// Docs: https://archnet-plateform.fr/#CanvasExtension

using System.Collections.Generic;
using System.Linq;
using ArchNet.Installer.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArchNet.Extension.Editor
{
    public sealed class ResponsiveRecipe : ServiceRecipe
    {
        public const string RecipeId = "responsive";

        public override string Id => RecipeId;
        public override string DisplayName => "Responsive UI";
        public override string Description => "Every ArchNet screen fits phones, tablets and computers, in portrait and landscape, away from notches. Click Update after adding screens.";
        public override string Category => "UI";
        public override string FolderName => "Responsive";
        public override bool HasTool => true;

        public override void Create(RecipeContext context)
        {
            int done = 0;
            foreach (string guid in AssetDatabase.FindAssets("t:Prefab", new[] { RecipeRunner.RootFolder }))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                GameObject root = PrefabUtility.LoadPrefabContents(path);
                try
                {
                    if (!MakeAllResponsive(root)) continue;
                    PrefabUtility.SaveAsPrefabAsset(root, path);
                    ArchNetLog.Info($"Responsive: {path}.");
                    done++;
                }
                finally
                {
                    PrefabUtility.UnloadPrefabContents(root);
                }
            }
            foreach (string guid in AssetDatabase.FindAssets("t:Scene", new[] { RecipeRunner.RootFolder }))
                if (MakeSceneResponsive(AssetDatabase.GUIDToAssetPath(guid))) done++;
            ArchNetLog.Info(done == 0 ? "Responsive: every ArchNet screen already fits." : $"Responsive: {done} prefabs and scenes updated.");
        }

        public override void OpenTool() => ResponsiveWindow.Open();

        static bool MakeSceneResponsive(string path)
        {
            Scene scene = SceneManager.GetSceneByPath(path);
            bool wasOpen = scene.isLoaded;
            if (wasOpen && scene.isDirty)
            {
                ArchNetLog.Warning($"Responsive: {path} has unsaved changes and was skipped.", "Save the scene, then click Update on Responsive UI.");
                return false;
            }
            if (!wasOpen) scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
            try
            {
                bool changed = false;
                foreach (GameObject root in scene.GetRootGameObjects()) changed |= MakeAllResponsive(root);
                if (changed)
                {
                    EditorSceneManager.SaveScene(scene);
                    ArchNetLog.Info($"Responsive: {path}.");
                }
                return changed;
            }
            finally
            {
                if (!wasOpen) EditorSceneManager.CloseScene(scene, true);
            }
        }

        /// <summary>Every root canvas under this object. Returns true when something changed.</summary>
        public static bool MakeAllResponsive(GameObject root)
        {
            bool changed = false;
            // A canvas inside a nested prefab (the screens under the Bootstrap) is handled in its own prefab.
            foreach (Canvas canvas in root.GetComponentsInChildren<Canvas>(true)
                         .Where(c => (c.isRootCanvas || c.transform.parent == null) && !PrefabUtility.IsPartOfPrefabInstance(c)))
                changed |= MakeResponsive(canvas);
            return changed;
        }

        public static bool MakeResponsive(Canvas canvas)
        {
            bool changed = false;
            if (canvas.TryGetComponent(out CanvasScaler scaler) && !canvas.TryGetComponent(out ResponsiveCanvas _))
            {
                canvas.gameObject.AddComponent<ResponsiveCanvas>();
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
                changed = true;
            }
            if (canvas.GetComponentInChildren<SafeArea>(true) != null) return changed;

            // Everything but full-screen backgrounds moves into a Safe Area container.
            List<Transform> content = canvas.transform.Cast<Transform>().Where(child => !IsBackground(child)).ToList();
            if (content.Count == 0) return changed;
            var safe = new GameObject("Safe Area", typeof(RectTransform), typeof(SafeArea)).GetComponent<RectTransform>();
            safe.SetParent(canvas.transform, false);
            safe.anchorMin = Vector2.zero;
            safe.anchorMax = Vector2.one;
            safe.offsetMin = safe.offsetMax = Vector2.zero;
            safe.SetSiblingIndex(content.Min(child => child.GetSiblingIndex()));
            foreach (Transform child in content) child.SetParent(safe, false);
            return true;
        }

        static bool IsBackground(Transform child) =>
            child is RectTransform rect && rect.anchorMin == Vector2.zero && rect.anchorMax == Vector2.one &&
            rect.offsetMin == Vector2.zero && rect.offsetMax == Vector2.zero &&
            child.TryGetComponent(out Graphic _) && child.childCount == 0;
    }
}
