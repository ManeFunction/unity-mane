using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mane
{
    public class PrefabList<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private T _source;
        [SerializeField] private List<T> _elements = new List<T>();
        
        public void Init(int count, Action<T, int> elementInitAction)
        {
            for (int i = 0; i < count; i++)
            {
                T element;
                if (i < _elements.Count)
                    element = _elements[i];
                else
                {
                    element = Instantiate(_source, transform);
                    _elements.Add(element);
                }
                
                elementInitAction?.Invoke(element, i);
            }
            
            while (_elements.Count > count)
            {
                Destroy(_elements[_elements.Count - 1].gameObject);
                _elements.RemoveAt(_elements.Count - 1);
            }
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
            _elements.Remove(element);
            Destroy(element.gameObject);
        }
        
        public void RemoveElementAt(int index)
        {
            Destroy(_elements[index].gameObject);
            _elements.RemoveAt(index);
        }
    }
}