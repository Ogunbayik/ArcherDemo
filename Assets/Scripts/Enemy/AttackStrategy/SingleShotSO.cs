using UnityEngine;

[CreateAssetMenu(fileName = "New Single Shot", menuName = "ScriptableObject/AttackStrategy/Single")]
public class SingleShotSO : AttackStrategySO
{
    [Header("Single Shot Settings")]
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float prefabSpeed;
    public override void ExecuteAttack(EnemyBase enemy)
    {
        var prefab = Instantiate(attackPrefab);
        prefab.transform.position = enemy.transform.position;
        prefab.transform.rotation = enemy.transform.rotation;
        prefab.GetComponent<Arrow>().SetupArrow(prefabSpeed);
    }

}
