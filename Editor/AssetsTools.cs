using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    public static class AssetsTools
    {
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
                "Force Assets Re-Serialization",
                "This may be long operation despite of the size of your project, so be aware! There is no progress bar so it may looks like that Unity is frozen, but be patient, it's working. Proceed operation?",
                "Yes, go ahead!", "Cancel!"))
            {
                AssetDatabase.ForceReserializeAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}