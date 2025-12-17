using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoffinSpawner : MonoBehaviour
{
    private CoffinAnimationController animationController;

    private GameObject _enemyPrefab;
    private void Awake()
    {
        animationController = GetComponent<CoffinAnimationController>();
    }
    private void Start()
    {
        StartSpawnSequnce();
    }
    private void StartSpawnSequnce()
    {
        Sequence spawnSequence = DOTween.Sequence();
        //Coffin Move Y coordinate than open
        spawnSequence.AppendCallback(() => animationController.PlaySummonAnimation());
        spawnSequence.AppendInterval(2f);
        //Coffin is Opening
        spawnSequence.AppendCallback(() => animationController.PlayOpenAnimation());
        spawnSequence.AppendInterval(3f);
        //Creaing Enemy
        spawnSequence.AppendCallback(() => SpawnEnemy());
        spawnSequence.JoinCallback(() => animationController.PlayDestroyAnimation());
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
