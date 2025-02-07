using UnityEngine;
using UnityEditor;

namespace Mane.Inspector.Editor
{
    public static class RectTransformPositionToAnchorEditor
    {
        [MenuItem("CONTEXT/RectTransform/Convert/anchoredPosition into Anchors", false, 500)]
        private static void FreezeAnchors(MenuCommand command)
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

            FreezeAnchoredPositionIntoAnchors(rt, parentRt);
        }

        private static void FreezeAnchoredPositionIntoAnchors(RectTransform rt, RectTransform parentRt)
        {
            Vector2 anchoredPos = rt.anchoredPosition;
            if (anchoredPos.sqrMagnitude < Mathf.Epsilon)
            {
                return;
            }

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

            rt.anchorMin += deltaAnchor;
            rt.anchorMax += deltaAnchor;

            rt.anchoredPosition = Vector2.zero;
            
            EditorUtility.SetDirty(rt);
        }
    }
}