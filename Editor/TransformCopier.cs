using Mane.Extensions;
using UnityEditor;
using UnityEngine;

namespace Mane.Inspector
{
    public class TransformCopier : EditorWindow
    {
        [MenuItem("CONTEXT/Transform/Copy Global Values", false, 1000)]
        private static void SaveMenu(MenuCommand command)
        {
            Transform target = command.context as Transform;

            Vector3 p = target.position;
            Quaternion r = target.rotation;
            GUIUtility.systemCopyBuffer = $"{p.x}:{p.y}:{p.z}:{r.x}:{r.y}:{r.z}:{r.w}";
        }

        
        [MenuItem("CONTEXT/Transform/Paste Global Values", true, 1001)]
        private static bool PasteMenuValidate(MenuCommand command) =>
            !string.IsNullOrWhiteSpace(GUIUtility.systemCopyBuffer) &&
            GetValidClipboard().Length == 7;

        [MenuItem("CONTEXT/Transform/Paste Global Values", false, 1001)]
        private static void PasteMenu(MenuCommand command)
        {
            Transform target = command.context as Transform;
            string[] buffer = GetValidClipboard();

            target.position = new Vector3(
                buffer[0].ParseFloat(),
                buffer[1].ParseFloat(),
                buffer[2].ParseFloat());

            target.rotation = new Quaternion(
                buffer[3].ParseFloat(),
                buffer[4].ParseFloat(),
                buffer[5].ParseFloat(),
                buffer[6].ParseFloat());
            
            EditorUtility.SetDirty(target);
        }

        
        private static string[] GetValidClipboard() =>
            GUIUtility.systemCopyBuffer.Split(':');
    }
}