using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Mane.Editor
{
    [CustomEditor(typeof(Area))]
    public class AreaEditor : UnityEditor.Editor
    {
        private FieldInfo _centerField;
        private FieldInfo _sizeField;

        private readonly BoxBoundsHandle _handle = new BoxBoundsHandle
        {
            wireframeColor = Color.cyan,
            handleColor = Color.cyan,
        };

        private void Awake()
        {
            _centerField = typeof(Area).GetField("_center", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            _sizeField = typeof(Area).GetField("_size", 
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private void OnSceneGUI()
        {
            Area area = (Area)target;
            
            if (!area.enabled) return;

            _handle.center = area.transform.position + (Vector3)_centerField.GetValue(area);
            _handle.size = (Vector3)_sizeField.GetValue(area);
            
            EditorGUI.BeginChangeCheck();
            _handle.DrawHandle();
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(area, "Change Area");
                
                _centerField.SetValue(area, _handle.center - area.transform.position);
                _sizeField.SetValue(area, _handle.size);
            }
        }
    }
}