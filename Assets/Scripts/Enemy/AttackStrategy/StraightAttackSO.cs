using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Range Attack", menuName = "ScriptableObject/AttackStrategy/Straight")]
public class StraightAttackSO : AttackStrategySO
{
    [Header("Straight Strategy Settings")]
    [SerializeField] private Transform attackPrefab;
    [SerializeField] private float prefabCount;
    [SerializeField] private float prefabSpeed;
    [SerializeField] private float distanceToPrefab;

    private int maxPrefabCount = 5;

    private Vector3 spawnLocalPosition;
    public override void ExecuteAttack(EnemyBase enemy)
    {
        if (prefabCount > maxPrefabCount)
            return;

        for (int i = 0; i < prefabCount; i++)
        {
            var prefab = Instantiate(attackPrefab);
            var isEvenPrefab = prefabCount % 2;

            if (isEvenPrefab == 0)
            {
                var isEvenIndex = i % 2; //sað, sol
                var distanceMultiplier = Mathf.Floor((float)i / 2);

                if(distanceMultiplier == 0)
                {
                    if(isEvenIndex == 0)
                        spawnLocalPosition = enemy.AttackTransform.localPosition + new Vector3(distanceToPrefab / prefabCount, 0f, 0f);
                    else
                        spawnLocalPosition = enemy.AttackTransform.localPosition + new Vector3(-distanceToPrefab / prefabCount, 0f, 0f);
                }
                else
                {
                    if (isEvenIndex == 0)
                        spawnLocalPosition = enemy.AttackTransform.localPosition + new Vector3(distanceToPrefab * distanceMultiplier, 0f, 0f);
                    else
                        spawnLocalPosition = enemy.AttackTransform.localPosition + new Vector3(-distanceToPrefab * distanceMultiplier, 0f, 0f);
                }
            }
            else
            {
                float distanceMultiplier = Mathf.Ceil((float)i / 2f);

                var isEvenIndex = i % 2;
                if(isEvenIndex == 0)
                    spawnLocalPosition = enemy.AttackTransform.localPosition + new Vector3(distanceToPrefab * distanceMultiplier, 0f, 0f);
                else
                    spawnLocalPosition = enemy.AttackTransform.localPosition + new Vector3(-distanceToPrefab * distanceMultiplier, 0f, 0f);
            }

            prefab.transform.position = enemy.AttackTransform.TransformPoint(spawnLocalPosition);
            prefab.transform.rotation = enemy.transform.rotation;
            prefab.GetComponent<Arrow>().SetupArrow(prefabSpeed);
        }

    }

}
