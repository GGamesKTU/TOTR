using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heal : MonoBehaviour
{
    [SerializeField]
    private Text medsText;

    [SerializeField]
    private float meds = 5;

    [SerializeField]
    private float maxHeal = 25;

    [SerializeField]
    private float minHeal = 5;

    private HealthAndArmor Hp;

    private Backpack backpack;

    // Start is called before the first frame update
    void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        meds = backpack.medicine;
        Hp = GetComponent<HealthAndArmor>();
        medsText.text = $"{meds}";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && meds > 0)
        {
            if(Hp.GetHealth() != Hp.GetMaxHealth())
            {
                meds--;
                Hp.AddHealth(Random.Range(minHeal, maxHeal));
                medsText.text = $"{meds}";
            }
        }
        
    }

    public float GetMeds()
    {
        return meds;
    }
}
