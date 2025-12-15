using UnityEngine;
[CreateAssetMenu(fileName = "New Spread Attack", menuName = "ScriptableObject/AttackStrategy/Spread")]
public class SpreadAttackSO : AttackStrategySO
{
    [Header("Spread Attack Settings")]
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private int prefabCount;
    [SerializeField] private float prefabSpeed;
    [SerializeField] private float prefabDegree;

    public override void ExecuteAttack(EnemyBase enemy)
    {
        var isEvenPrefab = prefabCount % 2 == 0;
        if (isEvenPrefab)
        {
            Debug.LogWarning("Spread Shot Strategy is not suitable for even numbers");
            prefabCount = Mathf.Max(1, prefabCount - 1);
        }

        for (int i = 0; i < prefabCount; i++)
        {
            var prefab = Instantiate(attackPrefab);
            prefab.transform.position = enemy.GetAttackTransform().position;
            prefab.transform.rotation = enemy.transform.rotation;

            var rotationMultiplier = Mathf.Ceil((float)i / 2f);

            var isEvenIndex = i % 2 == 0;
            if (isEvenIndex)
                prefab.transform.Rotate(0f, prefabDegree * rotationMultiplier, 0f);
            else
                prefab.transform.Rotate(0f, prefabDegree * -rotationMultiplier, 0f);
            
            prefab.GetComponent<Arrow>().SetupArrow(prefabSpeed);
        }

    }
}
