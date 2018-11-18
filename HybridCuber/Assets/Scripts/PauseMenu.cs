using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;

    public GameObject gamePauseUI;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                Resume();
            } else
            {
                PauseGame();
            }
        }
	}
    public void Resume ()
    {
        gamePauseUI.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    private void PauseGame ()
    {
        gamePauseUI.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
