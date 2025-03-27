using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Transform CameraTransform;
    public Transform TargetTransform;


    void Update()
    { 
        CameraTransform.position = new Vector3 (TargetTransform.position.x, TargetTransform.position.y, CameraTransform.position.z);
    }
}
