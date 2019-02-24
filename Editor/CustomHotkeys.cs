using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


namespace Mane.Utils.Editor
{
    public class CustomHotkeys : ScriptableObject
    {
        [MenuItem("HotKey/Run \u2044 Stop _F5", false, 500)]
        private static void PlayGame()
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        [MenuItem("HotKey/Pause _F6", false, 501)]
        private static void PauseGame()
        {
            EditorApplication.ExecuteMenuItem("Edit/Pause");
        }

        [MenuItem("HotKey/Step _F7", false, 502)]
        private static void StepGame()
        {
            EditorApplication.ExecuteMenuItem("Edit/Step");
        }

        [MenuItem("HotKey/Step _F7", true)]
        private static bool StepGameCheck()
        {
            return Application.isPlaying;
        }



        [MenuItem("HotKey/Clear Console _F8", false, 900)]
        private static void ClearConsole()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo method = type.GetMethod("Clear");
            if (method != null)
            {
                method.Invoke(new object(), null);
            }
        }

        [MenuItem("HotKey/Create children _%&N", false, 901)]
        private static void CreateChildren()
        {
            if (Selection.gameObjects == null || Selection.gameObjects.Length == 0)
            {
                return;
            }

            GameObject go = null;
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                go = new GameObject("GameObject");
                go.transform.SetParent(Selection.gameObjects[i].transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                go.transform.localRotation = Quaternion.identity;
                go.layer = Selection.gameObjects[i].layer;
                if (Selection.gameObjects[i].GetComponent<RectTransform>() != null)
                {
                    go.AddComponent<RectTransform>();
                }
            }

            if (Selection.gameObjects.Length == 1)
            {
                Selection.SetActiveObjectWithContext(go, Selection.activeContext);
            }
        }

        [MenuItem("HotKey/Take screenshot _F10", false, 902)]
        public static void Capture()
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("Can save screenshots only in playmode!");
                
                return;
            }

            DateTime t = DateTime.Now;
            string path = Application.dataPath.RemoveLastPathComponent() + "/Temp/Screenshots";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string scrName =
                $"Temp/Screenshots/Screenshot_{t.Year}_{t.Month:00}_{t.Day:00}_{t.Hour:00}_{t.Minute:00}_{t.Second:00}.png";

            ScreenCapture.CaptureScreenshot(scrName, 1);

            Debug.Log(scrName + " saved to the project folder!");
        }

        [MenuItem("HotKey/Enable \u2044 Disable selected GO _F4", false, 903)]
        private static void ChangeSelectedObjectState()
        {
            GameObject go = Selection.activeGameObject;
            go.SetActive(!go.activeSelf);
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(go.scene);
            }
        }

        [MenuItem("HotKey/Enable \u2044 Disable selected GO _F4", true, 903)]
        private static bool ChangeSelectedObjectStateCheck()
        {
            return Selection.activeGameObject;
        }



        [MenuItem("HotKey/Clear saved data _%F12", false, 800)]
        private static void ClearSavedData()
        {
            string path = Application.persistentDataPath;
            if (!Directory.Exists(path))
            {
                Debug.Log("Saves directory doesn't exist!");

                return;
            }

            DeleteFolder(path);

            Debug.Log("Saved data successfully cleared!");
        }

        private static void DeleteFolder(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                File.Delete(file);
            }

            string[] subfolders = Directory.GetDirectories(path);
            foreach (string folder in subfolders)
            {
                DeleteFolder(folder);
            }

            Directory.Delete(path);
        }

        [MenuItem("HotKey/Delete all PlayerPrefs _%#F12", false, 801)]
        private static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Player prefs successfully cleared!");
        }



        private static Scene _lastClosed;

        [MenuItem("HotKey/Scenes/Unload selected _%w", false, 600)]
        private static void UnloadSelectedScene()
        {
            SaveSelectedSceneOrLastAndClose(false);
        }

        [MenuItem("HotKey/Scenes/Remove selected _%#w", false, 601)]
        private static void RemoveScene()
        {
            SaveSelectedSceneOrLastAndClose(true);
        }

        private static void SaveSelectedSceneOrLastAndClose(bool unload)
        {
            GameObject selection = Selection.activeGameObject;
            Scene scene = selection ? selection.scene : SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            if (EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] {scene}))
            {
                _lastClosed = scene;
                EditorSceneManager.CloseScene(scene, unload);
            }
        }

        [MenuItem("HotKey/Scenes/Reopen last closed _%t", false, 602)]
        private static void LoadLastUnloadedScene()
        {
            EditorSceneManager.OpenScene(_lastClosed.path, OpenSceneMode.Additive);
        }

        [MenuItem("HotKey/Scenes/Reopen last closed _%t", true, 602)]
        private static bool LoadLastUnloadedSceneCheck()
        {
            return _lastClosed.IsValid();
        }
    }
}