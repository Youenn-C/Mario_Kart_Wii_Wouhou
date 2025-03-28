using System.Collections;
using UnityEngine;

public class LifeTimeItemLaunchable : MonoBehaviour
{
     [SerializeField] private GameObject itemLaunchablePrefab;
    void Start()
    {
        StartCoroutine(LifeTime());
        Debug.Log("LifeTimeItemLaunchable");
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(itemLaunchablePrefab);
        Debug.Log("Life is Done");
    }
}
