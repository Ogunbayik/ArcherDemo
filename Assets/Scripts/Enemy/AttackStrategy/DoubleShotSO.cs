using UnityEngine;

[CreateAssetMenu(fileName = "New Double Shot", menuName = "ScriptableObject/AttackStrategy/Double")]
public class DoubleShotSO : AttackStrategySO
{
    [Header("Double Shot Settings")]
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float distanceToPrefab;
    [SerializeField] private float prefabSpeed;

    private int prefabCount = 2;
    public override void ExecuteAttack(EnemyBase enemy)
    {
        for (int i = 0; i < prefabCount; i++)
        {
            var prefab = Instantiate(attackPrefab);
            var isEvenIndex = i % prefabCount;

            if (isEvenIndex == 0)
                prefab.transform.position = enemy.AttackTransform.position + new Vector3(distanceToPrefab, 0, 0);
            else
                prefab.transform.position = enemy.AttackTransform.position + new Vector3(-distanceToPrefab, 0, 0);

            prefab.transform.rotation = enemy.transform.rotation;
            prefab.GetComponent<Arrow>().SetupArrow(prefabSpeed);
        }


    }
}
