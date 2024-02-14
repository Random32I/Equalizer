using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rig;
    [SerializeField] GameObject boss;
    [SerializeField] GameManager game;
    [SerializeField] float speed;
    public float health = 176f;

    [SerializeField] float lane = 3;

    float timeCounter;

    bool isMoving;
    public bool canMove = true;


    bool isParrying;
    bool canParry = true;
    bool failedParry;

    [Header("Sound Effects")]
    [SerializeField] AudioSource bossHit;
    [SerializeField] AudioSource hitNoise;
    [SerializeField] AudioSource parry;
    [SerializeField] AudioSource switchLanes;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isParrying && failedParry)
        {
            isParrying = false;
            failedParry = false;
            canParry = true;
            GetComponent<BoxCollider>().size /= 6;
        }

        transform.rotation = Quaternion.LookRotation(new Vector3(0, transform.position.y, 0) - transform.position, transform.up);
        transform.Rotate(Vector3.up * 90);

        float radius = 0.8f * lane - 1 + 1.64f;

        if (canMove)
        {
            if (Input.GetKey(KeyCode.D))
            {
                timeCounter -= speed;
                transform.position = Quaternion.AngleAxis(timeCounter, Vector3.up) * new Vector3(radius, 0f);
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                timeCounter += speed;
                transform.position = Quaternion.AngleAxis(timeCounter, Vector3.up) * new Vector3(radius, 0f);
                isMoving = true;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                rig.velocity = Vector3.zero;
                isMoving = false;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                rig.velocity = Vector3.zero;
                isMoving = false;
            }



            if (Input.GetKeyDown(KeyCode.W))
            {
                if (lane > 1)
                {
                    //switch ((Mathf.Round(game.interval * 2f) / 2f) % 1)
                    //{
                    //case 0:
                    lane--;
                    if (isMoving)
                    {
                        transform.position += transform.right * 0.8f;
                    }
                    else
                    {
                        transform.position -= transform.right * 0.8f;
                    }
                    switchLanes.Play();
                    //break;
                    //}
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (lane < 3)
                {
                    //switch ((Mathf.Round(game.interval * 2f) / 2f) % 1)
                    //{
                    //case 0:
                    lane++;
                    if (isMoving)
                    {
                        transform.position -= transform.right * 0.8f;
                    }
                    else
                    {
                        transform.position += transform.right * 0.8f;
                    }
                    switchLanes.Play();
                    //break;
                    //}
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (lane == 1)
                {
                    boss.GetComponent<Boss>().TakeDamage(true);
                    bossHit.Play();
                }
            }
            else if (Input.GetMouseButtonDown(1) && canParry)
            {
                isParrying = true;
                canParry = false;
                GetComponent<BoxCollider>().size *= 6;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Projectile")
        {
            if (!isParrying)
            {
                if (health > 0)
                {
                    hitNoise.Play();
                    health -= 4f;
                    Destroy(other.gameObject);
                }
            }
            else
            {
                other.GetComponent<Rigidbody>().velocity *= -1;
                other.name = "Reflected Projectile";
                isParrying = false;
                canParry = true;
                GetComponent<BoxCollider>().size /= 6;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isParrying)
        {
            failedParry = true;
        }
    }

    public void ToggleMovement(bool state)
    {
        canMove = state;
    }
}
