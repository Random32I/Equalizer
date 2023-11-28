using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    GameObject projectile;
    [SerializeField] GameObject player;
    [SerializeField] float speed;

    [Header("Rhythm")]
    [SerializeField] AudioSource music;
    [SerializeField] AudioSource test;
    [SerializeField] float bpm;
    public float interval = 0;
    int lastInterval = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        interval = (music.timeSamples / (music.clip.frequency * (60 / (bpm * 2)))) - 0.027f;
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            if (lastInterval % 4 == 0)
            {
                FireProjectile();
            }
            /*switch (Mathf.FloorToInt(interval))
            {

            }*/
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        if (gameObject.name == "Temp Projectile")
        {
            projectile = Instantiate(gameObject);
            projectile.name = "Projectile";
            projectile.transform.position = Vector3.up * 2;
            projectile.GetComponent<Rigidbody>().velocity = (player.transform.position - Vector3.up * 2).normalized * speed;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.name == "Wall" || collision.transform.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
