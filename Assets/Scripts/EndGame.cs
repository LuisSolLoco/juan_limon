using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource exitAudio, caughtAudio;
    private bool hasAudioPlayed;
    private bool isPlayerAtExit,isPlayerCaught;
    private bool doRestart;
    private float timer;
    
    // Start is called before the first frame update
    public GameObject player;

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,exitAudio);
        }else if (isPlayerCaught)
        {
            doRestart = true;
          EndLevel(caughtBackgroundImageCanvasGroup,caughtAudio);
        }
    }

    void EndLevel(CanvasGroup imageCanvasGroup,AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }
        
        
        
        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;
        if (imageCanvasGroup.name== "Gameover Image Background")
        {
            
        }
if (timer > fadeDuration+displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();    
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
           // exitBackgroundImageCanvasGroup.alpha=1;
        }     
    }

    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
    
    /// <summary>
    /// finaliza el juego, desvanece el canvas group y cierra el juego
    /// </summary>
    void EndLevel()
    {
        Application.Quit();
    }

}
