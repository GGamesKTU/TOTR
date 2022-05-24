using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreviousLocation : MonoBehaviour
{
    private static PreviousLocation Instance;

    [SerializeField] 
    private GameObject other;

    [SerializeField]
    private int coliderIndex;

    private bool[] entranceArray = new bool[6];

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        entranceArray[0] = GameObject.FindGameObjectWithTag("House1").GetComponent<Collider>().enabled = true;
        entranceArray[1] = GameObject.FindGameObjectWithTag("House2").GetComponent<Collider>().enabled = true;
        entranceArray[2] = GameObject.FindGameObjectWithTag("House3").GetComponent<Collider>().enabled = true;
        entranceArray[3] = GameObject.FindGameObjectWithTag("House4").GetComponent<Collider>().enabled = true;
        entranceArray[4] = GameObject.FindGameObjectWithTag("GasStation").GetComponent<Collider>().enabled = true;
        entranceArray[5] = GameObject.FindGameObjectWithTag("Store").GetComponent<Collider>().enabled = true;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "MainMenuScene" || SceneManager.GetActiveScene().name == "HomeBase")
        {
            Destroy(gameObject);
        }
        else if(SceneManager.GetActiveScene().name == "demo_scene")
        {
            other = GameObject.FindGameObjectWithTag("Player");
            other.transform.position = gameObject.transform.position;

            if(entranceArray != null)
            {
                GameObject.FindGameObjectWithTag("House1").GetComponent<Collider>().enabled = entranceArray[0];
                GameObject.FindGameObjectWithTag("House2").GetComponent<Collider>().enabled = entranceArray[1];
                GameObject.FindGameObjectWithTag("House3").GetComponent<Collider>().enabled = entranceArray[2];
                GameObject.FindGameObjectWithTag("House4").GetComponent<Collider>().enabled = entranceArray[3];
                GameObject.FindGameObjectWithTag("GasStation").GetComponent<Collider>().enabled = entranceArray[4];
                GameObject.FindGameObjectWithTag("Store").GetComponent<Collider>().enabled = entranceArray[5];
            }
        }
        else
        {
            other = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void SavePosition(int index)
    {
        gameObject.transform.position = other.transform.position;
        entranceArray[index] = false;
        
    }
}
