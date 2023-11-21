using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject projectile;
    [SerializeField] GameObject player;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameObject.name == "Temp Projectile")
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
