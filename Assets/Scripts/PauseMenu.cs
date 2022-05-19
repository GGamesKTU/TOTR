using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pause_UI;

    bool is_paused = false;
    bool status_change = true;

    public Texture2D Crosshair;

    static float offsetx = 0;
    static float offsety = 0;

    Vector2 offset = new Vector2(offsetx, offsety);
    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.sceneCount == 1)
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
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                status_change = false;
            }
            else
            {
                Time.timeScale = 1;
                pause_UI.SetActive(false);
                if (Crosshair != null)
                {
                    Cursor.SetCursor(Crosshair, new Vector2(Crosshair.width / 2, Crosshair.height / 2), CursorMode.ForceSoftware);
                }
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
