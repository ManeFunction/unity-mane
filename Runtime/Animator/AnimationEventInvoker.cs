using System;
using UnityEngine;

namespace Mane.AnimatorStateMachine
{
    public class AnimationEventInvoker : MonoBehaviour
    {
        public event Action AnimationEventInvoked;
        
        private void InvokeEvent() => AnimationEventInvoked?.Invoke();
    }
}
