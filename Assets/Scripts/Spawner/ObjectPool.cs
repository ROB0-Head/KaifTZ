using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
  [SerializeField] private GameObject _container;
  [SerializeField] private int _capacity;
  
  private List<GameObject> _pool = new List<GameObject>();
  
  protected void Inicialize(GameObject enemy)
  {
      for (int i = 0; i < _capacity; i++)
      {
          GameObject spawnedEnemy = Instantiate(enemy, _container.transform);
          spawnedEnemy.SetActive(false);
          _pool.Add(spawnedEnemy);
      }
  } protected void Inicialize(GameObject[] enemy)
  {
      for (int i = 0; i < _capacity; i++)
      {
          int randomIndex = Random.Range(0, enemy.Length);
          GameObject spawnedEnemy = Instantiate(enemy[randomIndex], _container.transform);
          spawnedEnemy.SetActive(false);
          _pool.Add(spawnedEnemy);
      }
  }

  protected bool TryGetObject(out GameObject result)
  {
      result = _pool.FirstOrDefault(p => p.activeSelf == false);
      return result != null;
  }
}
