using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MapMoveSystem : StateSystemComponent<MapMoveSystem.MapMoveSystemStateType>
    {
        public enum MapMoveSystemStateType
        {
            GoingToThePoint,
            ReachedThePoint,
            Sleep
        }

        public NavMeshAgent Agent { get; set; }


        private Vector3 _destinationPosition;
        private IEnumerator _reachPointCoroutine;


        private void Update()
        {
            Debug.DrawLine(this.transform.position, _destinationPosition);
        }

        public void SetDestinationPosition(Vector3 position)
        {
            if (_destinationPosition == position)
            {
                Agent.SetDestination(position);
                return;
            }

            _destinationPosition = position;
            Agent.SetDestination(position);
            ChangeState(MapMoveSystemStateType.GoingToThePoint);

            if (_reachPointCoroutine != null) StopCoroutine(_reachPointCoroutine);
            _reachPointCoroutine = ReachPointCoroutine(_destinationPosition);
            StartCoroutine(_reachPointCoroutine);
        }

        public void Stop()
        {
            Agent.isStopped = true;
            SetState(MapMoveSystemStateType.Sleep);
        }


        public bool CanGetToPosition(Vector3 position)
        {
            var path = new NavMeshPath();
            Agent.CalculatePath(position, path);

            return path.status == NavMeshPathStatus.PathComplete;
        }


        private IEnumerator ReachPointCoroutine(Vector3 destination)
        {
            while (true)
            {
                if (!Agent.pathPending)
                {
                    if (Agent.isOnNavMesh && Agent.remainingDistance <= Agent.stoppingDistance)
                    {
//                        if (!Agent.hasPath || Math.Abs(Agent.velocity.sqrMagnitude) == 0.0f)
//                        {
//                        if (Math.Abs((this.transform.position - destination).magnitude) < 10f)
//                        {
                        ChangeState(MapMoveSystemStateType.ReachedThePoint);
                        yield break;
//                        }

//                        }
                    }
                }

                yield return null;
            }
        }
    }
}