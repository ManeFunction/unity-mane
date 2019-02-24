using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Mane.Utils
{
    public static class UnityClassesExtensions
    {
        /// <summary>
        /// Gets first found component from root objects on scene.
        /// </summary>
        public static T GetRootComponent<T>(this Scene scene) where T : MonoBehaviour
        {
            T result = null;

            if (!scene.isLoaded)
            {
                return null;
            }

            // search for active objects
            T[] objects = Object.FindObjectsOfType<T>();
            foreach (T obj in objects)
            {
                if (obj.gameObject.scene == scene)
                {
                    result = obj;
                    break;
                }
            }

            if (result != null)
            {
                return result;
            }

            // if root wasn't found try to get hidden objects instead
            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene currentScene = SceneManager.GetSceneAt(i);
                if (!currentScene.isLoaded)
                {
                    continue;
                }

                GameObject[] allGameObjects = currentScene.GetRootGameObjects();
                foreach (GameObject go in allGameObjects)
                {
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }

            objects = results.ToArray();
            foreach (T obj in objects)
            {
                if (obj.gameObject.scene == scene)
                {
                    result = obj;
                    break;
                }
            }

            return result;
        }


        public static void SafeDestroy(this Object o)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
            {
                Object.Destroy(o);
            }
            else
            {
                Object.DestroyImmediate(o);
            }
#else
            Object.Destroy(o);
#endif
        }


        public static T Instantiate<T>(this string path, Transform parent = null) where T : Object
        {
            return Object.Instantiate(Resources.Load<T>(path), parent);
        }


        public static void Reset(this Transform transform, float z = 0f)
        {
            transform.localPosition = new Vector3(0f, 0f, z);
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
        }
        
        
        public static void SetLayerRecursively(this GameObject go, int newLayer)
        {
            if (go == null)
            {
                return;
            }

            go.layer = newLayer;
            foreach (Transform child in go.transform)
            {
                if (child != null)
                {
                    SetLayerRecursively(child.gameObject, newLayer);
                }
            }
        }

        public static void SetSortingLayerRecursively(this GameObject go, int newLayer, int? newOrder = null)
        {
            if (go == null)
            {
                return;
            }

            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sortingLayerID = newLayer;
                if (newOrder.HasValue)
                {
                    renderer.sortingOrder = newOrder.Value;
                }
            }

            Canvas canvas = go.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingLayerID = newLayer;
                if (newOrder.HasValue)
                {
                    canvas.sortingOrder = newOrder.Value;
                }
            }

            foreach (Transform child in go.transform)
            {
                if (child != null)
                {
                    SetSortingLayerRecursively(child.gameObject, newLayer, newOrder);
                }
            }
        }
    }
}