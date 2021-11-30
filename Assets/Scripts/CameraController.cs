using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    public float minX = 0f, maxX = 10f;
    public float minY = 0f, maxY = 10f;
    void Update()
    {
        Vector3 pos = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }
}
