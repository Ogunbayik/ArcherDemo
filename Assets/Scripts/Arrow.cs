using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float movementSpeed;

    public void SetupArrow(float speed)
    {
        movementSpeed = speed;
    }
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }
}
