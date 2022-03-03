using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pause_UI;

    bool is_paused = false;
    bool status_change = true;

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            is_paused = !is_paused;
            status_change = true;
        }

        if(status_change)
        {
            if(is_paused)
            {
                Time.timeScale = 0;
                pause_UI.SetActive(true);
                status_change = false;
            }
            else
            {
                Time.timeScale = 1;
                pause_UI.SetActive(false);
                status_change = false;
            }
        }
    }
    /// <summary>
    /// Change game state to unpaused
    /// </summary>
    public void resume_btn_click_fn()
    {
        is_paused = false;
        status_change = true;
    }
}
