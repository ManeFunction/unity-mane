using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mane.Editor
{
    public class SceneStats : MonoBehaviour
    {
        private int _totalGameObjectsCount;
        private int _totalVerticesCount;
        private int _totalComponentsCount;

        [ContextMenu("Calculate Stats")]
        private void CalculateStats()
        {
            _totalGameObjectsCount = 0;
            _totalVerticesCount = 0;
            _totalComponentsCount = 0;

            Scene scene = gameObject.scene;

            // Get all root GameObjects in the scene
            GameObject[] rootObjects = scene.GetRootGameObjects();

            // Loop through each root GameObject and calculate stats recursively
            foreach (GameObject rootObject in rootObjects)
                CalculateStatsRecursively(rootObject);

            Debug.Log($"Total GameObjects on the {scene.name}: {_totalGameObjectsCount}");
            Debug.Log($"Total Vertices count on the {scene.name}: {_totalVerticesCount}");
            Debug.Log($"Total Components count on the {scene.name}: {_totalComponentsCount}");
        }

        private void CalculateStatsRecursively(GameObject go)
        {
            _totalGameObjectsCount++;

            // Check if the GameObject has a Renderer or SkinnedMeshRenderer component
            Renderer rendererComponent = go.GetComponent<Renderer>();
            SkinnedMeshRenderer skinnedRendererComponent = go.GetComponent<SkinnedMeshRenderer>();

            if (rendererComponent != null || skinnedRendererComponent != null)
            {
                // If the GameObject has Renderer or SkinnedMeshRenderer, add its vertices count to the totalVerticesCount
                MeshFilter meshFilter = go.GetComponent<MeshFilter>();
                if (meshFilter != null && meshFilter.sharedMesh != null)
                    _totalVerticesCount += meshFilter.sharedMesh.vertexCount;

                if (skinnedRendererComponent != null && skinnedRendererComponent.sharedMesh != null)
                    _totalVerticesCount += skinnedRendererComponent.sharedMesh.vertexCount;
            }

            // Calculate components count (do not count Transform component)
            _totalComponentsCount += go.GetComponents<Component>().Length - 1;

            // Calculate stats for children
            int childCount = go.transform.childCount;
            for (int i = 0; i < childCount; i++)
                CalculateStatsRecursively(go.transform.GetChild(i).gameObject);
        }
    }
}