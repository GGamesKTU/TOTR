using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private float stamina;

    [SerializeField]
    private float maxStamina = 100;

    [SerializeField]
    private Image staminaBar;

    private float calmTime = 0;

    private float runTime = 0;

    private PlayerController pc;

    private float fillBarRatio;

    private Backpack backpack;

    // Start is called before the first frame update
    void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        pc = GetComponent<PlayerController>();

        stamina = backpack.stamina;
        maxStamina = 100 + backpack.EnduranceUp * 10;

        fillBarRatio = 1 / maxStamina;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        calmTime += Time.deltaTime;

        if (pc.GetIsRunning())
        {
            runTime += Time.deltaTime;
        }
        else runTime = 0;

        if(runTime > 1)
        {
            UseStamina(5);
            runTime = 0;  
        }

        if (calmTime >= 5 && stamina != maxStamina)
        {
            StaminaRegen();
            calmTime = 0;
        }
    }

    private void StaminaRegen()
    {
        if ((stamina + 10) >= maxStamina) stamina = maxStamina;
        else stamina += 10;

        staminaBar.fillAmount = stamina * fillBarRatio;
    }

    public void UseStamina(float used)
    {
        if ((stamina - used) <= 0) stamina = 0;
        else stamina -= used;

        calmTime = 0;
        staminaBar.fillAmount = stamina * fillBarRatio;
    }

    public float GetStamina()
    {
        return stamina;
    }
}
