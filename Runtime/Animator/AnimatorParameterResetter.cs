using System;
using UnityEngine;

namespace Mane.StateMachine
{
    public class AnimatorParameterResetter : StateMachineBehaviour
    {
        [SerializeField] private ParameterType _type;
        [SerializeField] private string _parameter;


        private int? _parameterHash;


        public int ParameterHash
        {
            get
            {
                if (_parameterHash == null)
                    _parameterHash = Animator.StringToHash(_parameter);

                return _parameterHash.Value;
            }
        }


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            switch (_type)
            {
                case ParameterType.Bool:
                    animator.SetBool(ParameterHash, false);
                    break;
                case ParameterType.Int:
                    animator.SetInteger(ParameterHash, 0);
                    break;
                case ParameterType.Float:
                    animator.SetFloat(ParameterHash, 0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            base.OnStateEnter(animator, stateInfo, layerIndex);
        }


        public enum ParameterType
        {
            Bool = 0,
            Int = 1,
            Float = 2,
        }
    }
}