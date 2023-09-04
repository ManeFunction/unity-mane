using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    [CustomEditor(typeof(ColorSchemeComponent), true)]
    public class ColorSchemeComponentEditor : UnityEditor.Editor
    {
        private ColorSchemeComponent _target;

        private SerializedProperty _graphics;
        private SerializedProperty _colorScheme;

        private MethodInfo _colorSetter;

        private void OnEnable()
        {
            _target = target as ColorSchemeComponent;

            _graphics = serializedObject.FindProperty("_graphics");
            _colorScheme = serializedObject.FindProperty("_colorScheme");

            _target.RefreshColorScheme();
        }

        public override void OnInspectorGUI()
        {
            if (!DrawGUI()) return;

            _target.RefreshColorScheme();
        }

        private bool DrawGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();

            EditorGUILayout.PropertyField(_colorScheme);

            if (_colorScheme.objectReferenceValue != null)
            {
                ColorScheme colorScheme = (ColorScheme)_colorScheme.objectReferenceValue;
                int colorCount = colorScheme.Length;

                EditorGUILayout.Space();

                while (_graphics.arraySize < colorCount)
                    _graphics.InsertArrayElementAtIndex(_graphics.arraySize);

                while (_graphics.arraySize > colorCount)
                    _graphics.DeleteArrayElementAtIndex(_graphics.arraySize - 1);

                for (int i = 0; i < colorCount; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUI.BeginChangeCheck();
                    Color color = EditorGUILayout.ColorField(GUIContent.none, colorScheme[i],
                        true, true, false, GUILayout.Width(40f));
                    if (EditorGUI.EndChangeCheck())
                    {
                        Undo.RecordObject(_colorScheme.objectReferenceValue, "Change Color");
                        SetColor(i, color);
                        EditorUtility.SetDirty(_colorScheme.objectReferenceValue);
                    }

                    var graphics = _graphics.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(graphics, GUIContent.none);

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(1f);
                }
            }

            return serializedObject.ApplyModifiedProperties();
        }

        private void SetColor(int i, Color color)
        {
            if (_colorSetter == null)
                _colorSetter =
                    typeof(ColorScheme).GetMethod("SetColor", BindingFlags.Instance | BindingFlags.NonPublic);

            _colorSetter.Invoke(_target, new object[] { i, color });
        }
    }
}