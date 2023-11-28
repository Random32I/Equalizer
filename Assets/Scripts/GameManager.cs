using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Pausing")]
    [SerializeField] bool isPaused = false;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Animator anim;

    [Header("Music")]
    [SerializeField] AudioSource music;
    private float musicPauseTime;

    [Header("UI")]
    [SerializeField] TextMeshPro bossHealth;
    [SerializeField] TextMeshProUGUI playerHealth;

    [Header("Scripts")]
    [SerializeField] Boss boss;
    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossHealth.text = $"Health: {boss.health}";
        playerHealth.text = $"Health: {player.health}";

        bossHealth.transform.rotation = Quaternion.LookRotation(player.transform.position * -1); 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (!isPaused)
            {
                music.Play();
                music.time = musicPauseTime;
                Time.timeScale = 1;

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Close") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
                PauseMenu.SetActive(false);
            }
            else
            {
                musicPauseTime = music.time;
                music.Stop();
                Time.timeScale = 0;

                PauseMenu.SetActive(true);
            }

            anim.SetBool("Paused", isPaused);
        }
    }
}
