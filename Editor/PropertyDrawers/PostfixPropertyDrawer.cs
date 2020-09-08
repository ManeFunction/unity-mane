using UnityEditor;
using UnityEngine;

namespace Mane.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(PostfixAttribute))]
    public class PostfixPropertyDrawer : PropertyDrawer
    {
        private float _postfixWidth = -1f;
        private GUIStyle _postfixStyle = null;


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PostfixAttribute attr = attribute as PostfixAttribute;

            if (_postfixWidth == -1f)
            {
                if (attr.Width == 0f)
                {
                    _postfixWidth = GUI.skin.label.CalcSize(new GUIContent(attr.Text)).x;
                }
                else
                {
                    _postfixWidth = attr.Width;
                }
            }

            if (_postfixStyle == null)
            {
                _postfixStyle = new GUIStyle(GUI.skin.label)
                {
                    alignment = TextAnchor.MiddleRight
                };
            }

            Rect fieldRect = position;
            fieldRect.width -= _postfixWidth;
            EditorGUI.PropertyField(fieldRect, property, label, true);

            Rect postfixRect = position;
            postfixRect.x = position.x + position.width - _postfixWidth;
            postfixRect.width = _postfixWidth;
            EditorGUI.LabelField(postfixRect, attr.Text, _postfixStyle);
        }
    }
}