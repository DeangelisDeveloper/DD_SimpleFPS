using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject enemy = null;
    [SerializeField] Transform[] spawners = null;
    [SerializeField] float spawnTime = 0f;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnTime);
        
        for (int i = 0; i < spawners.Length; i++)
            Instantiate(enemy, spawners[i].position, Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(Spawn());
    }
}
