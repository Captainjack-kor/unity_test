using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        // 사용자마다 다른 frame에 같은 속도를 주기 위해 역을 곱해준다.
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}
