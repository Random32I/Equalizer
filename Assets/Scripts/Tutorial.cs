using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] GameObject panel;

    [SerializeField] Player player;

    [SerializeField] AudioSource menuSelect;

    [SerializeField] AudioSource music;
    public int tutorialStage;
    readonly List<int> chart = new() {
    0, 4, 8, 12, 16, 20, 24, 28, 32, 36, 40, 44, 48, 52, 56, 60,
    64, 66, 70, 71, 73, 75, 76, 80, 81, 82, 84, 87, 89, 91, 92,
    96, 100, 104, 108, 112, 114, 120, 126
    };

    float time;

    void Start()
    {
        tutorialText.text = "Welcome to Equalizer!";
        player.canMove = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.canMove)
        {
            switch (tutorialStage)
            {
                case 0:
                    tutorialText.text = "To move around the lanes use the A and D keys, and to move between lanes use the W and S keys.";
                    tutorialStage++;
                    menuSelect.Play();
                    break;
                case 1:
                    tutorialText.text = "Try dodging this simple rhythm.";
                    tutorialStage++;
                    menuSelect.Play();
                    break;
                //Simple Dodge Pattern
                case 2:
                    music.Play();
                    tutorialStage++;
                    ToggleText(false);
                    menuSelect.Play();
                    break;
                //Complex Dodge Pattern
                case 3:
                    music.Play();
                    music.time = time;
                    tutorialStage++;
                    ToggleText(false);
                    menuSelect.Play();
                    break;
                //Attack Tutorial
                case 4:
                    tutorialText.text = "To attack just hit the left mouse button to the beat while in the front lane. Try it now!";
                    tutorialStage++;
                    menuSelect.Play();
                    break;
                case 5:
                    music.Play();
                    music.time = time;
                    tutorialStage++;
                    ToggleText(false);
                    menuSelect.Play();
                    break;
                //Insert Parrying here
                //End
                case 7:
                    tutorialText.text = "You'll get the hang of it the more you play!";
                    tutorialStage++;
                    menuSelect.Play();
                    break;
                case 8:
                    tutorialText.text = "This concludes our demo! Thanks for playing!";
                    tutorialStage++;
                    menuSelect.Play();
                    break;
                case 9:
                    SceneManager.LoadScene("Title");
                    break;
            }
        }
    }

    public void StageComplete(float timeStamp)
    {
        time = timeStamp;
        switch (tutorialStage)
        {
            case 3:
                chart.RemoveRange(0, 16);
                tutorialText.text = "Good! Now try this more complicated rhythm.";
                ToggleText(true);
                break;
            case 4:
                chart.RemoveRange(0, 15);
                tutorialText.text = "Next you need to learn how to attack.";
                ToggleText(true);
                break;
            case 6:
                chart.RemoveRange(0, 8);
                tutorialText.text = "Was that hard?";
                tutorialStage++;
                ToggleText(true);
                break;
        }
    }

    public int GetChart(int index)
    {
        return chart[index];
    }

    public int GetChartLength()
    {
        return chart.Count;
    }

    void ToggleText (bool state)
    {
        tutorialText.gameObject.SetActive(state);
        panel.SetActive(state);
        player.canMove = !state;
        Projectile[] projectiles = GameObject.FindObjectsOfType<Projectile>();
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (projectiles[i].name != "Temp Projectile")
            {
                Destroy(projectiles[i].gameObject);
            }
        }
    }
}
