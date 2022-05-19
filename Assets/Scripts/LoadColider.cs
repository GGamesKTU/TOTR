using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadColider : MonoBehaviour
{
    public GameObject other;
    public GameObject confirm;
    public string scene;
    bool isTriggered = false;

    private Backpack backpack;

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody && other.CompareTag("Player"))
        {
            confirm.SetActive(true);
            isTriggered = true;
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
            backpack.SaveState();
            SceneManager.LoadScene(scene);
        }
    }
}
