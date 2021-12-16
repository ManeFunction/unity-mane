using Mane.Extensions;
using UnityEngine;
using UnityEditor;


namespace Mane.Editor
{
    public class ColorPicker : EditorWindow
    {
        [MenuItem("Mane/Color Picker %&C", false, 1000)]
        public static void ShowWindow() => GetWindow(typeof(ColorPicker)).titleContent = new GUIContent("Color Picker");

        [SerializeField] private Color _color = Color.white;

        private static GUIStyle _labelStyle;

        private void OnGUI()
        {
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleRight
                };
            }

            // title
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Color");
            _color = EditorGUILayout.ColorField(_color);
            EditorGUILayout.Space();

            // channel values
            _color.r = DrawChannel(_color.r, "R");
            _color.g = DrawChannel(_color.g, "G");
            _color.b = DrawChannel(_color.b, "B");
            _color.a = DrawChannel(_color.a, "A");

            // color to hex
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("HEX", _labelStyle, GUILayout.Width(30));
            string hex = EditorGUILayout.TextField(_color.ToHex());
            EditorGUILayout.EndHorizontal();

            // hex to color
            Color c;
            if (ColorUtility.TryParseHtmlString(hex, out c))
                _color = c;

            // c# script
            EditorGUILayout.Space();
            EditorGUILayout.TextField($"new Color({_color.r:n3}f, {_color.g:n3}f, {_color.b:n3}f, {_color.a:n3}f)");
        }

        private float DrawChannel(float value, string label)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, _labelStyle, GUILayout.Width(15));
            value = EditorGUILayout.FloatField(value);
            value = EditorGUILayout.IntField((int)(255 * value)) / 255f;
            EditorGUILayout.EndHorizontal();

            value = value.Clamp(0f, 1f);

            return value;
        }
    }
}