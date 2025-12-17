using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;

    [Header("VFX Settings")]
    [SerializeField] private GameObject smokeEffect;
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

    public void PlaySmokeVFX(Vector3 spawnPosition)
    {
        var smoke = Instantiate(smokeEffect);
        smoke.transform.position = spawnPosition;
    }
}
