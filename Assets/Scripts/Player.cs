using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rig;
    [SerializeField] GameObject boss;
    [SerializeField] GameManager game;
    [SerializeField] float speed;
    public float health = 120f;

    float lane = 3;

    float timeCounter;

    public FMODUnity.EventReference ouchSFX;
    FMOD.Studio.EventInstance ouch;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(0, transform.position.y, 0) - transform.position, transform.up);
        transform.Rotate(Vector3.up * 90);

        if (Input.GetKey(KeyCode.D))
        {
            rig.velocity = transform.forward * speed;
            speed = 9;
            speed = 10;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rig.velocity = transform.forward * -speed;
            speed = 9;
            speed = 10;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rig.velocity = Vector3.zero;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rig.velocity = Vector3.zero;
        }

        

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (lane > 1)
            {
                switch ((Mathf.Round(game.interval * 2f) / 2f) % 1)
                {
                    case 0:
                        lane--;
                        transform.position += transform.forward * 0.8f;
                        break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (lane < 3)
            {
                switch ((Mathf.Round(game.interval * 2f) / 2f) % 1)
                {
                    case 0:
                        lane++;
                        transform.position += transform.forward * -0.8f;
                        break;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (lane == 1)
            boss.GetComponent<Boss>().TakeDamage();
        }
        transform.position = (new Vector3(0, transform.position.y, 0) - transform.position).normalized * -(0.8f * lane - 1 + 1.64f);
    }

    private void LateUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ouch = FMODUnity.RuntimeManager.CreateInstance(ouchSFX);
        ouch.start();

        if (other.name == "Projectile")
        {
            if (health > 0)
            health -= 10f;
        }
    }
}
