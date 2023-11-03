using System;
using System.Collections;
using System.Collections.Generic;
using Mane.Extensions;
using UnityEngine;

namespace Mane
{
    public abstract class PrefabList<T> : MonoBehaviour, IEnumerable<T> where T : Component
    {
        [SerializeField] private bool _destroyElements = true;
        [SerializeField] private T _source;
        [SerializeField] private List<T> _elements = new List<T>();

        public event Action<T> ElementAdded;
        public event Action<T> ElementWillBeRemoved;
        public event Action<int> CountChanged;
        
        public int Count => _destroyElements ? _elements.Count : FindTheEdge();

        public int Capacity => _elements.Count;
        
        public T this[int index] => _elements[index];
        
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
        
        public void AddElement(Action<T, bool> elementInitAction)
        {
            var creation = GetElement(Count);
            
            elementInitAction?.Invoke(creation.element, creation.isNew);

            ElementAdded?.Invoke(creation.element);
            CountChanged?.Invoke(Count);
        }

        public void RemoveElement(T element)
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
        
        public void RemoveElementAt(int i)
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
        
        public void ClearElements(bool riseRemoveEvents = false)
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