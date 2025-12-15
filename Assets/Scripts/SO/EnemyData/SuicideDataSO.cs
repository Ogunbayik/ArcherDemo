using UnityEngine;

[CreateAssetMenu(fileName = "New Suicide Enemy", menuName = "ScriptableObject/Enemy/Suicide")]
public class SuicideDataSO : BaseEnemyDataSO
{
    [Header("Suicide Settings")]
    [SerializeField] private float _suicideTime;
    [SerializeField] private float _explosionRadius;

    public float SuicideTime => _suicideTime;
    public float ExplosionRadius => _explosionRadius;
}
