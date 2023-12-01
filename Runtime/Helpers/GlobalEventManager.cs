using System;
using System.Collections.Generic;
using System.Linq;

namespace Mane
{
    public class GlobalEventManager : MonoSingleton<GlobalEventManager>
    {
        // Dictionary to hold events and their respective listeners
        private Dictionary<Type, Delegate> _eventListeners;

        protected override void Awake()
        {
            base.Awake();
            
            _eventListeners = new Dictionary<Type, Delegate>();
        }

        /// <summary>
        /// Add a listener to an event
        /// </summary>
        /// <param name="listener">The listener to add</param>
        /// <typeparam name="T">Type of event, derived from EventInfo</typeparam>
        public void AddListener<T>(EventDelegate<T> listener) where T : BaseEvent
        {
            if (_eventListeners.TryGetValue(typeof(T), out var existingDelegate))
            {
                // check if delegate is already subscribed to this event
                if (existingDelegate.GetInvocationList().Contains(listener)) return;
                
                _eventListeners[typeof(T)] = Delegate.Combine(existingDelegate, listener);
            }
            else
                _eventListeners[typeof(T)] = listener;
        }

        /// <summary>
        /// Remove a listener from an event
        /// </summary>
        /// <param name="listener">The listener to remove</param>
        /// <typeparam name="T">Type of event, derived from EventInfo</typeparam>
        public void RemoveListener<T>(EventDelegate<T> listener) where T : BaseEvent
        {
            if (!_eventListeners.TryGetValue(typeof(T), out var existingDelegate)) return;
            
            var newDelegate = Delegate.Remove(existingDelegate, listener);
            if (newDelegate == null)
                _eventListeners.Remove(typeof(T));
            else
                _eventListeners[typeof(T)] = newDelegate;
        }

        /// <summary>
        /// Raise an event
        /// </summary>
        /// <param name="e">The event to raise</param>
        /// <typeparam name="T">Type of event, derived from EventInfo</typeparam>
        public void RaiseEvent<T>(T e) where T : BaseEvent
        {
            if (!_eventListeners.TryGetValue(typeof(T), out var delegateObj)) return;
            
            var callback = delegateObj as EventDelegate<T>;
            callback?.Invoke(e);
        }
    }
    
    /// <summary>
    /// Delegate for global events
    /// </summary>
    /// <typeparam name="T">Type of event, derived from EventInfo</typeparam>
    public delegate void EventDelegate<in T>(T e) where T : BaseEvent;

    /// <summary>
    /// Base class for all events
    /// </summary>
    public abstract class BaseEvent
    {
        /// <summary>
        /// The object that sent this event
        /// </summary>
        public object Sender { get; private set; }
        
        protected BaseEvent(object sender) => Sender = sender;
    }
}