using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rig;
    [SerializeField] GameObject boss;
    [SerializeField] float speed;
    [SerializeField] float health = 120f;

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Projectile")
        {
            health -= 10f;
        }
    }
}
