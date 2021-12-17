using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    public static class AssetsTools
    {
        [MenuItem("GameObject/Apply Prefab(s) Changes", true, -10)]
        private static bool ApplyPrefabsChecker() => IsPrefabSelected();

        [MenuItem("GameObject/Apply Prefab(s) Changes", false, -10)]
        private static void ApplyPrefabs()
        {
            foreach (Object obj in Selection.objects)
                ApplyChanges(obj as GameObject);
        }

        [MenuItem("GameObject/Apply Prefab(s) Transform Changes", true, -9)]
        private static bool ApplyPrefabsTransformChecker() => IsPrefabSelected();
        
        [MenuItem("GameObject/Apply Prefab(s) Transform Changes", false, -9)]
        private static void ApplyPrefabsTransform()
        {
            foreach (Object obj in Selection.objects)
                ApplyTransformChanges(obj as GameObject);
        }

        [MenuItem("GameObject/Apply Prefab(s) Changes (+Transform)", true, -8)]
        private static bool ApplyPrefabsAllChecker() => IsPrefabSelected();
        
        [MenuItem("GameObject/Apply Prefab(s) Changes (+Transform)", false, -8)]
        private static void ApplyPrefabsAll()
        {
            foreach (Object obj in Selection.objects)
            {
                ApplyChanges(obj as GameObject);
                ApplyTransformChanges(obj as GameObject);
            }
        }
        
        private static bool IsPrefabSelected() => 
            Selection.objects.Any(o => o != null && PrefabUtility.IsPartOfPrefabInstance(o));
        
        
        [MenuItem("Assets/Force Re-serialise Asset(s)", false, 45)]
        private static void SaveAssets()
        {
            AssetDatabase.ForceReserializeAssets(Selection.objects
                .Select(AssetDatabase.GetAssetPath));

            foreach (Object obj in Selection.objects)
                if (obj) EditorUtility.SetDirty(obj);
        }
        
        [MenuItem("Mane/Force Reserialize All Assets", false, 1100)]
        private static void ForceSaveAssets()
        {
            if (EditorUtility.DisplayDialog(
                "Force Assets Reserialization",
                "This may be long operation despite of the size of your project, so be aware! There is no progress bar so it may looks like that Unity is frozen, but be patient, it's working. Proceed operation?",
                "Yes, go ahead!", "Cancel!"))
            {
                AssetDatabase.ForceReserializeAssets();
                AssetDatabase.Refresh();
            }
        }


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
    }
}