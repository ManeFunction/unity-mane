using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    public static class PrefabReserialiser
    {
        [MenuItem("Assets/Force Re-serialise Asset(s)")]
        private static void ReserialiseAssets()
        {
            AssetDatabase.ForceReserializeAssets(Selection.objects
                .Select(AssetDatabase.GetAssetPath));

            foreach (Object obj in Selection.objects)
            {
                if (obj) EditorUtility.SetDirty(obj);
            }
        }
        
        [MenuItem("GameObject/Apply Transform Changes", false, -1)]
        private static void CleanPrefab()
        {
            foreach (Object obj in Selection.objects)
            {
                GameObject go = obj as GameObject;
                if (!go) return;
                
                SerializedObject so = new SerializedObject(go.transform);
                var i = so.GetIterator();
                while (i.Next(true))
                    PrefabUtility.ApplyPropertyOverride(i,
                        PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(go),
                        InteractionMode.UserAction);
                EditorUtility.SetDirty(obj);
            }
        }
    }
}