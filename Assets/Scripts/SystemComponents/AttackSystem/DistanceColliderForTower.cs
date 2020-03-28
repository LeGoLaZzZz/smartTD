using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(BoxCollider))]
    public class DistanceColliderForTower : MonoBehaviour
    {    
        private StandartTowerAttackSystem _standartTowerAttackSystem;

        private void Awake()
        {
            _standartTowerAttackSystem = GetComponentInParent<StandartTowerAttackSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.gameObject;
            var attackable = enemy.GetComponent<IAttackable>();
            if (attackable != null)
            {
                _standartTowerAttackSystem.NewTargetEnter(attackable);
            }
        }


        private void OnTriggerExit(Collider other)
        {
            var enemy = other.gameObject;
            var attackable = enemy.GetComponent<IAttackable>();
            if (attackable != null)
            {
                _standartTowerAttackSystem.NewTargetExit(attackable);
            }
        }
    }
}