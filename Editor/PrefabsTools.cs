using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Mane.Editor
{
    public static class PrefabsTools
    {
        [MenuItem("GameObject/Prefab/Apply Prefab(s) Changes", true, 1500)]
        private static bool ApplyPrefabsChecker() => IsPrefabSelected();

        [MenuItem("GameObject/Prefab/Apply Prefab(s) Changes", false, 1500)]
        private static void ApplyPrefabs(MenuCommand menuCommand)
        {
            GameObject obj = menuCommand.context as GameObject;
            if (!obj) return;
            
            ApplyChanges(obj);
        }

        [MenuItem("GameObject/Prefab/Apply Prefab(s) Transform Changes", true, 1501)]
        private static bool ApplyPrefabsTransformChecker() => IsPrefabSelected();
        
        [MenuItem("GameObject/Prefab/Apply Prefab(s) Transform Changes", false, 1501)]
        private static void ApplyPrefabsTransform(MenuCommand menuCommand)
        {
            GameObject obj = menuCommand.context as GameObject;
            if (!obj) return;
            
            ApplyTransformChanges(obj);
        }

        [MenuItem("GameObject/Prefab/Apply Prefab(s) Changes (+Transform)", true, 1502)]
        private static bool ApplyPrefabsAllChecker() => IsPrefabSelected();
        
        [MenuItem("GameObject/Prefab/Apply Prefab(s) Changes (+Transform)", false, 1502)]
        private static void ApplyPrefabsAll(MenuCommand menuCommand)
        {
            GameObject obj = menuCommand.context as GameObject;
            if (!obj) return;
            
            ApplyChanges(obj);
            ApplyTransformChanges(obj);
        }
        
        private static bool IsPrefabSelected() => 
            Selection.objects.Any(o => o != null && PrefabUtility.IsPartOfPrefabInstance(o));
        

        private static void ApplyChanges(GameObject gameObject)
        {
            if (!gameObject) return;
            
            PrefabUtility.ApplyPrefabInstance(gameObject, InteractionMode.UserAction);
            EditorUtility.SetDirty(gameObject);
        }

        private static void ApplyTransformChanges(GameObject gameObject)
        {
            if (!gameObject) return;
            
            SerializedObject so = new SerializedObject(gameObject.transform);
            string path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);
            PrefabUtility.ApplyPropertyOverride(so.FindProperty("m_LocalRotation"),
                path, InteractionMode.UserAction);
            PrefabUtility.ApplyPropertyOverride(so.FindProperty("m_LocalPosition"),
                path, InteractionMode.UserAction);
            EditorUtility.SetDirty(gameObject);
        }
        

        [MenuItem("GameObject/Prefab/Save to Prefab(s)", true, 1000)]
        private static bool ValidateCreatePrefab() => Selection.activeGameObject != null 
            && !EditorUtility.IsPersistent(Selection.activeGameObject);

        [MenuItem("GameObject/Prefab/Save to Prefab(s)", false, 1000)]
        private static void CreatePrefabs(MenuCommand menuCommand)
        {
            if (Selection.objects.Length > 1 && menuCommand.context != Selection.objects[0])
                return;

            if (GetSavingPopupOption() || GetPrefabsPathGuid() == null)
                PrefabSaverSettings.ShowPopup(true).SaveButtonPressed += IterateObjects;
            else
                IterateObjects();


            void IterateObjects()
            {
                GUID? guid = GetPrefabsPathGuid();
                if (guid == null) return;

                string path = AssetDatabase.GUIDToAssetPath(guid.Value);
                
                foreach (GameObject gameObject in Selection.gameObjects)
                    gameObject.SaveToPrefab(path);
            }
        }

        /// <summary>
        /// Saves the specified GameObject as a prefab at the given path.
        /// </summary>
        /// <param name="gameObject">The GameObject to save as a prefab.</param>
        /// <param name="path">The directory path where the prefab should be saved.</param>
        /// <param name="mode">The interaction mode for how Unity should handle errors or validation checks. Defaults to UserAction.</param>
        public static void SaveToPrefab(this GameObject gameObject, string path, InteractionMode mode = InteractionMode.UserAction)
        {
            // Generate the full path for the new prefab, ensuring it is unique to avoid overwriting existing files.
            string localPath = $"{path}/{gameObject.name}.prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            // Save the GameObject as a new prefab asset and connect it to the prefab.
            PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, mode);
        }

        internal static string PrefabsPathKey => $"Mane.{Application.productName}.PrefabsPathGuid";
        internal static GUID? GetPrefabsPathGuid()
        {
            string saved = EditorPrefs.GetString(PrefabsPathKey, string.Empty);
            return string.IsNullOrEmpty(saved) ? (GUID?)null : new GUID(saved);
        }

        internal static string SavingPopupKey => $"Mane.{Application.productName}.AskPath"; 
        internal static bool GetSavingPopupOption() => EditorPrefs.GetBool(SavingPopupKey, true);
    }
}