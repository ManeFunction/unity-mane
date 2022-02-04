using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using UnityEditor.SceneManagement;


namespace Mane.Editor
{
    public static class EditorTools
    {
        [MenuItem("Mane/Clear Console _F8", false, 900)]
        public static void ClearConsole()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo method = type.GetMethod("Clear");
            if (method != null)
                method.Invoke(new object(), null);
        }

        [MenuItem("Mane/Take screenshot _F10", false, 902)]
        public static void CaptureScreenshot()
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
        

        [MenuItem("Mane/Enable \u2044 Disable selected GO _F4", false, 903)]
        private static void ChangeSelectedObjectState()
        {
            bool state = !Selection.activeGameObject.activeSelf;
            foreach (GameObject go in Selection.gameObjects)
                go.SetActive(state);
            
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(Selection.activeGameObject.scene);
        }

        [MenuItem("Mane/Enable \u2044 Disable selected GO _F4", true, 903)]
        private static bool ChangeSelectedObjectStateCheck() => Selection.activeGameObject;



        [MenuItem("Mane/Clear saved data _%F12", false, 800)]
        public static void ClearSavedData()
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

        [MenuItem("Mane/Delete all PlayerPrefs _%#F12", false, 801)]
        public static void DeletePlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Player prefs successfully cleared!");
        }

        
        private static void DeleteFolder(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
                File.Delete(file);

            string[] subfolders = Directory.GetDirectories(path);
            foreach (string folder in subfolders)
                DeleteFolder(folder);

            Directory.Delete(path);
        }
        
        public static void CreateDirectoryFromAssetPath(string assetPath)
        {
            string directoryPath = Path.GetDirectoryName(assetPath);
            if (directoryPath == null || Directory.Exists(directoryPath)) return;
            
            Directory.CreateDirectory(directoryPath);
            AssetDatabase.Refresh();
        }
    }
}