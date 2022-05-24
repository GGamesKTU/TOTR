using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadColider : MonoBehaviour
{
    public GameObject other;
    public GameObject confirm;
    public GameObject spawnLocation;
    public string scene;

    bool isTriggered = false;
    bool wasTeleported = false;

    private Backpack backpack;

    [SerializeField]
    int entranceIndex;

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody && other.CompareTag("Player"))
        {
            confirm.SetActive(true);
            isTriggered = true;

            //spawnLocation.transform.position = other.transform.position;
            //DontDestroyOnLoad(spawnLocation);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody && other.CompareTag("Player"))
        {
            confirm.SetActive(false);
            isTriggered = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTriggered == true)
        {
            backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
            if (SceneManager.GetActiveScene().name == "demo_scene")
            {
               GameObject.FindGameObjectWithTag("StartPos").GetComponent<PreviousLocation>().SavePosition(entranceIndex);
            }           
            backpack.SaveState();

            SceneManager.LoadScene(scene);
            
        }
        //if (SceneManager.GetActiveScene().name == "demo_scene" && 
        //    spawnLocation.transform.position.z != 0 &&
        //    spawnLocation.transform.position != other.transform.position &&
        //    !wasTeleported)
        //{
        //    other.transform.position = spawnLocation.transform.position;
        //    wasTeleported = true;
            
        //}
    }
}
