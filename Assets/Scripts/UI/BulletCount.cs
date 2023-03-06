using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class BulletCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _count;
    [SerializeField] private Shooting _bullets;
    
    private void OnEnable()
    {
        _count.text = _bullets.CurrentBulletCount.ToString();
        _bullets.BulletCountChanged += OnBulletCountChanged;
    }

    private void OnDisable()
    {
        _bullets.BulletCountChanged -= OnBulletCountChanged;
    }

    private void OnBulletCountChanged(int count)
    {
        _count.text = count.ToString();
    }
}
