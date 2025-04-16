using UnityEngine;
using UnityEditor;

namespace Mane.Inspector.Editor
{
    public static class RectTransformToAnchorsEditor
    {
        [MenuItem("CONTEXT/RectTransform/Convert/Bake Transform to Anchors", false, 500)]
        private static void BakeToAnchors(MenuCommand command)
        {
            RectTransform rt = command.context as RectTransform;
            if (rt == null)
            {
                Debug.LogWarning("No RectTransform found.");
                return;
            }

            // The parent must also be a RectTransform; otherwise, we can't compute anchors
            RectTransform parentRt = rt.parent as RectTransform;
            if (parentRt == null)
            {
                Debug.LogWarning("Selected RectTransform has no RectTransform parent. Cannot adjust anchors.");
                return;
            }

            Undo.RecordObject(rt, "Freeze anchoredPosition into Anchors");

            BakeRectTransformToAnchors(rt, parentRt);
        }

        private static void BakeRectTransformToAnchors(RectTransform rt, RectTransform parentRt)
        {
            Vector2 anchoredPos = rt.anchoredPosition;
            Vector2 sizeDelta = rt.sizeDelta;
            
            Rect parentRect = parentRt.rect;
            float pw = parentRect.width;
            float ph = parentRect.height;

            if (Mathf.Approximately(pw, 0f) || Mathf.Approximately(ph, 0f))
            {
                Debug.LogWarning("Parent RectTransform has zero width or height, cannot shift anchors.");
                return;
            }

            float deltaAnchorX = anchoredPos.x / pw;
            float deltaAnchorY = anchoredPos.y / ph;
            Vector2 deltaAnchor = new(deltaAnchorX, deltaAnchorY);

            float deltaWidth = sizeDelta.x / pw;
            float deltaHeight = sizeDelta.y / ph;
            
            Vector2 anchorMin = rt.anchorMin + deltaAnchor;
            Vector2 anchorMax = rt.anchorMax + deltaAnchor;
            
            anchorMin.x -= deltaWidth * .5f;
            anchorMax.x += deltaWidth * .5f;
            anchorMin.y -= deltaHeight * .5f;
            anchorMax.y += deltaHeight * .5f;

            rt.anchorMin = anchorMin;
            rt.anchorMax = anchorMax;

            rt.anchoredPosition = Vector2.zero;
            rt.sizeDelta = Vector2.zero;
            
            EditorUtility.SetDirty(rt);
        }
    }
}