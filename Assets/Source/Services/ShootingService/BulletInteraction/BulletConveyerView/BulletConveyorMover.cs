using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class BulletConveyorMover
{
    private List<Transform> _bulletPathPoints;

    private float _bulletSpeed = 5f;

    public event Action<BulletView> Arrived;

    public BulletConveyorMover(List<Transform> bulletPathPoints)
    {
        _bulletPathPoints = bulletPathPoints;
    }

    public IEnumerator Move(BulletView view)
    {   
        foreach (var pathPoint in _bulletPathPoints)
        {
            while (Vector3.Distance(pathPoint.position, view.transform.position) > 0.1f)
            {
                view.transform.position = Vector3.MoveTowards(view.transform.position, pathPoint.position, _bulletSpeed * Time.deltaTime);
                yield return null;
            }
        }

        Arrived?.Invoke(view);
    }
}