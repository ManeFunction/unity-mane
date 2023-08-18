using System.Globalization;
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

        private string _hexText;
        private string _codeText;
        
        private string _hue;
        private int _hueInt;
        
        private string _saturation;
        private int _saturationInt;
        
        private string _brightness;
        private int _brightnessInt;

        private string _light;
        private int _lightInt;

        private static GUIStyle _labelStyle;
        
        private void OnEnable()
        {
            _hexText = _color.ToHex();
            UpdateReadOnlyFields();
        }

        private void OnGUI()
        {
            if (_labelStyle == null)
            {
                _labelStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleRight
                };
            }

            Color newColor = _color;
            
            // check clipboard
            string buffer = EditorGUIUtility.systemCopyBuffer;
            if (!string.IsNullOrWhiteSpace(buffer))
            {
                if (ColorUtility.TryParseHtmlString(buffer, out Color color))
                    newColor = color;
                else if ((buffer.Length == 6 || buffer.Length == 8) &&
                         ColorUtility.TryParseHtmlString("#" + buffer, out color))
                    newColor = color;
            }
            
            if (CheckColorChanged(newColor)) return;

            // title
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Color");
            newColor = EditorGUILayout.ColorField(_color);
            EditorGUILayout.Space();

            // channel values
            newColor.r = DrawChannel(newColor.r, "R");
            newColor.g = DrawChannel(newColor.g, "G");
            newColor.b = DrawChannel(newColor.b, "B");
            newColor.a = DrawChannel(newColor.a, "A");
            
            if (CheckColorChanged(newColor)) return;

            // color to hex
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("HEX", _labelStyle, GUILayout.Width(30f));
            string newHex = EditorGUILayout.TextField(_hexText);
            EditorGUILayout.EndHorizontal();
            
            if (CheckHexChanged(newHex)) return;
            
            // color components
            EditorGUILayout.Space();
            DrawReadOnlyField("Hue", _hue, _hueInt);
            DrawReadOnlyField("Saturation", _saturation, _saturationInt);
            DrawReadOnlyField("Brightness", _brightness, _brightnessInt);
            
            EditorGUILayout.Space();
            DrawReadOnlyField("Light", _light, _lightInt);

            // c# script
            EditorGUILayout.Space();
            EditorGUILayout.TextField(_codeText);
        }

        private float DrawChannel(float value, string label)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, _labelStyle, GUILayout.Width(15f));
            value = EditorGUILayout.FloatField(value);
            value = EditorGUILayout.IntField((int)(255 * value)) / 255f;
            EditorGUILayout.EndHorizontal();

            value = value.Clamp(0f, 1f);

            return value;
        }
        
        private void DrawReadOnlyField(string label, string value, int intValue)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, _labelStyle, GUILayout.Width(65f));
            EditorGUILayout.TextField(value);
            EditorGUILayout.IntField(intValue);
            EditorGUILayout.EndHorizontal();
        }

        private bool CheckColorChanged(Color color)
        {
            if (_color == color)
                return false;

            _color = color;
            _hexText = _color.ToHex();
            
            UpdateReadOnlyFields();

            return true;
        }

        private bool CheckHexChanged(string hex)
        {
            if (_hexText == hex)
                return false;

            if (ColorUtility.TryParseHtmlString(hex, out Color color))
            {
                _color = color;
                _hexText = hex;
                
                UpdateReadOnlyFields();
            }
            else
                _hexText = _color.ToHex();

            return true;
        }

        private void UpdateReadOnlyFields()
        {
            _codeText = GetCodeText();
            
            _hueInt = _color.GetHue();
            _hue = (_hueInt / 360f).ToString(CultureInfo.InvariantCulture);
            
            float saturation = _color.GetSaturation();
            _saturation = saturation.ToString(CultureInfo.InvariantCulture);
            _saturationInt = (int)(saturation * 100);
            
            float brightness = _color.GetBrightness();
            _brightness = brightness.ToString(CultureInfo.InvariantCulture);
            _brightnessInt = (int)(brightness * 255);
            
            float light = _color.GetLight();
            _light = light.ToString(CultureInfo.InvariantCulture);
            _lightInt = (int)(light * 100);
        }

        private string GetCodeText() => $"new Color({_color.r:n3}f, {_color.g:n3}f, {_color.b:n3}f, {_color.a:n3}f)";
    }
}