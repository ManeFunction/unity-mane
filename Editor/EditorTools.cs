﻿using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using System.Reflection;
using UnityEditor.SceneManagement;

namespace Mane.Editor
{
    /// <summary>
    /// Provides a set of tools for the Unity Editor.
    /// </summary>
    public static class EditorTools
    {
        /// <summary>
        /// Clears the Unity console.
        /// </summary>
        [MenuItem("Mane/Clear Console _F8", false, 900)]
        public static void ClearConsole()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type type = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo method = type.GetMethod("Clear");
            if (method != null)
                method.Invoke(new object(), null);
        }

        /// <summary>
        /// Captures a screenshot from the Game window and saves it to the desktop.
        /// Uses the current window resolution setup for the screenshot dimensions.
        /// </summary>
        [MenuItem("Mane/Take screenshot _F10", false, 902)]
        public static void CaptureScreenshot()
        {
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
        

        /// <summary>
        /// Clears all saved data from game's persistent data path.
        /// </summary>
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

        /// <summary>
        /// Deletes all PlayerPrefs.
        /// </summary>
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
        
        /// <summary>
        /// Creates a directory from an asset path if it doesn't exist.
        /// </summary>
        /// <param name="assetPath">The path to the asset.</param>
        public static void CreateDirectoryFromAssetPath(string assetPath)
        {
            string directoryPath = Path.GetDirectoryName(assetPath);
            if (directoryPath == null || Directory.Exists(directoryPath)) return;
            
            Directory.CreateDirectory(directoryPath);
            AssetDatabase.Refresh();
        }
    }
}