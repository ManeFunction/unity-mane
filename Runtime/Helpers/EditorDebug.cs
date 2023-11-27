using UnityEngine;

namespace Mane
{
    /// <summary>
    /// This class is used to log messages only in Editor.
    /// </summary>
    public static class EditorDebug
    {
        /// <summary>
        /// This method is used to log messages only in Editor.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void Log(object message)
        {
#if UNITY_EDITOR
            Debug.Log(message);
#endif
        }
        
        /// <summary>
        /// This method is used to log warnings only in Editor.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void LogWarning(object message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }
        
        /// <summary>
        /// This method is used to log errors only in Editor.
        /// </summary>
        /// <param name="message">Message to log.</param>
        public static void LogError(object message)
        {
#if UNITY_EDITOR
            Debug.LogError(message);
#endif
        }
    }
}