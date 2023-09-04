using UnityEngine;
using UnityEngine.UI;

namespace Mane
{
    public class ColorSchemeSyncComponent : MaskableGraphic
    {
        [SerializeField] private MaskableGraphic[] _children;

        public override Color color
        {
            get => base.color;
            set
            {
                base.color = value;

                foreach (MaskableGraphic child in _children)
                {
                    if (child)
                        child.color = value;
                }
            }
        }
    }
}