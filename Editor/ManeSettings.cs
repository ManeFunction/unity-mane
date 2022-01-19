using UnityEditor;
using UnityEngine;


namespace Mane.Editor
{
    internal class ManeSettings : ScriptableObject
    {
        private const string ManeSettingsPath = "Assets/Editor/ManeSettings.asset";

        [SerializeField] private string _prefabsSavingPath;

        public string PrefabsSavingPath => _prefabsSavingPath;

        internal static ManeSettings GetOrCreateSettings()
        {
            ManeSettings settings = AssetDatabase.LoadAssetAtPath<ManeSettings>(ManeSettingsPath);
            if (settings == null)
            {
                settings = CreateInstance<ManeSettings>();
                settings._prefabsSavingPath = "Assets/";
                AssetDatabase.CreateAsset(settings, ManeSettingsPath);
                AssetDatabase.SaveAssets();
            }

            return settings;
        }

        internal static SerializedObject GetSerializedSettings() =>
            new SerializedObject(GetOrCreateSettings());
    }
}