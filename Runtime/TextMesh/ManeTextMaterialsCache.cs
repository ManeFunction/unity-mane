using System.Collections.Generic;
using UnityEngine;

namespace Mane
{
    public partial class ManeText
    {
        private static readonly List<Material> MaterialsCache = new List<Material>();

        private Material FindMaterial(Font font)
        {
            if (font == null) return null;

            int i = 0;
            while (i < MaterialsCache.Count)
            {
                if (MaterialsCache[i] == null)
                {
                    MaterialsCache.RemoveAt(i);

                    continue;
                }

                if (MaterialsCache[i].mainTexture == _font.material.mainTexture)
                    return MaterialsCache[i];

                i++;
            }

            return null;
        }

        private Material CreateMaterial(Font font, bool addToCache)
        {
            if (font == null) return null;

            Material m = new Material(_font.material)
            {
                shader = UnityEngine.Shader.Find(Shader),
                hideFlags = HideFlags.DontSave
            };

            if (m != null)
            {
                if (addToCache)
                {
                    MaterialsCache.Add(m);
                    m.name = _font.name;
                }
                else m.name = string.Empty;
            }

            return m;
        }

        // it is not fair but works faster than search
        private bool ContainMaterial(Material material) => 
            material != null && !string.IsNullOrEmpty(material.name);

        private void DestroyMaterial(Material material)
        {
            if (material != null)
            {
                if (Renderer.sharedMaterial == material)
                    Renderer.sharedMaterial = null;

#if UNITY_EDITOR
                if (Application.isPlaying)
                    Destroy(material);
                else
                    DestroyImmediate(material);
#else
			    Destroy(material);
#endif
            }
        }
    }
}