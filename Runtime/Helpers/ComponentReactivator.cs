using Mane.Extensions;
using Mane.Inspector;
using UnityEngine;

namespace Mane
{
    public class ComponentReactivator : MonoBehaviour
    {
        private const string Postfix = "frames";

        [InfoBox("This component disables target component on Awake and enables it with delay on Start. Can be used to force re-calculate layouts or something.")]
        [SerializeField] private Behaviour _component;
        [SerializeField, Postfix(Postfix)] private int _deactivateInterval;
        [SerializeField, Postfix(Postfix)] private int _reactivateInterval = 1;

        private void Awake()
        {
            if (_deactivateInterval > 0)
                this.DelayedFrames(Deactivate, _deactivateInterval);
            else
                Deactivate();
        }

        private void Start() => this.DelayedFrames(Activate, _deactivateInterval + _reactivateInterval);


        private void Activate() => _component.enabled = true;

        private void Deactivate() => _component.enabled = false;
    }
}