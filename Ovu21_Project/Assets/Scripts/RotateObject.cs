using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0f;
    private float rotationY = 0f;

    void FixedUpdate()
    {
        rotationY += 2f;

        transform.rotation = Quaternion.Euler(90f, rotationY * rotationSpeed, transform.rotation.z);
    }
}