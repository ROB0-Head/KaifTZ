using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Spawner _enemyCount;
    
    private void OnEnable()
    {
        _score.text = _enemyCount.CurrentEnemyCount.ToString();
        _enemyCount.EnemyCountChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        _enemyCount.EnemyCountChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int count)
    {
        _score.text = count.ToString();
    }
}
