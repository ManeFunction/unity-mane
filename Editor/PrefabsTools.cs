using System.Linq;
using UnityEngine;
using UnityEditor;


namespace Mane.Editor
{
    public static class PrefabsTools
    {
        [MenuItem("GameObject/Apply Prefab(s) Changes", true, -10)]
        private static bool ApplyPrefabsChecker() => IsPrefabSelected();

        [MenuItem("GameObject/Apply Prefab(s) Changes", false, -10)]
        private static void ApplyPrefabs(MenuCommand menuCommand)
        {
            GameObject obj = GetNextSelectedObject(menuCommand);
            if (!obj) return;
            
            ApplyChanges(obj);
        }

        [MenuItem("GameObject/Apply Prefab(s) Transform Changes", true, -9)]
        private static bool ApplyPrefabsTransformChecker() => IsPrefabSelected();
        
        [MenuItem("GameObject/Apply Prefab(s) Transform Changes", false, -9)]
        private static void ApplyPrefabsTransform(MenuCommand menuCommand)
        {
            GameObject obj = GetNextSelectedObject(menuCommand);
            if (!obj) return;
            
            ApplyTransformChanges(obj);
        }

        [MenuItem("GameObject/Apply Prefab(s) Changes (+Transform)", true, -8)]
        private static bool ApplyPrefabsAllChecker() => IsPrefabSelected();
        
        [MenuItem("GameObject/Apply Prefab(s) Changes (+Transform)", false, -8)]
        private static void ApplyPrefabsAll(MenuCommand menuCommand)
        {
            GameObject obj = GetNextSelectedObject(menuCommand);
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
        

        [MenuItem("GameObject/Save to Prefab(s)", true, -20)]
        private static bool ValidateCreatePrefab() => Selection.activeGameObject != null 
            && !EditorUtility.IsPersistent(Selection.activeGameObject);

        [MenuItem("GameObject/Save to Prefab(s)", false, -20)]
        private static void CreatePrefabs(MenuCommand menuCommand)
        {
            GameObject obj = GetNextSelectedObject(menuCommand);
            if (!obj) return;
            
            string localPath = GetPrefabsPath() + obj.name + ".prefab";
            EditorTools.CreateDirectoryFromAssetPath(localPath);
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            
            PrefabUtility.SaveAsPrefabAssetAndConnect(obj, localPath, InteractionMode.UserAction);
        }


        private static GameObject GetNextSelectedObject(MenuCommand menuCommand) =>
            Selection.gameObjects.FirstOrDefault(o => o == menuCommand.context);
        
        internal static string PrefsKey => $"Mane.PrefabsPath.{Application.productName}"; 

        internal static string GetPrefabsPath() => EditorPrefs.GetString(PrefsKey, "Assets/");
    }
}