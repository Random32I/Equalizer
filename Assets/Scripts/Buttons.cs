using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject normalMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayDemo()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void PlayCamp()
    {
        SceneManager.LoadScene("Camp");
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        SceneManager.LoadScene("Title");
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        normalMenu.SetActive(false);
    }

    public void Back()
    {
        settingsMenu.SetActive(false);
        normalMenu.SetActive(true);
    }
}
