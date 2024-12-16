using UnityEngine;


[DefaultExecutionOrder(-10000)]
public class CameraFollow : MonoBehaviour
{
    public float speed;
    public Transform target;

    void LateUpdate()
    {
        Vector3 targetPos = target.position;
        targetPos.z = -10;

        transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }
}