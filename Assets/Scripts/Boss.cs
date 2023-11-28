using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float health = 120f;
    [SerializeField] Projectile projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        switch ((Mathf.Round(projectile.interval * 2f) / 2f) % 1)
        {
            case 0:
            if (health > 0)
                health -= 1; 
            break;
        }
    }
}
