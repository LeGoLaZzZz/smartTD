//using System;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using UnityEngine;
//
//
//namespace DefaultNamespace
//{
//    public class GameContainer : MonoBehaviour
//    {
//        private IContainer _container;
//
//        private void Awake()
//        {
//            _container = new Container();
//        }
//
//
//        public void RegisterObjectInContainer(IComponent component, string key)
//        {
//            _container.Add(component, key);
//        }
//
//
//        public void GetObjectFromContainer(string key)
//        {
//            var logger = _container.;
//        }
//    }
//}