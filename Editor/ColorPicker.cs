using UnityEngine;
using UnityEditor;


namespace Mane.Extensions.Editor
{
    public class ColorPicker : EditorWindow
    {
        [MenuItem("Window/Color Picker %&C")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ColorPicker)).titleContent = new GUIContent("Color Picker");
        }

        [SerializeField] private Color color = Color.white;

        private static GUIStyle labelStyle;

        private void OnGUI()
        {
            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleRight
                };
            }

            // title
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Color");
            color = EditorGUILayout.ColorField(color);
            EditorGUILayout.Space();

            // channel values
            color.r = DrawChannel(color.r, "R");
            color.g = DrawChannel(color.g, "G");
            color.b = DrawChannel(color.b, "B");
            color.a = DrawChannel(color.a, "A");

            // color to hex
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("HEX", labelStyle, GUILayout.Width(30));
            string hex = EditorGUILayout.TextField(color.ToHex());
            EditorGUILayout.EndHorizontal();

            // hex to color
            Color c;
            if (ColorUtility.TryParseHtmlString(hex, out c))
                color = c;

            // c# script
            EditorGUILayout.Space();
            EditorGUILayout.TextField($"new Color({color.r:n3}f, {color.g:n3}f, {color.b:n3}f, {color.a:n3}f)");
        }

        private float DrawChannel(float value, string label)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, labelStyle, GUILayout.Width(15));
            value = EditorGUILayout.FloatField(value);
            value = EditorGUILayout.IntField((int)(255 * value)) / 255f;
            EditorGUILayout.EndHorizontal();

            value = value.Clamp(0f, 1f);

            return value;
        }
    }
}