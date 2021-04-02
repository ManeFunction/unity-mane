using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


namespace Mane.Editor
{
    public class CustomHotkeys : ScriptableObject
    {
        [MenuItem("Mane Utils/Run \u2044 Stop _F5", false, 500)]
        private static void PlayGame()
        {
            EditorApplication.ExecuteMenuItem("Edit/Play");
        }

        [MenuItem("Mane Utils/Pause _F6", false, 501)]
        private static void PauseGame()
        {
            EditorApplication.ExecuteMenuItem("Edit/Pause");
        }

        [MenuItem("Mane Utils/Step _F7", false, 502)]
        private static void StepGame()
        {
            EditorApplication.ExecuteMenuItem("Edit/Step");
        }

        [MenuItem("Mane Utils/Step _F7", true)]
        private static bool StepGameCheck()
        {
            return Application.isPlaying;
        }
        
        
        [MenuItem("Mane Utils/Clear Console _F8", false, 900)]
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

        [MenuItem("Mane Utils/Take screenshot _F10", false, 902)]
        public static void Capture()
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("Can save screenshots only in playmode!");
                
                return;
            }

            DateTime t = DateTime.Now;
            string scrName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + 
                             $"/Screenshot_{t.Year}_{t.Month:00}_{t.Day:00}_{t.Hour:00}_{t.Minute:00}_{t.Second:00}.png";

            ScreenCapture.CaptureScreenshot(scrName, 1);

            Debug.Log($"Screenshot captured: {scrName}");
        }

        [MenuItem("Mane Utils/Enable \u2044 Disable selected GO _F4", false, 903)]
        private static void ChangeSelectedObjectState()
        {
            bool state = !Selection.activeGameObject.activeSelf;
            foreach (GameObject go in Selection.gameObjects)
            {
                go.SetActive(state);
            }
            if (!Application.isPlaying)
            {
                EditorSceneManager.MarkSceneDirty(Selection.activeGameObject.scene);
            }
        }

        [MenuItem("Mane Utils/Enable \u2044 Disable selected GO _F4", true, 903)]
        private static bool ChangeSelectedObjectStateCheck()
        {
            return Selection.activeGameObject;
        }



        [MenuItem("Mane Utils/Clear saved data _%F12", false, 800)]
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

        [MenuItem("Mane Utils/Delete all PlayerPrefs _%#F12", false, 801)]
        private static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Player prefs successfully cleared!");
        }



        private static Scene _lastClosed;

        [MenuItem("Mane Utils/Unload scene _%w", false, 600)]
        private static void UnloadSelectedScene()
        {
            SaveSelectedSceneOrLastAndClose(false);
        }

        [MenuItem("Mane Utils/Remove scene _%#w", false, 601)]
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

        [MenuItem("Mane Utils/Reopen scene _%t", false, 602)]
        private static void LoadLastUnloadedScene()
        {
            EditorSceneManager.OpenScene(_lastClosed.path, OpenSceneMode.Additive);
        }

        [MenuItem("Mane Utils/Reopen scene _%t", true, 602)]
        private static bool LoadLastUnloadedSceneCheck()
        {
            return _lastClosed.IsValid();
        }
        
        [MenuItem("Mane Utils/Force Reserialize Assets", false, 1000)]
        private static void ForceSaveAssets()
        {
            if (EditorUtility.DisplayDialog(
                "Force Assets Reserialization",
                "This may be long operation despite of the size of your project, so be aware! There is no progress bar so it may looks like that Unity is frozen, but be patient, it's working. Proceed operation?",
                "Yes, go ahead!", "Cancel!"))
            {
                AssetDatabase.ForceReserializeAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}