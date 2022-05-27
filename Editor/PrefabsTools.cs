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

            if (GetSavingPopupOption())
                PrefabSaverSettings.ShowPopup(true).SaveButtonPressed += IterateObjects;
            else
                IterateObjects();


            void IterateObjects()
            {
                string path = GetPrefabsPath();
                EditorTools.CreateDirectoryFromAssetPath($"{path}a.prefab");
                
                foreach (GameObject gameObject in Selection.gameObjects)
                    gameObject.SaveToPrefab(path);
            }
        }

        public static void SaveToPrefab(this GameObject gameObject, string path,
            InteractionMode mode = InteractionMode.UserAction)
        {
            string localPath = $"{path}{gameObject.name}.prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, localPath, mode);
        }

        internal static string PrefabsPathKey => $"Mane.{Application.productName}.PrefabsPath";
        internal static string GetPrefabsPath() => EditorPrefs.GetString(PrefabsPathKey, "Assets/");
        
        internal static string SavingPopupKey => $"Mane.{Application.productName}.PathAsk"; 
        internal static bool GetSavingPopupOption() => EditorPrefs.GetBool(SavingPopupKey, true);
    }
}