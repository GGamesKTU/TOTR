using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitMap : MonoBehaviour
{
    public GameObject other;

    private Backpack backpack;

    void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody && other.CompareTag("Player"))
        {
            backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
            backpack.Return = true;
            backpack.SaveState();
            SceneManager.LoadScene(1);

        }
    }
}