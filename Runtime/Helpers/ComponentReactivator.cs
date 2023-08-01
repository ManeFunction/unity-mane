using Mane.Extensions;
using Mane.Inspector;
using UnityEngine;

namespace Mane
{
    public class ComponentReactivator : MonoBehaviour
    {
        private const string Postfix = "frames";

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