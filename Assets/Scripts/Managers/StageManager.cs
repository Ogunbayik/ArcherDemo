using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    [Header("Stage Settings")]
    [SerializeField] private List<StageDataSO> _stageDataList = new List<StageDataSO>();
    [SerializeField] private GameObject _coffinPrefab;

    private List<GameObject> _coffinList = new List<GameObject>();
    private List<GameObject> _enemyList;

    private StageDataSO _currentStage;

    private int _currentStateIndex = 2;

    public StageDataSO CurrentStage => _currentStage;
    private void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }
    private void Start()
    {
        InitiliazeStage();
        SetupEnemyList();
    }
    private void InitiliazeStage()
    {
        _currentStage = _stageDataList[_currentStateIndex];
    }
    private void SetupEnemyList()
    {
        _enemyList = new List<GameObject>();

        foreach (var entry in _currentStage.EnemyCountEntry)
        {
            for (int i = 0; i < entry.EnemyCount; i++)
            {
                _enemyList.Add(entry.EnemyPrefab);
            }
        }
        GameUtils.Shuffle(_enemyList);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnInitialCoffin();
    }
    private void SpawnInitialCoffin()
    {
        //When Player Enter the Button All coffins spawning

        for (int i = 0; i < _currentStage.MaxStageEnemyCount; i++)
        {
            var randomX = Random.Range(_currentStage.MinimumBorderX, _currentStage.MaximumBorderX);
            var randomZ = Random.Range(_currentStage.MinimumBorderZ, _currentStage.MaximumBorderZ);
            var randomPosition = new Vector3(randomX, 0f, randomZ);
            
            //Create VFX Effect with coffin
            var coffin = Instantiate(_coffinPrefab);
            var enemy = _enemyList[i];
            coffin.transform.position = randomPosition;
            coffin.GetComponent<CoffinSpawner>().SetEnemyPrefab(enemy);
            _coffinList.Add(coffin);
            _enemyList.Remove(enemy);
        }
    }



}
