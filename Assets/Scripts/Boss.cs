using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Boss : MonoBehaviour
{
    public float health = 120f;
    [SerializeField] Projectile projectile;
    public GameManager game;
    public Player player;
    [SerializeField] ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(player.transform.position - new Vector3(0, player.transform.position.y, 0), transform.up);
        transform.Rotate(Vector3.up * 90);
    }

    public void TakeDamage(bool type)
    {
        //type true is melee, type false is range
        if (type)
        {
            switch ((Mathf.Round(game.interval * 2f) / 2f) % 1)
            {
                case 0:
                    if (health > 0)
                    {
                        particles.Play();
                        health -= 5;
                    }
                    break;
            }
        }
        else
        {
            if (health > 0)
            {
                particles.Play();
                health -= 10;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Reflected Projectile")
        {
            TakeDamage(false);
        }
    }
}
