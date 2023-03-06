using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float AngleInDegrees;
    [SerializeField] private int _bulletCount;

    private float _escapeTime = 0;
    private Vector3 _fromTo;
    private Vector3 _fromToXZ;
    
    
    private float g = Physics.gravity.y;
    private GameObject _target;
    public int CurrentBulletCount { get; private set; }
    
    public event UnityAction<int> BulletCountChanged;
    private void Start()
    {
        CurrentBulletCount = _bulletCount;
    }

    private void Update()
    {
        _escapeTime += Time.deltaTime;
        _shootPoint.localEulerAngles = new Vector3(-AngleInDegrees, 0f, 0f);
        if (Input.GetMouseButtonDown(0))
        {
            if (_escapeTime >= 0.5 && CurrentBulletCount != 0)
            {
                _escapeTime = 0;
                BulletCount(-1);
                Shoot();
            }
        }

        if (_escapeTime >= 1 && CurrentBulletCount != _bulletCount)
        {
            BulletCount(1);
        }
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)); 
        RaycastHit hit; 

        if (Physics.Raycast(ray, out hit))
        {
            _fromTo =hit.point  - transform.position;
            _fromToXZ = new Vector3(_fromTo.x, 0f, _fromTo.z);
            transform.rotation = Quaternion.LookRotation(_fromToXZ, Vector3.up);
            float x = _fromToXZ.magnitude;
            float y = _fromTo.y;
            float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

            float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
            float v = Mathf.Sqrt(Mathf.Abs(v2));

            GameObject newBullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().velocity = _shootPoint.forward * v;
        }
    }

    private void BulletCount(int change)
    {
        CurrentBulletCount += change;
        BulletCountChanged?.Invoke(CurrentBulletCount);
    }
}