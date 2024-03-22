using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Pausing")]
    [SerializeField] bool isPaused = false;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject EndMenu;
    [SerializeField] Animator anim;
    [SerializeField] Image results;
    [SerializeField] Image grade;
    [SerializeField] Sprite clearHit;
    [SerializeField] Sprite clearNoHit;
    [SerializeField] Sprite[] letters = new Sprite[6];

    [Header("Music")]
    [SerializeField] AudioSource music;
    private float musicPauseTime;
    [SerializeField] AudioSource charge;
    [SerializeField] AudioSource fire;

    [Header("UI")]
    [SerializeField] GameObject bossHealth;
    [SerializeField] GameObject bossHealthOutline;
    [SerializeField] RawImage playerHealth;
    [SerializeField] TextMeshProUGUI menuText;

    [Header("Scripts")]
    [SerializeField] Boss boss;
    [SerializeField] Player player;
    [SerializeField] Projectile projectile;
    [SerializeField] Tutorial tutorial;
    [SerializeField] Camp camp;

    [Header("Rhythm")]
    [SerializeField] float bpm;
    public float interval = 0;
    public int lastInterval = 0;

    [Header("Tutorial")]
    float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        Time.timeScale = 1;
        projectile.speed = (2.44f*bpm)/60;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tutorial)
        {
            if (boss.health < 0)
                boss.health = 0;

            if (player.health < 0)
                player.health = 0;

            if (player.health == 0)
            {
                music.Stop();
                Time.timeScale = 0;
                EndMenu.SetActive(true);
                isPaused = true;
                anim.SetBool("Paused", isPaused);
            }
            else if (boss.health == 0)
            {
                music.Stop();
                Time.timeScale = 0;
                EndMenu.SetActive(true);
                isPaused = true;
                anim.SetBool("Paused", isPaused);
                if (player.health == 176)
                {
                    grade.sprite = letters[0];
                    if (grade.transform.localScale.x == grade.transform.localScale.y)
                    {
                        grade.transform.localScale -= Vector3.right * 0.2f;
                    }
                    results.sprite = clearNoHit;
                }
                else
                {
                    switch (Mathf.Floor(player.health/44))
                    {
                        case 0:
                            grade.sprite = letters[4];
                            break;
                        case 1:
                            grade.sprite = letters[3];
                            break;
                        case 2:
                            grade.sprite = letters[2];
                            break;
                        case 3:
                            grade.sprite = letters[1];
                            break;
                    }
                    results.sprite = clearHit;
                }
            }
            else if (!music.isPlaying)
            {
                music.Stop();
                Time.timeScale = 0;
                EndMenu.SetActive(true);
                isPaused = true;
                anim.SetBool("Paused", isPaused);
            }
        }
        

        interval = (music.timeSamples / (music.clip.frequency * (60 / (bpm * 2)))) - 0.027f;
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            player.canAttack = true;
            if (lastInterval % 2 == 0)
            {
                player.canDash = true;
            }
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
            else
            {
                if (camp)
                {
                    if (camp.GetChart(0) == lastInterval)
                    {
                        fire.Play();
                        projectile.FireProjectile();
                        camp.RemoveChart();
                    }
                    else if (camp.GetChart(0) - 2 == lastInterval)
                    {
                        //charge.Play();
                    }
                }
            }
        }

        bossHealth.transform.localScale = new Vector3 (-(boss.health * 0.9370001f / (400)),
            bossHealth.transform.localScale.x, bossHealth.transform.localScale.z);

        playerHealth.rectTransform.offsetMax = new Vector2((player.health - 166), playerHealth.rectTransform.offsetMax.y);

        bossHealthOutline.transform.localRotation = Quaternion.LookRotation(GameObject.Find("Camera").transform.position); 

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
