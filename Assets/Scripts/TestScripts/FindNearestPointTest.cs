using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestPointTest : MonoBehaviour
{
    public GameObject wall;
    private Collider _collider;
    private Vector3 _closestPoint;

    // Start is called before the first frame update
    void Start()
    {
        _collider = wall.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        _closestPoint = _collider.ClosestPoint(position);
        Debug.DrawLine(position, _closestPoint, Color.red);
    }
}