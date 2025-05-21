using UnityEngine;

public class PreserveRotationRelative : MonoBehaviour
{
    [SerializeField] private GameObject parent; // The parent object
    [SerializeField] private float rotationOffset; // Offset in degrees

    private Quaternion initialRelativeRotation; // The initial relative rotation

    void Start()
    {
        if (parent != null)
        {
            // Calculate the initial relative rotation
            initialRelativeRotation = Quaternion.Inverse(parent.transform.rotation) * transform.rotation;
        }
    }

    void Update()
    {
        if (parent != null)
        {
            // Preserve the relative rotation dynamically
            transform.rotation = parent.transform.rotation * initialRelativeRotation * Quaternion.Euler(0, 0, rotationOffset);
        }
    }
}