using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action<MouseButtonClickArgs> LeftMouseButtonClick;


        public class MouseButtonClickArgs : EventArgs
        {
            public Vector3 MousePosition;

            public MouseButtonClickArgs(Vector3 mousePosition)
            {
                MousePosition = mousePosition;
            }
        }


        private static PlayerInput _instance;
        public static PlayerInput GetInstance() => _instance;

        private void Awake()
        {
            _instance = this;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnLeftMouseButtonClick(Input.mousePosition);
            }
        }

        protected virtual void OnLeftMouseButtonClick(Vector3 mousePosition)
        {
            LeftMouseButtonClick?.Invoke(new MouseButtonClickArgs(mousePosition));
        }
    }
}