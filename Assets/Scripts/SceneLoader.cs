using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SceneLoader : MonoBehaviour
{
    private Backpack backpack;
    private Stats stat;

    public void NewGame()
    {
        string path = Application.persistentDataPath + "/stats.data";
        if(File.Exists(path))
        {
            File.Delete(path);
        }

        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/stats.data";
        if (File.Exists(path))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadScene(string scenename)
    {
        if (SceneManager.GetActiveScene().name == "HomeBase")
        {
            backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
            backpack.GetUpgrades();
            backpack.TakeResources();
            stat.SaveStats();
        }

        SceneManager.LoadScene(scenename);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    static public float offsetx = 0;
    static public float offsety = 0;

    Vector2 mouseoffset = new Vector2( offsetx,offsety);

    //Loads level scene
    public void LoadLevel(string scenename)
    {
        if (SceneManager.GetActiveScene().name == "HomeBase")
        {
            backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
            stat = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
            backpack.GetUpgrades();
            backpack.TakeResources();
            stat.SaveStats();
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);

        loadingScreen.SetActive(true);

        while(operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100f + "%";
        }
        //Cursor.SetCursor(Crosshair,Vector2.zero,CursorMode.ForceSoftware);
    }
    public void DeleteSave()
    {
        string path = Application.persistentDataPath + "/stats.data";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }


    //Load options menu
    public void LoadOptions()
    {
        SceneManager.LoadScene("OptionsScene",LoadSceneMode.Additive);
    }
    //Unload Scene
    public void BackToScene()
    {
        SceneManager.UnloadSceneAsync(3);
    }
    //Quits the game 
    public void QuitGame()
    {
        Application.Quit();
    }
}
