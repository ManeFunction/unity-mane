using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    [CustomEditor(typeof(ColorSchemeComponent), true)]
    public class ColorSchemeComponentEditor : UnityEditor.Editor
    {
        private ColorSchemeComponent _target;

        private SerializedProperty _graphic;
        private SerializedProperty _colorScheme;

        private MethodInfo _colorSetter;
        private MethodInfo _objectsRefresher;
        
        private const float LeftColumnWidth = 40f;
        private const float RightColumnWidth = 20f;

        private void OnEnable()
        {
            _target = target as ColorSchemeComponent;

            _graphic = serializedObject.FindProperty("_graphic");
            _colorScheme = serializedObject.FindProperty("_colorScheme");

            _target.Refresh();
        }

        public override void OnInspectorGUI()
        {
            if (!DrawGUI()) return;

            _target.Refresh();
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

                while (_graphic.arraySize < colorCount)
                    _graphic.InsertArrayElementAtIndex(_graphic.arraySize);

                while (_graphic.arraySize > colorCount)
                    _graphic.DeleteArrayElementAtIndex(_graphic.arraySize - 1);

                for (int i = 0; i < colorCount; i++)
                {
                    SerializedProperty graphicCollection = _graphic.GetArrayElementAtIndex(i);
                    SerializedProperty graphicArray = graphicCollection.FindPropertyRelative("_graphic");

                    if (graphicArray.arraySize == 0)
                        graphicArray.InsertArrayElementAtIndex(0);

                    int last = graphicArray.arraySize - 1;
                    for (int j = 0; j < graphicArray.arraySize; j++)
                    {
                        EditorGUILayout.BeginHorizontal();

                        if (j == 0)
                        {
                            EditorGUI.BeginChangeCheck();
                            Color color = EditorGUILayout.ColorField(GUIContent.none, colorScheme[i],
                                true, true, false, GUILayout.Width(LeftColumnWidth));
                            if (EditorGUI.EndChangeCheck())
                            {
                                Undo.RecordObject(_colorScheme.objectReferenceValue, "Change Color");
                                SetColor(i, color);
                                EditorUtility.SetDirty(_colorScheme.objectReferenceValue);
                            }
                        }
                        else
                            GUILayout.Label($"       {(j == last ? '\u2514' : '\u251c')}",
                                GUILayout.Width(LeftColumnWidth));

                        EditorGUILayout.PropertyField(graphicArray.GetArrayElementAtIndex(j), GUIContent.none);

                        if (j == 0)
                        {
                            if (GUILayout.Button("+", GUILayout.Width(RightColumnWidth)))
                            {
                                graphicArray.InsertArrayElementAtIndex(graphicArray.arraySize - 1);
                                break;
                            }
                        }
                        else
                        {
                            if (GUILayout.Button("-", GUILayout.Width(RightColumnWidth)))
                            {
                                graphicArray.DeleteArrayElementAtIndex(j);
                                break;
                            }
                        }

                        EditorGUILayout.EndHorizontal();
                        
                        if (j == 0)
                            GUILayout.Space(1f);
                    }
                }
            }

            return serializedObject.ApplyModifiedProperties();
        }

        private void SetColor(int i, Color color)
        {
            if (_colorSetter == null)
                _colorSetter =
                    typeof(ColorScheme).GetMethod("SetColor",
                        BindingFlags.Instance | BindingFlags.NonPublic);

            _colorSetter.Invoke(_target.ColorScheme, new object[] { i, color });
            
            if (_objectsRefresher == null)
                _objectsRefresher =
                    typeof(ColorSchemeComponent).GetMethod("RefreshColor",
                        BindingFlags.Instance | BindingFlags.NonPublic);
            
            _objectsRefresher.Invoke(_target, new object[] { i });
        }
    }
}