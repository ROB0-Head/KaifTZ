using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private Transform[] _wayPoint;
    [SerializeField] private float _secondsForSuperMove;
    
    private int _currentWayPoint;
    private float _escapeTime = 0;
    private void Start()
    {
        _currentWayPoint = Random.Range(0, _wayPoint.Length);
    }

    private void Update()
    {
        _escapeTime += Time.deltaTime;
        if (_escapeTime >= Random.Range(3, _secondsForSuperMove))
        {
            _escapeTime = 0;
            Mover(500);
        }
        else Mover(1);
        if (transform.position == _wayPoint[_currentWayPoint].transform.position)
        {
            _currentWayPoint = Random.Range(0, _wayPoint.Length);
        }
    }
    private void Mover(int superMove)
    {
        transform.position = Vector3.MoveTowards(transform.position,_wayPoint[_currentWayPoint].transform.position,_speed * superMove * Time.deltaTime);
        transform.LookAt(_wayPoint[_currentWayPoint]);
    }

    public void Init(Transform[] wayPoints)
    {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            _wayPoint[i] = wayPoints[i];
        }
    }
    
    public void Die()
    {
        gameObject.SetActive(false);
    }
}
