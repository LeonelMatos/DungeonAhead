using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public bool isGamePaused;
    Player player;
    Notification notification;

    void Start()
    {
        //TODO: Add command functions on runtime for the pause menu buttons
        //Em busca da código automática #2077

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //Resume button
        Button resumeButton = transform.GetChild(0).GetComponent<Button>();
        //Save button
        Button saveButton = transform.GetChild(1).GetComponent<Button>();
        //Load button
        Button loadButton = transform.GetChild(2).GetComponent<Button>();
        //Exit button
        Button exitButton = transform.GetChild(3).GetComponent<Button>();

        resumeButton.onClick.AddListener(delegate {PauseGame(); });
        saveButton.onClick.AddListener(delegate { player.SaveGame(); });
        loadButton.onClick.AddListener(delegate { player.LoadGame(); });
        exitButton.onClick.AddListener(delegate { ApplicationQuit(); });
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0f;
            
            GetComponent<Image>().enabled = true;
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        else{
            isGamePaused = false;
            Time.timeScale = 1f;

            GetComponent<Image>().enabled = false;
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void ApplicationQuit()
    {
        //TODO: Save first
        player.SaveGame();
        StartCoroutine(SaveAndQuit());
    }

    private IEnumerator SaveAndQuit()
    {
        yield return new WaitForSecondsRealtime(1); //Time until the game quits
        Debug.Log("Quitted game");
        Application.Exit();
        
    }

    public IEnumerator LoadTransitionStart()
    {
        Image transitionPanel = GameObject.FindGameObjectWithTag("TransitionPanel").GetComponent<Image>();

        transitionPanel.transform.SetAsLastSibling();
        float alpha = 0;
        for (int i = 0; i < 10; i++)
        {
            transitionPanel.color = new Color(0, 0, 0, alpha += 0.1f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }

    public IEnumerator LoadTransitionEnd()
    {
        Image transitionPanel = GameObject.FindGameObjectWithTag("TransitionPanel").GetComponent<Image>();
        float alpha = 1;
        transitionPanel.color = Color.black;
        for (int i = 0; i < 10; i++)
        {
            transitionPanel.color = new Color(0, 0, 0, alpha -= 0.1f);
            yield return new WaitForSecondsRealtime(0.02f);
        }
        transitionPanel.transform.SetAsFirstSibling();
    }

    
}
