using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Mane.Extensions
{
    /// <summary>
    /// Provides extension methods for Unity classes.
    /// </summary>
    public static class UnityClassesExtensions
    {
        /// <summary>
        /// Gets a component of type T from the game object. If the component does not exist, it adds one to the game object.
        /// </summary>
        /// <typeparam name="T">The type of the component to get. Must be a Component or a subclass of Component.</typeparam>
        /// <param name="gameObject">The game object to get the component from.</param>
        /// <returns>The component of type T.</returns>
        public static T GetRequiredComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
                component = gameObject.AddComponent<T>();

            return component;
        }
        
        /// <summary>
        /// Gets a component of type T from the game object of the provided component. If the component does not exist, it adds one to the game object.
        /// </summary>
        /// <typeparam name="T">The type of the component to get. Must be a Component or a subclass of Component.</typeparam>
        /// <param name="component">The component whose game object is to be used to get the component from.</param>
        /// <returns>The component of type T.</returns>
        public static T GetRequiredComponent<T>(this Component component) where T : Component => 
            component.gameObject.GetRequiredComponent<T>();
        
        
        /// <summary>
        /// Gets the first found root component of a specific type in a given scene.
        /// </summary>
        /// <typeparam name="T">The type of the component to get. Must be a Component or a subclass of Component.</typeparam>
        /// <param name="scene">The scene to get the root component from.</param>
        /// <returns>The root component of type T in the scene. If the scene is not loaded or the component is not found, returns null.</returns>
        public static T GetRootComponent<T>(this Scene scene) where T : Component
        {
            if (!scene.isLoaded) return null;

            // search for active objects
            T[] objects = Object.FindObjectsOfType<T>();
            T result = objects.FirstOrDefault(obj => obj.gameObject.scene == scene);

            if (result != null)
                return result;

            // if root wasn't found try to get hidden objects instead
            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene currentScene = SceneManager.GetSceneAt(i);
                if (!currentScene.isLoaded)
                    continue;

                GameObject[] allGameObjects = currentScene.GetRootGameObjects();
                foreach (GameObject go in allGameObjects)
                    results.AddRange(go.GetComponentsInChildren<T>(true));
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


        /// <summary>
        /// Safely destroys an Object. If the application is playing, it uses Object.Destroy, otherwise it uses Object.DestroyImmediate.
        /// </summary>
        /// <param name="o">The Object to destroy.</param>
        public static void SafeDestroy(this Object o)
        {
#if UNITY_EDITOR
            if (Application.isPlaying)
                Object.Destroy(o);
            else
                Object.DestroyImmediate(o);
#else
            Object.Destroy(o);
#endif
        }


        /// <summary>
        /// Instantiates an object of type T from a resource located at a specified path and optionally sets its parent.
        /// </summary>
        /// <typeparam name="T">The type of the object to instantiate. Must be a subclass of UnityEngine.Object.</typeparam>
        /// <param name="path">The path to the resource to instantiate.</param>
        /// <param name="parent">The parent Transform to set for the instantiated object. If null, the object will have no parent.</param>
        /// <returns>The instantiated object of type T.</returns>
        public static T Instantiate<T>(this string path, Transform parent = null) where T : Object => 
            Object.Instantiate(Resources.Load<T>(path), parent);


        /// <summary>
        /// Duplicates a GameObject.
        /// </summary>
        /// <param name="source">The GameObject to duplicate.</param>
        /// <returns>The duplicated GameObject.</returns>
        public static GameObject Duplicate(this GameObject source)
        {
            GameObject clone = Object.Instantiate(source, source.transform.parent);
            clone.transform.SetSiblingIndex(source.transform.GetSiblingIndex() + 1);

            return clone;
        }
        
        /// <summary>
        /// Duplicates a Component.
        /// </summary>
        /// <typeparam name="T">The type of the component to duplicate. Must be a Component or a subclass of Component.</typeparam>
        /// <param name="source">The component to duplicate.</param>
        /// <returns>The duplicated component.</returns>
        public static T Duplicate<T>(this T source) where T : Component
        {
            T clone = Object.Instantiate(source, source.transform.parent);
            clone.transform.SetSiblingIndex(source.transform.GetSiblingIndex() + 1);

            return clone;
        }
        
        /// <summary>
        /// Resets the position, scale, and rotation of a Transform.
        /// </summary>
        /// <param name="transform">The Transform to reset.</param>
        /// <param name="z">The z-coordinate for the local position of the Transform. Defaults to 0.</param>
        public static void Reset(this Transform transform, float z = 0f)
        {
            transform.localPosition = new Vector3(0f, 0f, z);
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
        }
        
        /// <summary>
        /// Rotates a Transform around a pivot point.
        /// </summary>
        /// <param name="transform">The Transform to rotate.</param>
        /// <param name="pivot">The pivot point for the rotation.</param>
        /// <param name="rotation">The rotation to apply.</param>
        public static void RotateAround(this Transform transform, Vector3 pivot, Quaternion rotation)
        {
            transform.position = rotation * (transform.position - pivot) + pivot;
            transform.rotation = rotation * transform.rotation;
        }
        
        /// <summary>
        /// Sets the layer of a GameObject and all its children recursively.
        /// </summary>
        /// <param name="go">The GameObject to set the layer of.</param>
        /// <param name="newLayer">The new layer to set.</param>
        public static void SetLayerRecursively(this GameObject go, int newLayer)
        {
            if (go == null) return;

            go.layer = newLayer;
            foreach (Transform child in go.transform)
            {
                if (child != null)
                    SetLayerRecursively(child.gameObject, newLayer);
            }
        }

        /// <summary>
        /// Sets the sorting layer of a GameObject and all its children recursively.
        /// </summary>
        /// <param name="go">The GameObject to set the sorting layer of.</param>
        /// <param name="newLayer">The new sorting layer to set.</param>
        /// <param name="newOrder">The new sorting order to set. If null, the sorting order is not changed.</param>
        public static void SetSortingLayerRecursively(this GameObject go, int newLayer, int? newOrder = null)
        {
            if (go == null) return;

            Renderer renderer = go.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sortingLayerID = newLayer;
                if (newOrder.HasValue)
                    renderer.sortingOrder = newOrder.Value;
            }

            Canvas canvas = go.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.sortingLayerID = newLayer;
                if (newOrder.HasValue)
                    canvas.sortingOrder = newOrder.Value;
            }

            foreach (Transform child in go.transform)
            {
                if (child != null)
                    SetSortingLayerRecursively(child.gameObject, newLayer, newOrder);
            }
        }
        
        /// <summary>
        /// Sets the active state of a GameObject and all its children recursively.
        /// </summary>
        /// <param name="go">The GameObject to set the active state of.</param>
        /// <param name="isActive">The active state to set.</param>
        public static void SetActiveStateRecursively(this GameObject go, bool isActive)
        {
            if (go == null) return;

            go.SetActive(isActive);
            foreach (Transform child in go.transform)
            {
                if (child != null)
                    SetActiveStateRecursively(child.gameObject, isActive);
            }
        }
        
        /// <summary>
        /// Performs an action on a GameObject and all its children recursively.
        /// </summary>
        /// <param name="go">The GameObject to perform the action on.</param>
        /// <param name="action">The action to perform on the GameObjects.</param>
        public static void DoRecursively(this GameObject go, Action<GameObject> action)
        {
            if (go == null || action == null) return;

            action(go);
            foreach (Transform child in go.transform)
            {
                if (child != null)
                    DoRecursively(child.gameObject, action);
            }
        }
        
        /// <summary>
        /// Determines whether a GameObject is a prefab.
        /// </summary>
        /// <param name="go">The GameObject to check.</param>
        /// <returns>True if the GameObject is a prefab, false otherwise.</returns>
        public static bool IsPrefab(this GameObject go)
        {
            // There is no "legit" way to know is GameObject prefab
            // or not besides PrefabUtility, but it's not available
            // in a runtime, so this is the most obvious workaround.
            return go.scene.rootCount == 0;
        }

        /// <summary>
        /// Invokes an action after a specified delay.
        /// </summary>
        /// <param name="target">The MonoBehaviour to attach the coroutine to.</param>
        /// <param name="action">The action to invoke after the delay.</param>
        /// <param name="delay">The delay before the action is invoked, in seconds.</param>
        /// <returns>A Coroutine running the delayed action, or null if the action is null or the delay is less than or equal to zero.</returns>
        public static Coroutine Delayed(this MonoBehaviour target, Action action, float delay)
        {
            if (action == null) return null;

            if (delay <= 0f)
            {
                action.Invoke();
                
                return null;
            }
            
            return target.StartCoroutine(Coroutine());

            
            IEnumerator Coroutine()
            {
                yield return new WaitForSeconds(delay);
                
                action.Invoke();
            }
        }

        /// <summary>
        /// Invokes an action after a specified number of frames.
        /// </summary>
        /// <param name="target">The MonoBehaviour to attach the coroutine to.</param>
        /// <param name="action">The action to invoke after the delay.</param>
        /// <param name="frames">The number of frames to wait before invoking the action.</param>
        /// <returns>A Coroutine running the delayed action, or null if the action is null or the number of frames is less than or equal to zero.</returns>
        public static Coroutine DelayedFrames(this MonoBehaviour target, Action action, int frames)
        {
            if (action == null) return null;

            if (frames <= 0)
            {
                action.Invoke();
                
                return null;
            }
            
            return target.StartCoroutine(Coroutine());

            
            IEnumerator Coroutine()
            {
                while (frames-- > 0)
                {
                    yield return null;
                }

                action.Invoke();
            }
        }

        /// <summary>
        /// Tries to stop a coroutine attached to a MonoBehaviour and sets the coroutine reference to null.
        /// </summary>
        /// <param name="target">The MonoBehaviour the coroutine is attached to.</param>
        /// <param name="coroutine">The coroutine to stop.</param>
        /// <returns>True if the coroutine was not null and the MonoBehaviour exists, false otherwise.</returns>
        public static bool TryKillCoroutine(this MonoBehaviour target, ref Coroutine coroutine)
        {
            if (coroutine == null || !target) return false;

            target.StopCoroutine(coroutine);
            coroutine = null;
            
            return true;
        }

        /// <summary>
        /// Iterates over the children of a GameObject and performs an action on each one. Optionally, can also recursively iterate over the children of the children.
        /// </summary>
        /// <param name="parent">The GameObject whose children to iterate over.</param>
        /// <param name="action">The action to perform on each child GameObject.</param>
        /// <param name="recursive">Whether to recursively iterate over the children of the children.</param>
        public static void IterateChildren(this GameObject parent, Action<GameObject> action, bool recursive = false)
        {
            int totalChildren = parent.transform.childCount;
            for (int i = 0; i < totalChildren; i++)
            {
                GameObject child = parent.transform.GetChild(i).gameObject;
                action?.Invoke(child);
                
                if (recursive)
                    child.IterateChildren(action, true);
            }
        }
    }
}