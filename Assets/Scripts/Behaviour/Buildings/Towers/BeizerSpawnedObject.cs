using UnityEngine;

namespace beizerTest
{
    public class BeizerSpawnedObject : MonoBehaviour
    {
        private float _timeForBeizer;
        private float _time;
        private Vector2 _speedPoint1;
        private Vector2 _speedPoint2;


        // точки для полета снаряда (кубик бейзер)
        private Vector3 _p0;
        private Vector3 _p1;
        private Vector3 _p2;
        private Transform _target;


        private void Awake()
        {
            _time = 0;
            _timeForBeizer = 0;
        }

        public void FixedUpdate()
        {
            _time = CalculateBezier(_timeForBeizer);
            _timeForBeizer += 0.008f;
            SetPosition(_time);


            if (_timeForBeizer >= 1)
            {
                Destroy(gameObject);
            }
        }


        public void SetTarget(Vector3 p0, Transform target, Vector2 speed1, Vector2 speed2)
        {
            _p0 = p0;
            _target = target;
            _speedPoint1 = speed1;
            _speedPoint2 = speed2;
        }


        /// <summary>
        /// скорость изменения кривая бейзера
        /// </summary>
        /// <param name="t">прогресс</param>
        /// <returns></returns>
        private float CalculateBezier(float t)
        {
            var p0 = new Vector2(0, 0);

            var p1 = _speedPoint1;
            var p2 = _speedPoint2;

            var p3 = new Vector2(1, 1);

            var u = 1 - t;
            var tt = t * t;
            var uu = u * u;
            var uuu = uu * u;
            var ttt = tt * t;
            var p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p.y;
        }


        /// <summary>
        /// айти вектор полета снаряда по кубик бейзера по прогресу
        /// </summary>
        /// <param name="t">прогрес</param>
        /// <returns></returns>
        private Vector3 CalculateBezierVector(float t)
        {
            var u = 1 - t;
            var tt = t * t;
            var uu = u * u;
            var uuu = uu * u;
            var ttt = tt * t;
            var p = uuu * _p0;

            if (!_target)
            {
                Destroy(gameObject);
                return Vector3.zero;
            }

            _p1 = (_p0 + _target.position) * 0.3f;
            _p2 = (_p0 + _target.position) * 0.6f;

            p += 3 * uu * t * _p1;
            p += 3 * u * tt * _p2;
            p += ttt * _target.position;

            return p;
        }


        private void SetPosition(float t)
        {
            var vector3 = CalculateBezierVector(t);

            Debug.DrawLine(transform.position, vector3, Color.red, 1f);

            transform.position = vector3;
        }
    }
}