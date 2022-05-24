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

    private GameObject other;
    //[SerializeField] private GameObject house1Spawn;
    //[SerializeField] private GameObject house2Spawn;
    //[SerializeField] private GameObject house3Spawn;
    //[SerializeField] private GameObject house4Spawn;
    //[SerializeField] private GameObject gasStationSpawn;
    //[SerializeField] private GameObject superMarketSpawn;

    private void Start()
    {
        other = GameObject.FindGameObjectWithTag("Player");
    }

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

        //if (SceneManager.GetActiveScene().name == "House1")
        //{
        //    SceneManager.LoadScene(scenename);
        //    other.transform.position = house1Spawn.transform.position;
        //}
        //else if (SceneManager.GetActiveScene().name == "House2")
        //{
        //    SceneManager.LoadScene(scenename);
        //    other.transform.position = house2Spawn.transform.position;
        //}
        //else if (SceneManager.GetActiveScene().name == "House3")
        //{
        //    SceneManager.LoadScene(scenename);
        //    other.transform.position = house3Spawn.transform.position;
        //}
        //else if (SceneManager.GetActiveScene().name == "House4")
        //{
        //    SceneManager.LoadScene(scenename);
        //    other.transform.position = house4Spawn.transform.position;
        //}
        //else if (SceneManager.GetActiveScene().name == "GasStation")
        //{
        //    SceneManager.LoadScene(scenename);
        //    other.transform.position = gasStationSpawn.transform.position;
        //}
        //else if (SceneManager.GetActiveScene().name == "SuperMarket")
        //{
        //    SceneManager.LoadScene(scenename);
        //    other.transform.position = superMarketSpawn.transform.position;
        //}

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
