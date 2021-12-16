using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mane.Editor
{
    public static class SceneManagementTools
    {
        private static Scene _lastClosed;

        [MenuItem("Mane/Unload scene _%w", false, 600)]
        private static void UnloadSelectedScene() => SaveSelectedSceneOrLastAndClose(false);

        [MenuItem("Mane/Remove scene _%#w", false, 601)]
        private static void RemoveScene() => SaveSelectedSceneOrLastAndClose(true);

        private static void SaveSelectedSceneOrLastAndClose(bool unload)
        {
            GameObject selection = Selection.activeGameObject;
            Scene scene = selection ? selection.scene : SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

            if (EditorSceneManager.SaveModifiedScenesIfUserWantsTo(new Scene[] { scene }))
            {
                _lastClosed = scene;
                EditorSceneManager.CloseScene(scene, unload);
            }
        }

        [MenuItem("Mane/Reopen scene _%t", false, 602)]
        private static void LoadLastUnloadedScene() => EditorSceneManager.OpenScene(_lastClosed.path, OpenSceneMode.Additive);

        [MenuItem("Mane/Reopen scene _%t", true, 602)]
        private static bool LoadLastUnloadedSceneCheck() => _lastClosed.IsValid();
    }
}