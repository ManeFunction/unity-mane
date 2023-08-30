using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mane
{
    public class PrefabList<T> : MonoBehaviour, IEnumerable<T> where T : Component
    {
        [SerializeField] private bool _destroyElements = true;
        [SerializeField] private T _source;
        [SerializeField] private List<T> _elements = new List<T>();
        
        public int Count => _elements.Count;
        
        public T this[int index] => _elements[index];
        
        public void Init(int count, Action<T, int> elementInitAction)
        {
            for (int i = 0; i < count; i++)
            {
                T element = GetElement(i);
                elementInitAction?.Invoke(element, i);
            }
            
            RemoveExcessElements(count);
        }

        public void Init<D>(IReadOnlyList<D> data, Action<T, D, int> elementInitAction)
        {
            for (int i = 0; i < data.Count; i++)
            {
                T element = GetElement(i);
                elementInitAction?.Invoke(element, data[i], i);
            }
            
            RemoveExcessElements(data.Count);
        }
        
        public T AddElement(Action<T> elementInitAction)
        {
            T element = Instantiate(_source, transform);
            _elements.Add(element);
            
            elementInitAction?.Invoke(element);
            
            return element;
        }
        
        public void RemoveElement(T element)
        {
            if (_destroyElements)
            {
                _elements.Remove(element);
                Destroy(element.gameObject);
            }
            else
                element.gameObject.SetActive(false);
        }
        
        public void RemoveElementAt(int i)
        {
            if (_destroyElements)
            {
                Destroy(_elements[i].gameObject);
                _elements.RemoveAt(i);
            }
            else
                _elements[i].gameObject.SetActive(false);
        }
        
        public void ClearElements()
        {
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
        }
        

        private T GetElement(int i)
        {
            T element;
            if (i < _elements.Count)
            {
                element = _elements[i];
                element.gameObject.SetActive(true);
            }
            else
            {
                element = Instantiate(_source, transform);
                _elements.Add(element);
            }

            return element;
        }

        private void RemoveExcessElements(int count)
        {
            if (_elements.Count == count)
                return;
            
            int i = _elements.Count - 1;    
            while (i >= count)
                RemoveElementAt(i--);
        }
        

        public IEnumerator<T> GetEnumerator() => _elements.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}