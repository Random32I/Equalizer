using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health = 120f;
    [SerializeField] Projectile projectile;
    public GameManager game;
    public Player player;

    public FMODUnity.EventReference attackSFX;
    FMOD.Studio.EventInstance attack;


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

    public void TakeDamage()
    {
        attack = FMODUnity.RuntimeManager.CreateInstance(attackSFX);
        attack.start();

        switch ((Mathf.Round(game.interval * 2f) / 2f) % 1)
        {
            case 0:
            if (health > 0)
                health -= 1; 
            break;
        }
    }
}
