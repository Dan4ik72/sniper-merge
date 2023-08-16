using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using VContainer;

public class GameBootstrapper : MonoBehaviour, IDisposable
{
    [Inject] private MergeService _mergeService;
    [Inject] private ShootingService _shootingService;
    [Inject] private EnemiesService _enemiesService;
    [Inject] private InputService _inputService;
    [Inject] private ObjectDragService _objectDragService;
    [Inject] private BulletSpawnService _bulletSpawnService;
    [Inject] private InspectorCompletedLevelService _inspectorCompletedLevelService;

    private void Start()
    {
        _inputService.Init();
        _objectDragService.Init();
        _mergeService.Init();
        _bulletSpawnService.Init();
        _inspectorCompletedLevelService.Init(_enemiesService.Enemies, _shootingService.Gun);
        _shootingService.Init(_enemiesService.Enemies);
        _enemiesService.Init(_shootingService.Gun);
    }

    private void Update()
    {
        _objectDragService.Update();
        _shootingService.Update(Time.deltaTime);
        _enemiesService.Update(Time.deltaTime);
        _inputService.Update();
    }
    
    public void Dispose()
    {
        _objectDragService.Disable();
        _mergeService.Disable();
        _shootingService.Disable();
        _inputService.Disable();
    }
}
