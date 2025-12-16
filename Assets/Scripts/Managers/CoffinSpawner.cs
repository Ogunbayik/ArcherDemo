using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoffinSpawner : MonoBehaviour
{
    private GameObject _enemyPrefab;
    private void Start()
    {
        StartSpawnSequnce();
    }

    private void StartSpawnSequnce()
    {
        Sequence spawnSequence = DOTween.Sequence();
        //First Part waiting VFX Effect for half
        spawnSequence.AppendInterval(2f);
        //Coffin Move Y coordinate than open
        spawnSequence.AppendCallback(() => SummonAnimation());
        spawnSequence.AppendInterval(2f);
        //Coffin is Opening
        spawnSequence.AppendCallback(() => OpenAnimation());
        spawnSequence.AppendInterval(1f);
        //Creaing Enemy
        spawnSequence.AppendCallback(() => SpawnEnemy());
        spawnSequence.JoinCallback(() => DestroyAnimation());
    }
    private void SummonAnimation()
    {
        Debug.Log("Coffin is Summoning");
    }
    private void OpenAnimation()
    {
        Debug.Log("Coffin is Opening");
    }
    private void DestroyAnimation()
    {
        Debug.Log("Coffin is Destroying");
    }

    private void SpawnEnemy()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.transform.position = this.transform.position;
        enemy.transform.rotation = Quaternion.identity;
    }

    public void SetEnemyPrefab(GameObject prefab)
    {
        _enemyPrefab = prefab;
    }



  
}
