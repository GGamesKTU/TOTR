using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTrigger : MonoBehaviour
{
    [SerializeField]
    private string medicalPillTag = "MedicalPill";

    [SerializeField]
    private string ammoTag = "Ammo";

    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;

        if (IsMedicalPill(otherGameObject) || IsAmmo(otherGameObject))
        {
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
        }
    }

    private bool IsMedicalPill(GameObject obj)
    {
        return obj.CompareTag(medicalPillTag);
    }

    private bool IsAmmo(GameObject obj)
    {
        return obj.CompareTag(ammoTag);
    }

}
