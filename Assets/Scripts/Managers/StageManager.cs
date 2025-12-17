using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public static int _stageEnemyCount;

    [Header("Stage Settings")]
    [SerializeField] private List<StageDataSO> _stageDataList = new List<StageDataSO>();
    [SerializeField] private GameObject _coffinPrefab;

    private List<GameObject> _coffinList = new List<GameObject>();
    private List<GameObject> _enemyList;

    private StageDataSO _currentStage;

    private int _currentStateIndex = 2;
    private int _spawnCount;

    public StageDataSO CurrentStage => _currentStage;

    private bool _isInitialSpawnComplete = false;

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

        if (_isInitialSpawnComplete)
            SpawnCoffin();
    }
    private void SpawnInitialCoffin()
    {
        //When Player Enter the Button All coffins spawning
        for (int i = 0; i < _spawnCount; i++)
        {
            var randomSpawnPosition = GetRandomSpawnPosition();

            //Create VFX Effect with coffin
            VFXManager.Instance.PlaySmokeVFX(randomSpawnPosition);

            var coffin = Instantiate(_coffinPrefab);
            var enemy = _enemyList[i];

            coffin.transform.position = randomSpawnPosition;
            coffin.GetComponent<CoffinSpawner>().SetEnemyPrefab(enemy);
            _coffinList.Add(coffin);
            _enemyList.Remove(enemy);

            IncreaseStageEnemyCount();
        }

        _isInitialSpawnComplete = true;
    }
    private void SpawnCoffin()
    {
        if (CanSpawn())
        {
            var randomSpawnPosition = GetRandomSpawnPosition();

            VFXManager.Instance.PlaySmokeVFX(randomSpawnPosition);

            var firstEnemyIndex = 0;
            var coffin = Instantiate(_coffinPrefab);
            var enemy = _enemyList[firstEnemyIndex];

            coffin.transform.position = randomSpawnPosition;
            coffin.GetComponent<CoffinSpawner>().SetEnemyPrefab(enemy);
            _coffinList.Add(coffin);
            _enemyList.Remove(enemy);

            IncreaseStageEnemyCount();
        }
        else
            Debug.Log("All Enemies are spawned");
    }
    private bool CanSpawn()
    {
        return _enemyList.Count > 0 && _stageEnemyCount < _currentStage.MaxStageEnemyCount;
    }
    private Vector3 GetRandomSpawnPosition()
    {
        var randomX = Random.Range(_currentStage.MinimumBorderX, _currentStage.MaximumBorderX);
        var randomZ = Random.Range(_currentStage.MinimumBorderZ, _currentStage.MaximumBorderZ);
        var randomPosition = new Vector3(randomX, 0f, randomZ);

        return randomPosition;
    }
    public void IncreaseStageEnemyCount()
    {
        _stageEnemyCount++;
    }

    public void DecreaseStageEnemyCount()
    {
        _stageEnemyCount--;
    }
}
