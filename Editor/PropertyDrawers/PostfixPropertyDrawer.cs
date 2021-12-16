using UnityEditor;
using UnityEngine;

namespace Mane.Inspector.Editor
{
    [CustomPropertyDrawer(typeof(PostfixAttribute))]
    public class PostfixPropertyDrawer : PropertyDrawer
    {
        private float _postfixWidth = -1f;
        private GUIStyle _postfixStyle;


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 
            EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PostfixAttribute attr = attribute as PostfixAttribute;

            if (_postfixWidth == -1f)
                _postfixWidth = attr.Width == 0f ? 
                    GUI.skin.label.CalcSize(new GUIContent(attr.Text)).x : 
                    attr.Width;

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