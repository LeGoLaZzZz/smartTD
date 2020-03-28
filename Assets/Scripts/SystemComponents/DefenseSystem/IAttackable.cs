using UnityEngine;

namespace DefaultNamespace
{
    public interface IAttackable
    {
        Vector3 GetPosition();
        Vector3 GetNearestPoint(Vector3 position);
        GameObject GetGameObject();
        bool IsGameObjectNull();
        DefenseSystem DefenseSystem { get; }
    }
}