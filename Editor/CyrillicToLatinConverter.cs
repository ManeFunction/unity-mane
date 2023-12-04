using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    public static class CyrillicToLatinConverter
    {
        [MenuItem("GameObject/Convert Cyrillic names to Latin", true, 3000)]
        private static bool ConvertNamesChecker() => Selection.objects.Length > 0;

        
        [MenuItem("GameObject/Convert Cyrillic names to Latin", false, 3000)]
        private static void ConvertNames(MenuCommand menuCommand)
        {
            GameObject go = menuCommand.context as GameObject;
            if (!go) return;

            int totalObjects = CountChildren(go.transform);
            int processedObjects = 1;
            int namesChanged = 0;

            EditorUtility.DisplayProgressBar("Converting Names", "Processing...", 0);
            
            if (ReplaceCyrillicWithLatin(go.name, out string newName))
            {
                go.name = newName;
                namesChanged++;
                EditorUtility.SetDirty(go);
            }
            
            namesChanged += Convert(go.transform, ref processedObjects, totalObjects);

            EditorUtility.ClearProgressBar();
            Debug.Log($"Cyrillic names conversion complete. Total processed objects: {processedObjects}, " +
                      $"changed names: {namesChanged}");
        }

        private static readonly char[] CyrillicCharsMap =
        {
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у',
            'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я'
        };

        private static readonly char[] LatinCharsMap =
        {
            'a', 'b', 'v', 'g', 'd', 'e', 'e', 'z', '3', 'i', 'i', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u',
            'f', 'x', 'c', 'c', 's', 's', '\'', 'y', '\'', 'e', 'u', 'a'
        };

        private static int CountChildren(Transform parent) => 1 + parent.Cast<Transform>().Sum(CountChildren);

        private static int Convert(Transform parent, ref int processedObjects, int totalObjects)
        {
            int namesChanged = 0;
            foreach (Transform child in parent)
            {
                if (ReplaceCyrillicWithLatin(child.name, out string newName))
                {
                    child.name = newName;
                    namesChanged++;
                    EditorUtility.SetDirty(child.gameObject);
                }

                processedObjects++;

                EditorUtility.DisplayProgressBar("Converting Names", $"Processing {child.name}",
                    (float)processedObjects / totalObjects);

                namesChanged += Convert(child, ref processedObjects, totalObjects);
            }

            return namesChanged;
        }

        private static bool ReplaceCyrillicWithLatin(string input, out string result)
        {
            if (input.ToCharArray().All(c => !CyrillicCharsMap.Contains(c)))
            {
                result = null;
                return false;
            }

            result = string.Empty;
            foreach (char c in input)
            {
                int index = Array.IndexOf(CyrillicCharsMap, char.ToLower(c));
                if (index >= 0)
                {
                    char latinChar = LatinCharsMap[index];
                    result += char.IsUpper(c) ? char.ToUpper(latinChar) : latinChar;
                }
                else
                {
                    result += c;
                }
            }

            return true;
        }
    }
}