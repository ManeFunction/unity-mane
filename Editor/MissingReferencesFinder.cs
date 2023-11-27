using UnityEditor;
using UnityEngine;

namespace Mane.Editor
{
    public class MissingReferencesEditor : EditorWindow
    {
        private GameObject _root;

        [MenuItem("Mane/Missing References Helper", false, 1101)]
        private static void CreateWindow()
        {
            MissingReferencesEditor window = (MissingReferencesEditor)
                GetWindowWithRect(typeof(MissingReferencesEditor), new Rect(0, 0, 300, 120));
            window.titleContent = new GUIContent("Missing References Helper");
        }

        private void OnGUI()
        {
            const float smallOffset = 5f;
            const float mediumOffset = 10f;

            GUILayout.Space(smallOffset);
            GUILayout.Label("Place root object here:");
            _root = (GameObject)EditorGUILayout.ObjectField(_root, typeof(GameObject), true);
            GUILayout.Space(mediumOffset);

            if (GUILayout.Button("Scan"))
            {
                Scan(_root);

                Debug.Log("Scan completed!");
            }
        }

        private static void Scan(GameObject obj)
        {
            if (!obj) return;

            MonoBehaviour[] components = obj.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour component in components)
            {
                if (!component)
                {
                    Debug.LogError($"Missing Component in GO: {FullPath(obj)}", obj);

                    continue;
                }

                SerializedObject serializedObject = new SerializedObject(component);
                SerializedProperty serializedProperty = serializedObject.GetIterator();

                while (serializedProperty.NextVisible(true))
                {
                    if (serializedProperty.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (serializedProperty.objectReferenceValue == null
                            && serializedProperty.objectReferenceInstanceIDValue != 0)
                            Debug.LogError(
                                $"Missing Ref in: {FullPath(obj)}. Component: {component.GetType().Name}, Property: {ObjectNames.NicifyVariableName(serializedProperty.name)}",
                                obj);
                    }
                }
            }

            int childCount = obj.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = obj.transform.GetChild(i);

                // Yeah, it's super clunky solution, but at least it worked...
                if (child.gameObject.name.EndsWith(" (Missing Prefab)"))
                    Debug.LogError($"Missing prefab instance: {FullPath(child.gameObject)}");

                Scan(child.gameObject);
            }
            
            return;


            string FullPath(GameObject go) => go.transform.parent == null
                ? go.name
                : FullPath(go.transform.parent.gameObject) + "/" + go.name;
        }
    }
}