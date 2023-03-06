using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class Spawner : ObjectPool
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyTemplate;
    [SerializeField] private float _secondsBetweenSpawn;
    
    private float _escapeTime = 0;
    public int CurrentEnemyCount { get; private set; }
    public event UnityAction<int> EnemyCountChanged;

    private void Start()
    {
        Inicialize(_enemyTemplate);
        CurrentEnemyCount = -3;
    }

    private void Update()
    {
        _escapeTime += Time.deltaTime;
        
        if (_escapeTime >= Random.Range(2,_secondsBetweenSpawn))
        {
            if (TryGetObject(out GameObject enemy))
            {
                _escapeTime = 0;
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                SetEnemy(enemy,_spawnPoints[spawnPointNumber].position);
            }
        }
    }

    private void SetEnemy(GameObject enemy, Vector3 spawnPoint)
    {
        enemy.SetActive(true);
        CurrentEnemyCount++;
        if (CurrentEnemyCount >= 0)
        {
            ScoreCount();
        }
        enemy.transform.position = spawnPoint;
    }

    private void ScoreCount()
    {
        CurrentEnemyCount++;
        EnemyCountChanged?.Invoke(CurrentEnemyCount);
    }
}
