using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    public static Backpack Instance;

    public int scrapMetal = 0;
    public int scrapMax;

    public int wood = 0;
    public int woodMax;

    public int cloth = 0;
    public int clothMax;

    public float medicine = 0;
    public float medicineMax = 0;

    public float ammo = 0;
    public float ammoMax;
    public float mag;


    public int water = 0;
    public int waterMax;

    public int food = 0;
    public int foodMax;

    public float health = 100;
    public float armor = 15;
    public float stamina;

    public int BackpackUp;
    public int AccuracyUp;
    public int StrengthUp;
    public int PerceptionUp;
    public int SpeedUp;
    public int EnduranceUp;

    HealthAndArmor haa;
    Stamina stam;
    Shooting shot;
    Heal heal;

    public bool Return = false;

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
    }

    private void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene") GameObject.Destroy(gameObject);
        else if (SceneManager.GetActiveScene().name == "HomeBase" && Return)
        {
            Stats stat = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
            stat.Load();
            UnloadLoot();
            stat.SaveStats();
        }
        else if(SceneManager.GetActiveScene().name != "HomeBase" && !Return)
        {
            haa = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthAndArmor>();
            stam = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>();
            shot = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
            heal = GameObject.FindGameObjectWithTag("Player").GetComponent<Heal>();
            UpdateResourceText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "HomeBase")
        {
            health = haa.GetHealth();
            armor = haa.GetArmor();
            stamina = stam.GetStamina();
            ammo = shot.Ammo;
            mag = shot.Mag;
            medicine = heal.GetMeds();
        }
    }

    public void AddResources(int scrap, int wd, int cl, int fd, int wt)
    {
        if ((scrapMetal + scrap) <= scrapMax)
        {
            scrapMetal += scrap;
        }
        else scrapMetal = scrapMax;

        if ((wood + wd) <= woodMax)
        {
            wood += wd;
        }
        else wood = woodMax;

        if ((cloth + cl) <= clothMax)
        {
            cloth += cl;
        }
        else cloth = clothMax;

        if ((food + fd) <= foodMax)
        {
            food += fd;
        }
        else food = foodMax;

        if ((water + wt) <= waterMax)
        {
            water += wt;
        }
        else water = waterMax;

        UpdateResourceText();
    }

    public void TakeResources()
    {
        Stats stat = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        if(stat.Ammo >= 30)
        {
            mag = 30;
            stat.Ammo -= 30;
        }
        else 
        {
            mag = stat.Ammo;
            stat.Ammo = 0;
        }

        if (stat.Ammo >= ammoMax)
        {
            ammo = ammoMax;
            stat.Ammo -= ammoMax;
        }
        else
        {
            ammo = stat.Ammo;
            stat.Ammo = 0;
        }


        if (stat.Medicine >= medicineMax)
        {
            medicine = medicineMax;
            stat.Medicine -= medicineMax;
        }
        else
        {
            medicine = stat.Medicine;
            stat.Medicine = 0;
        }

        health = stat.Health;
        armor = stat.Armor;
        stamina = 100 + EnduranceUp * 10;
    }

    public void GetUpgrades()
    {
        Stats stat = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();

        BackpackUp = stat.BacpackUpgrade;
        AccuracyUp = stat.AccuracyUpgrade;
        StrengthUp = stat.StrengthUpgrade;
        PerceptionUp = stat.PerceptionUpgrade;
        SpeedUp = stat.SpeedUpgrade;
        EnduranceUp = stat.EnduranceUpgrade;

        
        scrapMax = 50 + BackpackUp * 10;
        woodMax = 50 + BackpackUp * 10;
        clothMax = 50 + BackpackUp * 10;
        waterMax = 50 + BackpackUp * 10;
        foodMax = 50 + BackpackUp * 10;

        ammoMax = 30 + BackpackUp * 30;
        medicineMax = 5 + BackpackUp *2;

        stamina = 100 + EnduranceUp * 10;
    }

    public void SaveState()
    {
        //HealthAndArmor haa = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthAndArmor>();
        //Stamina stam = GameObject.FindGameObjectWithTag("Player").GetComponent<Stamina>();
        //Shooting shot = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooting>();
        //Heal heal = GameObject.FindGameObjectWithTag("Player").GetComponent<Heal>();

        //health = haa.GetHealth();
        //armor = haa.GetArmor();
        //stamina = stam.GetStamina();
        //ammo = shot.Ammo;
        //mag = shot.Mag;
        //medicine = heal.GetMeds();
    }

    public void UnloadLoot()
    {
        Stats stat = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();

        if ((stat.Scrap + scrapMetal) <= stat.MaxScrap)
        {
            stat.Scrap += scrapMetal;
        }
        else stat.Scrap = stat.MaxScrap;

        if ((stat.Wood + wood) <= stat.MaxWood)
        {
            stat.Wood += wood;
        }
        else stat.Wood = stat.MaxWood;

        if ((stat.Cloth + cloth) <= stat.MaxCloth)
        {
            stat.Cloth += cloth;
        }
        else stat.Cloth = stat.MaxCloth;

        if ((stat.Food + food) <= stat.MaxFood)
        {
            stat.Food += food;
        }
        else stat.Food = stat.MaxFood;

        if ((stat.Water + water) <= stat.MaxWater)
        {
            stat.Water += water;
        }
        else stat.Water = stat.MaxWater;

        if ((stat.Medicine + medicine) <= stat.MaxMedicine)
        {
            stat.Medicine += medicine;
        }
        else stat.Medicine = stat.MaxMedicine;

        if ((stat.Ammo + mag + ammo) <= stat.MaxAmmo)
        {
            stat.Ammo = stat.Ammo + mag + ammo;
        }
        else stat.Ammo = stat.MaxAmmo;

        stat.Health = health;
        stat.Armor = armor;

        if ((stat.Thirst - 25) >= 0)
        {
            stat.Thirst = stat.Thirst - 25;
        }
        else stat.Thirst = 0;

        if ((stat.Hunger - 15) >= 0)
        {
            stat.Hunger -= 15;
        }
        else stat.Hunger = 0;

        stat.foodCollected = stat.foodCollected + stat.foodPerCycle * 3;
        stat.wateCollected = stat.wateCollected + stat.waterPerCycle * 5;

        scrapMetal = 0;
        wood = 0;
        cloth = 0;
        food = 0;
        water = 0;
        Return = false;

        stat.UpdateValues();
    }

    public void UpdateResourceText()
    {
        Text scrapText = GameObject.Find("/UI/Canvas/resources/slot1/have").GetComponent<Text>();
        Text maxScrapText = GameObject.Find("/UI/Canvas/resources/slot1/max").GetComponent<Text>();

        scrapText.text = $"{scrapMetal}";
        maxScrapText.text = $"{scrapMax}";

        Text woodText = GameObject.Find("/UI/Canvas/resources/slot2/have").GetComponent<Text>();
        Text maxWoodText = GameObject.Find("/UI/Canvas/resources/slot2/max").GetComponent<Text>();

        woodText.text = $"{wood}";
        maxWoodText.text = $"{woodMax}";

        Text clothText = GameObject.Find("/UI/Canvas/resources/slot3/have").GetComponent<Text>();
        Text maxClothText = GameObject.Find("/UI/Canvas/resources/slot3/max").GetComponent<Text>();

        clothText.text = $"{cloth}";
        maxClothText.text = $"{clothMax}";

        Text waterText = GameObject.Find("/UI/Canvas/resources/slot4/have").GetComponent<Text>();
        Text maxWaterText = GameObject.Find("/UI/Canvas/resources/slot4/max").GetComponent<Text>();

        waterText.text = $"{water}";
        maxWaterText.text = $"{waterMax}";

        Text foodText = GameObject.Find("/UI/Canvas/resources/slot5/have").GetComponent<Text>();
        Text maxFoodText = GameObject.Find("/UI/Canvas/resources/slot5/max").GetComponent<Text>();

        foodText.text = $"{food}";
        maxFoodText.text = $"{foodMax}";
    }

}
