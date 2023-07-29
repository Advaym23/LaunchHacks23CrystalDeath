using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    public Rigidbody2D bullet;


    public Transform barrel; 

    public float bulletSpeed = 20f;

    public float spawnTime = 1f;

    public float spawnDelay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomSpawnTimer());
    }

    // Update is called once per frame
    private void fireBullet()
    {
            var spawnBullet = Instantiate(bullet, barrel.position, barrel.rotation);
    }

    IEnumerator RandomSpawnTimer()
    {
        while (true)
        {
            fireBullet();
            yield return new WaitForSeconds(Random.Range(1, 3));

        }
    }
}
