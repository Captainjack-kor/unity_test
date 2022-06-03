
using System.Collections;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    float time;
    IEnumerator myCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        myCoroutine = MyCoroutine(1.0f);
        StartCoroutine(myCoroutine);
        time = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0)
        {
            StopCoroutine(myCoroutine);
        }
    }

    IEnumerator MyCoroutine(float t)
    {
        Debug.Log("MyCoroutine;" + t);
        yield return StartCoroutine(MySecondCoroutine(.1f));

        while (true)
        {
            Debug.Log("MyCoroutine");
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator MySecondCoroutine(float t)
    {
        Debug.Log("MySecondCoroutine;" + t);
        yield return new WaitForSeconds(0.1f);
    }
}