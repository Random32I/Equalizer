using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public FMODUnity.EventReference clickSFX;
    FMOD.Studio.EventInstance click;


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
        click = FMODUnity.RuntimeManager.CreateInstance(clickSFX);
        click.start();
        Application.Quit();
    }

    public void PlayDemo()
    {
        click = FMODUnity.RuntimeManager.CreateInstance(clickSFX);
        click.start();
        SceneManager.LoadScene("Tutorial");
    }

    public void Retry()
    {
        click = FMODUnity.RuntimeManager.CreateInstance(clickSFX);
        click.start();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Title()
    {
        click = FMODUnity.RuntimeManager.CreateInstance(clickSFX);
        click.start();
        SceneManager.LoadScene("Title");
    }
}
