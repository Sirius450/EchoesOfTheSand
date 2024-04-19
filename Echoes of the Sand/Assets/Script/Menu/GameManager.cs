using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject UI;

    public static GameManager Singleton;    //creation of singleton

    //public static event Action OnPaused;

    private void Awake()        //creation of singleton
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPauseMenu();
            Debug.Log("pause menu");
            //OnPaused.Invoke();
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            pauseMenu.SetActive(false);
        }
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            UI.SetActive(true);
        }
    }

    public void OnPauseMenu(/*InputAction.CallbackContext context*/)
    {
        //if (context.started)
        //{
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {

            if (pauseMenu.active == false)
            {
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }
        //}
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
