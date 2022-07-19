using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.ObjectPool
{
    public class ObjectPoolController : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private int _initCount;

        private List<GameObject> _objects;
        private int _busyObjectsCount;

        public void OnStart()
        {
            _objects = new List<GameObject>();
            _busyObjectsCount = 0;
            
            for (int i = 0; i < _initCount; i++)
                CreateNewObject();
        }

        public GameObject GetObject()
        {
            if (_busyObjectsCount == _objects.Count)
            {
                CreateNewObject();
                var last = _objects.Count - 1;
                return _objects[last];
            }

            GameObject result = null;

            for (int i = 0; i < _objects.Count; i++)
            {
                var obj = _objects[i];
                
                if (obj.activeInHierarchy)
                    continue;

                result = obj;
            }

            _busyObjectsCount += 1;
            return result;
        }

        public void ReturnObject(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(_container);
            _busyObjectsCount -= 1;
        }

        private void CreateNewObject()
        {
            var newObject = Instantiate(_prefab, _container);
            newObject.SetActive(false);
            _objects.Add(newObject);
        }
    }
}

