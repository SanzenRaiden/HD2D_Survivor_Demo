using System.Collections;
using UnityEngine;

public class DeletSelf : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(isDelet());
    }

    public IEnumerator isDelet()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
