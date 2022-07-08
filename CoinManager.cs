using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject nemo;
    public Rigidbody2D cube;

    // 코루틴으로 NPC 이동 제한 [에니메이션은]
    IEnumerator MoveObject() {
        cube = GetComponent<Rigidbody2D>();

        while(true) {
            float dir1 = Random.Range(-3f, 3f);
            float dir2 = Random.Range(-3f, 3f);

            yield return new WaitForSeconds(2);
            cube.velocity = new Vector2(dir1, dir2);
        }
    }
    void Start()
    {
        StartCoroutine(MoveObject());
    }

    void Update()
    {
        
    }
}
