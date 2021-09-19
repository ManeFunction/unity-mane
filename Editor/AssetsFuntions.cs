using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    public static class PrefabReserialiser
    {
        [MenuItem("Assets/Force Re-serialise Asset(s)", false, 45)]
        private static void ReserialiseAssets()
        {
            AssetDatabase.ForceReserializeAssets(Selection.objects
                .Select(AssetDatabase.GetAssetPath));

            foreach (Object obj in Selection.objects)
            {
                if (obj) EditorUtility.SetDirty(obj);
            }
        }
        
        [MenuItem("GameObject/Apply Prefab(s) Changes", false, -10)]
        private static void ApplyPrefabs()
        {
            foreach (Object obj in Selection.objects)
                ApplyChanges(obj as GameObject);
        }
        
        [MenuItem("GameObject/Apply Prefab(s) Transform Changes", false, -9)]
        private static void ApplyPrefabsTransform()
        {
            foreach (Object obj in Selection.objects)
                ApplyTransformChanges(obj as GameObject);
        }

        [MenuItem("GameObject/Apply Prefab(s) Changes (+Transform)", false, -8)]
        private static void ApplyPrefabsAll()
        {
            foreach (Object obj in Selection.objects)
            {
                ApplyChanges(obj as GameObject);
                ApplyTransformChanges(obj as GameObject);
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