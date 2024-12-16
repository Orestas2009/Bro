using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float scale;
    Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        var pos = cam.transform.position / scale;
        pos.z = 0;
        transform.position = pos;
    }
}