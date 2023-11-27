using System;
using System.Collections;
using System.Collections.Generic;
using Mane.Extensions;
using UnityEngine;

namespace Mane
{
    /// <summary>
    /// Represents a dynamic list of prefabricated elements (components) in Unity.
    /// Allows for the creation, initialization, addition, and removal of elements dynamically at runtime.
    /// </summary>
    /// <typeparam name="T">The component type of the elements in the list.</typeparam>
    public abstract class PrefabList<T> : MonoBehaviour, IEnumerable<T> where T : Component
    {
        [SerializeField] private bool _destroyElements = true;
        [SerializeField] private T _source;
        [SerializeField] private List<T> _elements = new List<T>();

        /// <summary>
        /// Occurs when a new element is added to the list.
        /// </summary>
        public event Action<T> ElementAdded;

        /// <summary>
        /// Occurs just before an element is removed from the list.
        /// </summary>
        public event Action<T> ElementWillBeRemoved;

        /// <summary>
        /// Occurs when the count of elements in the list changes.
        /// </summary>
        public event Action<int> CountChanged;

        public int Count => _destroyElements ? _elements.Count : FindTheEdge();

        public int Capacity => _elements.Count;

        public T this[int index] => _elements[index];

        /// <summary>
        /// Initializes the list with a specified count of elements, 
        /// calling a provided initialization action for each element.
        /// </summary>
        /// <param name="count">The number of elements to initialize.</param>
        /// <param name="elementInitAction">The action to initialize each element.</param>
        public void Init(int count, Action<T, int, bool> elementInitAction)
        {
            for (int i = 0; i < count; i++)
            {
                var creation = GetElement(i);
                elementInitAction?.Invoke(creation.element, i, creation.isNew);
            }

            RemoveExcessElements(count);

            CountChanged?.Invoke(Count);
        }

        /// <summary>
        /// Initializes the list with data from an IEnumerable, 
        /// calling a provided initialization action for each data item.
        /// </summary>
        /// <typeparam name="D">The type of data in the IEnumerable.</typeparam>
        /// <param name="data">The data to initialize the elements with.</param>
        /// <param name="elementInitAction">The action to initialize each element.</param>
        public void Init<D>(IEnumerable<D> data, Action<T, D, int, bool> elementInitAction)
        {
            int i = 0;
            foreach (D d in data)
            {
                var creation = GetElement(i);
                elementInitAction?.Invoke(creation.element, d, i, creation.isNew);
                i++;
            }

            RemoveExcessElements(i);

            CountChanged?.Invoke(Count);
        }

        /// <summary>
        /// Adds a new element to the list, invoking an optional initialization action.
        /// </summary>
        /// <param name="elementInitAction">The action to initialize the new element.</param>
        public virtual void AddElement(Action<T, bool> elementInitAction)
        {
            var creation = GetElement(Count);

            elementInitAction?.Invoke(creation.element, creation.isNew);

            ElementAdded?.Invoke(creation.element);
            CountChanged?.Invoke(Count);
        }

        /// <summary>
        /// Adds a range of elements to the list based on data from an IEnumerable,
        /// calling a provided initialization action for each data item.
        /// </summary>
        /// <typeparam name="D">The type of data in the IEnumerable.</typeparam>
        /// <param name="data">The data to initialize the elements with.</param>
        /// <param name="elementInitAction">The action to initialize each element.</param>
        public virtual void AddElementsRange<D>(IEnumerable<D> data, Action<T, D, int, bool> elementInitAction)
        {
            int i = Count;
            foreach (D d in data)
            {
                var creation = GetElement(i);
                elementInitAction?.Invoke(creation.element, d, i, creation.isNew);
                i++;
            }

            CountChanged?.Invoke(i);
        }

        /// <summary>
        /// Removes a specified element from the list.
        /// </summary>
        /// <param name="element">The element to remove.</param>
        public virtual void RemoveElement(T element)
        {
            ElementWillBeRemoved?.Invoke(element);

            if (_destroyElements)
            {
                _elements.Remove(element);
                element.gameObject.SafeDestroy();
            }
            else
            {
                element.gameObject.SetActive(false);
                element.transform.SetAsLastSibling();
                _elements.Remove(element);
                _elements.Add(element);
            }

            CountChanged?.Invoke(Count - 1);
        }

        /// <summary>
        /// Removes an element at a specified index from the list.
        /// </summary>
        /// <param name="i">The index of the element to remove.</param>
        public virtual void RemoveElementAt(int i)
        {
            ElementWillBeRemoved?.Invoke(_elements[i]);

            if (_destroyElements)
            {
                _elements[i].gameObject.SafeDestroy();
                _elements.RemoveAt(i);
            }
            else
                _elements[i].gameObject.SetActive(false);

            CountChanged?.Invoke(Count - 1);
        }

        /// <summary>
        /// Clears all elements from the list. Optionally triggers remove events for each element.
        /// </summary>
        /// <param name="riseRemoveEvents">Whether to trigger remove events for each element.</param>
        public virtual void ClearElements(bool riseRemoveEvents = false)
        {
            if (riseRemoveEvents)
            {
                int count = Count;
                for (int i = 0; i < count; i++)
                    ElementWillBeRemoved?.Invoke(_elements[i]);
            }

            if (_destroyElements)
            {
                foreach (T element in _elements)
                    Destroy(element.gameObject);
                _elements.Clear();
            }
            else
            {
                foreach (T element in _elements)
                    element.gameObject.SetActive(false);
            }

            CountChanged?.Invoke(0);
        }

        private (T element, bool isNew) GetElement(int i)
        {
            T element;
            bool isNew = false;
            if (i < _elements.Count)
            {
                element = _elements[i];
                element.gameObject.SetActive(true);
            }
            else
            {
                element = Instantiate(_source, transform);
                _elements.Add(element);
                isNew = true;
            }

            return (element, isNew);
        }

        private void RemoveExcessElements(int count)
        {
            if (_elements.Count == count)
                return;
            
            int i = _elements.Count - 1;    
            while (i >= count)
                RemoveElementAt(i--);
        }

        private int FindTheEdge()
        {
            for (int i = 0; i < _elements.Count; i++)
            {
                if (!_elements[i].gameObject.activeSelf)
                    return i;
            }

            return _elements.Count;
        }
        

        public IEnumerator<T> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
