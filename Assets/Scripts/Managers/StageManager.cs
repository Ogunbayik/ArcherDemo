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
            HandleStartStageSequence();
    }

    private void HandleStartStageSequence()
    {
        Sequence startSequence = DOTween.Sequence();
        //First Part Create VFX Smoke effect for coffin
        startSequence.AppendCallback(() => SpawnInitialCoffin());
        //Second Part Activate Coffins and Open Animation
        startSequence.AppendInterval(5f);
        startSequence.AppendCallback(() => ActivateCoffins());
    }

    private void SpawnInitialCoffin()
    {
        for (int i = 0; i < _currentStage.MaxStageEnemyCount; i++)
        {
            var randomX = Random.Range(_currentStage.MinimumBorderX, _currentStage.MaximumBorderX);
            var randomZ = Random.Range(_currentStage.MinimumBorderZ, _currentStage.MaximumBorderZ);
            var randomPosition = new Vector3(randomX, 0f, randomZ);
            
            //Create VFX Effect with coffin
            var coffin = Instantiate(_coffinPrefab);
            coffin.transform.position = randomPosition;
            coffin.gameObject.SetActive(false);
            _coffinList.Add(coffin);
        }
    }
    private void ActivateCoffins()
    {
        foreach (var coffin in _coffinList)
        {
            coffin.gameObject.SetActive(true);
        }
    }



}
