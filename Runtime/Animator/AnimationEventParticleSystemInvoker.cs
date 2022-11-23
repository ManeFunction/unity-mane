using UnityEngine;

namespace Mane.AnimatorStateMachine
{
    public class AnimationEventParticleSystemInvoker : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        
        private void InvokeParticleSystem()
        {
            if (_particleSystem)
                _particleSystem.Play();
        }
    }
}
