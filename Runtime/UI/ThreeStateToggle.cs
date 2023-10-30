using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mane.UI
{
    public class ThreeStateToggle : Toggle
    {
        public Graphic offGraphic;
        public Graphic undefinedGraphic;

        [SerializeField] private ToggleState _state = ToggleState.Undefined;
        
        public ThreeStateToggleEvent onStateValueChanged = new ThreeStateToggleEvent();
        
        public event UnityAction<bool?> StateValueChanged
        {
            add => onStateValueChanged.AddListener(value.Invoke);
            remove => onStateValueChanged.RemoveListener(value.Invoke);
        }

        public new bool isOn
        {
            get => base.isOn;
            set
            {
                base.isOn = value; 
                UpdateStateFromIsOn();
            }
        }

        private const string ToggleGroupNotSupportedMessage = "Toggle group is not supported with ThreeStateToggle";
        [Obsolete(ToggleGroupNotSupportedMessage, true)]
        public new ToggleGroup group
        {
            get => throw new NotSupportedException(ToggleGroupNotSupportedMessage);
            set => throw new NotSupportedException(ToggleGroupNotSupportedMessage);
        }

        public bool? State
        {
            get => StateToBool(_state);
            set
            {
                ToggleState state = BoolToState(value);
                if (_state == state) return;
                
                _state = state;
                UpdateIsOnFromState();
                PlayUndefinedEffect(transition == Transition.None);
                onStateValueChanged.Invoke(value);
            }
        }

        private ToggleState BoolToState(bool? value) => value switch
        {
            true => ToggleState.On,
            false => ToggleState.Off,
            null => ToggleState.Undefined,
        };

        private bool? StateToBool(ToggleState state) => state switch
        {
            ToggleState.On => true,
            ToggleState.Off => false,
            ToggleState.Undefined => null,
            _ => null,
        };

        protected override void Start()
        {
            base.Start();
            
            UpdateIsOnFromState();
            PlayUndefinedEffect(true);
        }
        
        private void PlayUndefinedEffect(bool instant)
        {
            ProcessGraphic(undefinedGraphic, ToggleState.Undefined);
            ProcessGraphic(offGraphic, ToggleState.Off);
            
            return;
            
            
            void ProcessGraphic(Graphic g, ToggleState state)
            {
                if (g == null) return;
                
#if UNITY_EDITOR
                if (!Application.isPlaying)
                    g.canvasRenderer.SetAlpha(_state == state ? 1f : 0f);
                else
#endif
                    g.CrossFadeAlpha(_state == state ? 1f : 0f, instant ? 0f : 0.1f, true);
            }
        }
        
        protected override void OnDidApplyAnimationProperties()
        {
            // Check if state has been changed by the animation.
            // Unfortunately there is no way to check if we don't have a graphic.
            if (undefinedGraphic != null)
            {
                bool isUndefined = !Mathf.Approximately(undefinedGraphic.canvasRenderer.GetColor().a, 0f);
                if (isUndefined && _state != ToggleState.Undefined)
                    _state = ToggleState.Undefined;
            }

            base.OnDidApplyAnimationProperties();
        }

        private void UpdateStateFromIsOn() => 
            State = StateToBool(base.isOn ? ToggleState.On : ToggleState.Off);

        private void UpdateIsOnFromState()
        {
            switch (_state)
            {
                case ToggleState.On:
                    base.isOn = true;
                    break;
                case ToggleState.Off:
                case ToggleState.Undefined:
                    base.isOn = false;
                    break;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            switch (State)
            {
                case true:  State = false; break;
                case false: State = null;  break;
                case null:  State = true;  break;
            }
        }
        
        
        [Serializable]
        public class ThreeStateToggleEvent : UnityEvent<bool?> { }

        private enum ToggleState
        {
            Undefined,
            On,
            Off,
        }
    }
}