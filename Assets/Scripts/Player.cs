using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rig;
    [SerializeField] GameObject boss;
    [SerializeField] float speed;
    public float health = 120f;

    int lane = 3;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(boss.transform.position - transform.position, transform.up);
        if (Input.GetKey(KeyCode.D))
        {
            rig.velocity = transform.right * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rig.velocity = transform.right * -speed;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (lane > 1)
            {
                lane--;
                transform.position += transform.forward * 2;
            }

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (lane < 3)
            {
                lane++;
                transform.position += transform.forward * -2;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (lane == 1)
            boss.GetComponent<Boss>().TakeDamage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Projectile")
        {
            if (health > 0)
            health -= 10f;
        }
    }
}
