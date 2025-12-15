using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyCountEntry
{
    // Anahtar (Key): Hangi Düþman Data'sý
    public GameObject EnemyPrefab;
    // Deðer (Value): Kaç Adet Üretileceði
    public int EnemyCount;
}


[CreateAssetMenu(fileName = "Stage Data", menuName = "ScriptableObject/Stage")]
public class StageDataSO : ScriptableObject
{
    [Header("Stage Settings")]
    [SerializeField] private List<EnemyCountEntry> _enemyCountEntry = new List<EnemyCountEntry>();
    [SerializeField] private int _maxStageEnemyCount;
    [Header("Border Settings")]
    [SerializeField] private float _minimumBorderX;
    [SerializeField] private float _maximumBorderX;
    [SerializeField] private float _minimumBorderZ;
    [SerializeField] private float _maximumBorderZ;

    [Header("Test")]
    [SerializeField] private Dictionary<GameObject ,int> _enemyList = new Dictionary<GameObject,int>();


    public Dictionary<GameObject,int> EnemyList => _enemyList;
    public List<EnemyCountEntry> EnemyCountEntry => _enemyCountEntry;
    public int MaxStageEnemyCount => _maxStageEnemyCount;
    public float MinimumBorderX => _minimumBorderX;
    public float MaximumBorderX => _maximumBorderX;
    public float MinimumBorderZ => _minimumBorderZ;
    public float MaximumBorderZ => _maximumBorderZ;
}
