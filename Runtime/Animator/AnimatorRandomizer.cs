using System;
using Mane.Inspector;
using UnityEngine;

namespace Mane.AnimatorStateMachine
{
    public class AnimatorRandomizer : StateMachineBehaviour
    {
        [Header("Leave condition empty for always true behaviour.")]
        [ArrayElements("_conditionParameter")]
        [SerializeField] private SwitchCondition[] _conditions;


        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (SwitchCondition condition in _conditions)
            {
                int? parameter = condition.ConditionParameter;
                if (parameter == null || animator.GetBool(parameter.Value))
                {
                    animator.SetInteger(condition.SelectorParameter, 
                                        UnityEngine.Random.Range(0, condition.TotalVariants));
                    break;
                }
            }
            
            base.OnStateEnter(animator, stateInfo, layerIndex);
        }


        [Serializable]
        public class SwitchCondition
        {
            [SerializeField] private string _conditionParameter;
            [SerializeField] private string _selectorParameter;
            [SerializeField] private int _totalVariants;
            
            private int? _selectorParameterHash;
            private int? _conditionParameterHash;
            
            public int SelectorParameter
            {
                get
                {
                    _selectorParameterHash ??= Animator.StringToHash(_selectorParameter);

                    return _selectorParameterHash.Value;
                }
            }
            
            public int? ConditionParameter
            {
                get
                {
                    if (string.IsNullOrEmpty(_conditionParameter)) return null;

                    _conditionParameterHash ??= Animator.StringToHash(_conditionParameter);

                    return _conditionParameterHash.Value;
                }
            }
            
            public int TotalVariants => _totalVariants;
        }
    }
}