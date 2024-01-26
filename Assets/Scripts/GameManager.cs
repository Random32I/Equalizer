using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Pausing")]
    [SerializeField] bool isPaused = false;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Animator anim;

    [Header("Music")]
    [SerializeField] AudioSource music;
    private float musicPauseTime;
    [SerializeField] AudioSource charge;
    [SerializeField] AudioSource fire;

    [Header("UI")]
    [SerializeField] TextMeshPro bossHealth;
    [SerializeField] TextMeshProUGUI playerHealth;
    [SerializeField] TextMeshProUGUI menuText;

    [Header("Scripts")]
    [SerializeField] Boss boss;
    [SerializeField] Player player;
    [SerializeField] Projectile projectile;
    [SerializeField] Tutorial tutorial;

    [Header("Rhythm")]
    [SerializeField] float bpm;
    public float interval = 0;
    int lastInterval = 0;

    [Header("Tutorial")]
    float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorial)
        {
            if (player.health == 0)
            {
                menuText.text = "You Died";
                music.Stop();
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                isPaused = true;
                anim.SetBool("Paused", isPaused);
            }
            else if (boss.health == 0)
            {
                menuText.text = "You Win";
                music.Stop();
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                isPaused = true;
                anim.SetBool("Paused", isPaused);
            }
            else if (!music.isPlaying)
            {
                menuText.text = "You Lost";
                music.Stop();
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                isPaused = true;
                anim.SetBool("Paused", isPaused);
            }
        }
        

        interval = (music.timeSamples / (music.clip.frequency * (60 / (bpm * 2)))) - 0.027f;
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            if (tutorial)
            {
                if (tutorial.tutorialStage > 1)
                {
                    for (int i = 0; i < tutorial.GetChartLength(); i++)
                    {
                        if (tutorial.GetChart(i) == lastInterval)
                        {
                            fire.Play();
                            projectile.FireProjectile();
                            break;
                        }
                        else if (tutorial.GetChart(i) - 2 == lastInterval)
                        {
                            //charge.Play();
                            break;
                        }
                    }
                    switch (lastInterval)
                    {
                        case 64:
                            if (tutorial.tutorialStage == 3)
                            {
                                if (player.health < 120)
                                {
                                    player.health = 120;
                                    music.time = 0;
                                }
                                else
                                {
                                    timestamp = music.time;
                                    music.Stop();
                                    tutorial.StageComplete(timestamp);
                                }
                            }
                            break;
                        case 96:
                            if (tutorial.tutorialStage == 4)
                            {
                                if (player.health < 120)
                                {
                                    player.health = 120;
                                    music.time = timestamp;
                                }
                                else
                                {
                                    timestamp = music.time;
                                    music.Stop();
                                    tutorial.StageComplete(timestamp);
                                }
                            }
                            break;
                        case 128:
                            if (tutorial.tutorialStage == 6)
                            {
                                if (boss.health == 120)
                                {
                                    music.time = timestamp;
                                }
                                else
                                {
                                    timestamp = music.time;
                                    music.Stop();
                                    tutorial.StageComplete(timestamp);
                                }
                            }
                            break;
                    }
                }
            }
        }

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
