using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

internal class Enemy : MonoBehaviour, IDamageble, IPoolElement
{
    private EnemyInfo _config;
    private IDamageble _target;
    private int _currentHealth;
    private StateFactory _stateFactory;
    private EnemyStateMachine _stateMachine;

    public event UnityAction<Enemy> Die;

    public void Init(EnemyInfo config, IDamageble target)
    {
        _config = config;
        Level = (int)config.Type;
        _target = target;
        _currentHealth = 0;
        _stateFactory = new StateFactory(this, target);
        _stateFactory.CreateStates();
        _stateMachine = new EnemyStateMachine(_stateFactory);
        _stateMachine.Reset();
    }

    public int Level { get; private set; }
    public bool IsAlive => _currentHealth > 0;

    public int Health => _currentHealth;
    public EnemyInfo Config => _config;
    public Vector3 Position => transform.position;

    public void Update() => _stateMachine.Update(Time.deltaTime);

    public void Respawn() => _currentHealth = _config.Health;

    public Transform GetTransform() => transform;

    public void ApplyDamage(int damage)
    {
        if (damage > 0)
            _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Die?.Invoke(this);
        }
    }
}