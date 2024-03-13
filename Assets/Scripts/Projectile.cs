using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    GameObject projectile;
    [SerializeField] GameObject player;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (name == "Projectile")
        {
            if (((player.transform.position - transform.position).normalized - (Vector3.up * 0.2f - transform.position).normalized).magnitude > Random.Range(1.85f, 2))
            {
                GetComponent<Rigidbody>().velocity = (player.transform.position - transform.position).normalized * speed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }
    }

    public void FireProjectile()
    {
        Player playerComp = player.GetComponent<Player>();
        float x = player.GetComponent<Player>().timeCounter % 360;
        if (gameObject.name == "Temp Projectile")
        {
            projectile = Instantiate(gameObject);
            projectile.name = "Projectile";
            projectile.transform.position = Vector3.up * 0.2f;
            projectile.GetComponent<Rigidbody>().velocity = (player.transform.position - Vector3.up * 0.2f).normalized * speed;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.name == "Wall" || (collision.transform.name == "Boss" && name == "Reflected Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
