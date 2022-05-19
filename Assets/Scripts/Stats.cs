using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Stats : MonoBehaviour
{
    [Header("Starting stats")]
    public int Scrap = 0;
    public int MaxScrap = 100;

    public int Wood = 0;
    public int MaxWood = 100;

    public int Cloth = 0;
    public int MaxCloth = 100;

    public float Medicine = 0;
    public float MaxMedicine = 10;

    public float Ammo = 0;
    public float MaxAmmo = 100;

    public int Water = 0;
    public int MaxWater = 100;

    public int Food = 0;
    public int MaxFood = 100;

    public int Hunger = 100;
    public int Thirst = 100;

    public float Health = 100;
    public float Armor = 15;

    public int waterPerCycle = 0;
    public int wateCollected = 0;

    public int foodPerCycle = 0;
    public int foodCollected = 0;

    [Header("Starting Char Upgrades")]
    public int BacpackUpgrade = 0;
    public int AccuracyUpgrade = 0;
    public int StrengthUpgrade = 0;
    public int PerceptionUpgrade = 0;
    public int SpeedUpgrade = 0;
    public int EnduranceUpgrade = 0;

    [Header("Starting Base Upgrades")]
    public int foodStorageUpgrade = 0;
    public int resourceStorageUpgrade = 0;
    public int waterCollectorUpgrade = 0;
    public int farmUpgrade = 0;


    ToolTipTrigger waterMadeText;
    ToolTipTrigger foodMadeText;
    public void SaveStats()
    {
        SaveSystem.Save(this);
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/stats.data";
        if (File.Exists(path))
        {
            StatsData data = SaveSystem.Load();

            if (data != null)
            {
                Scrap = data.Scrap;
                MaxScrap = data.MaxScrap;

                Wood = data.Wood;
                MaxWood = data.MaxWood;

                Cloth = data.Cloth;
                MaxCloth = data.MaxCloth;

                Medicine = data.Medicine;
                MaxMedicine = data.MaxMedicine;

                Ammo = data.Ammo;
                MaxAmmo = data.MaxAmmo;

                Water = data.Water;
                MaxWater = data.MaxWater;

                Food = data.Food;
                MaxFood = data.MaxFood;

                Hunger = data.Hunger;
                Thirst = data.Thirst;

                Health = data.Health;
                Armor = data.Armor;

                waterPerCycle = data.waterPerCycle;
                wateCollected = data.wateCollected;

                foodPerCycle = data.foodPerCycle;
                foodCollected = data.foodCollected;

                BacpackUpgrade = data.BacpackUpgrade;
                AccuracyUpgrade = data.AccuracyUpgrade;
                StrengthUpgrade = data.StrengthUpgrade;
                PerceptionUpgrade = data.PerceptionUpgrade;
                SpeedUpgrade = data.SpeedUpgrade;
                EnduranceUpgrade = data.EnduranceUpgrade;


                foodStorageUpgrade = data.foodStorageUpgrade;
                resourceStorageUpgrade = data.resourceStorageUpgrade;
                waterCollectorUpgrade = data.waterCollectorUpgrade;
                farmUpgrade = data.farmUpgrade;
            }
        }
    }
    private void Awake()
    {
        waterMadeText = GameObject.Find("/Home/BUTTONS/CollectWater").GetComponent<ToolTipTrigger>();
        foodMadeText = GameObject.Find("/Home/BUTTONS/CollectFood").GetComponent<ToolTipTrigger>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
        UpdateValues();
    }

    //------------------------------------------------------------------------------
    // HOME BUTTONS
    //------------------------------------------------------------------------------
    public void Eat()
    {
        if( Hunger < 100)
        {
            if (Food > 10)
            {
                Food -= 10;
                if ((Hunger + 15) < 100)
                {
                    Hunger += 15;
                }
                else Hunger = 100;

                UpdateValues();
            }
        }
    }

    public void Drink()
    {
        if(Thirst < 100)
        {
            if (Water >= 10)
            {
                Water -= 10;
                if ((Thirst + 15) < 100)
                {
                    Thirst += 15;
                }
                else Thirst = 100;

                UpdateValues();
            }
        }
    }

    public void Rest()
    {
        int restAmount = Hunger / 10 + Thirst / 10;
        if (restAmount > 0)
        {
            GameObject.Find("/Home/BUTTONS/Rest").GetComponent<Button>().enabled = false;
        }

        if ((Health + restAmount) < 100)
        {
            Health += restAmount;
        }
        else Health = 100;

        if ((Hunger - 10) < 0)
        {
            Hunger = 0;
        }
        else Hunger -= 10;

        if ((Thirst - 15) < 0)
        {
            Thirst = 0;
        }
        else Thirst -= 15;

        UpdateValues();
    }

    public void Repair()
    {
        if (Scrap >= 25 && Cloth >= 10)
        {
            Scrap -= 25;
            Cloth -= 10;

            if ((Armor + 15) < 100)
            {
                Armor += 15;
            }
            else Armor = 100;

            UpdateValues();
        }
    }

    public void CollectWater()
    {
        if ((Water + wateCollected) < MaxWater)
        {
            Water += wateCollected;
        }
        else Water = MaxWater;

        wateCollected = 0;
        UpdateValues();
    }

    public void CollectFood()
    {
        if ((Food + foodCollected) < MaxFood)
        {
            Food += foodCollected;
        }
        else Food = MaxFood;

        foodCollected = 0;
        UpdateValues();
    }

    public void MakeAmmo()
    {
        if (Scrap >= 15)
        {
            Scrap -= 15;
            if ((Ammo + 10) < MaxAmmo)
            {
                Ammo += 10;
            }
            else Ammo = MaxAmmo;

            UpdateValues();
        }
    }

    public void MakeMedicine()
    {
        if (Cloth >= 15 && Food >= 20)
        {
            Cloth -= 15;
            Food -= 10;
            if ((Medicine + 1) < MaxMedicine)
            {
                Medicine += 1;
            }
        }
        UpdateValues();
    }
    //------------------------------------------------------------------------------
    // UPGRADE BUTTONS
    //------------------------------------------------------------------------------

    public void UpgradeBackpack()
    {
        if (BacpackUpgrade < 5)
        {
            int[] Cost = BackpackUpCost();
            if (Cost[0] <= Scrap && Cost[1] <= Wood && Cost[2] <= Cloth)
            {
                BacpackUpgrade++;
                Scrap -= Cost[0];
                Wood -= Cost[1];
                Cloth -= Cost[2];
            }
        }
        UpdateCostBackpack();
    }

    public void UpgradeAccuracy()
    {
        if (AccuracyUpgrade < 5)
        {
            int[] Cost = AccuracyUpCost();
            if (Cost[0] <= Scrap && Cost[1] <= Wood && Cost[2] <= Cloth)
            {
                AccuracyUpgrade++;
                Scrap -= Cost[0];
                Wood -= Cost[1];
                Cloth -= Cost[2];
            }
        }
        UpdateCostAccuracy();
    }

    public void UpgradeStrength()
    {
        if (StrengthUpgrade < 5)
        {
            int[] Cost = StrengthUpCost();
            if (Cost[0] <= Food && Cost[1] <= Water)
            {
                StrengthUpgrade++;
                Food -= Cost[0];
                Water -= Cost[1];
            }
        }
        UpdateCostStrength();
    }

    public void UpgradePerception()
    {
        if (PerceptionUpgrade < 5)
        {
            int[] Cost = PerceptionUpCost();
            if (Cost[0] <= Food && Cost[1] <= Water)
            {
                PerceptionUpgrade++;
                Food -= Cost[0];
                Water -= Cost[1];
            }
        }
        UpdateCostPerception();
    }

    public void UpgradeSpeed()
    {
        if (SpeedUpgrade < 5)
        {
            int[] Cost = SpeedUpCost();
            if (Cost[0] <= Food && Cost[1] <= Water)
            {
                SpeedUpgrade++;
                Food -= Cost[0];
                Water -= Cost[1];
            }
        }
        UpdateCostSpeed();
    }

    public void UpgradeEndurance()
    {
        if (EnduranceUpgrade < 5)
        {
            int[] Cost = EnduranceUpCost();
            if (Cost[0] <= Food && Cost[1] <= Water)
            {
                EnduranceUpgrade++;
                Food -= Cost[0];
                Water -= Cost[1];
            }
        }
        UpdateCostEndurance();
    }

    //------------------------------------------------------------------------------
    public void UpgradeFoodStorage()
    {
        if (foodStorageUpgrade < 5)
        {
            int[] Cost = FoodStorageUpCost();
            if (Cost[0] <= Scrap && Cost[1] <= Wood && Cost[2] <= Cloth)
            {
                foodStorageUpgrade++;
                Scrap -= Cost[0];
                Wood -= Cost[1];
                Cloth -= Cost[2];

                int[] Values = FoodStorageUpValue();
                MaxWater = Values[0];
                MaxFood = Values[1];

                Text FoodNoofUpgrades = GameObject.Find("/Upgrades/BASE/VALUES/Expandfood/NumberofUpgrades").GetComponent<Text>();
                Text MaxWaterText = GameObject.Find("/Home/VALUES/Water/Max").GetComponent<Text>();
                Text MaxFoodText = GameObject.Find("/Home/VALUES/Food/Max").GetComponent<Text>();

                MaxWaterText.text = $"{(int)MaxWater}";
                MaxFoodText.text = $"{(int)MaxFood}";
                FoodNoofUpgrades.text = $"{(int)foodStorageUpgrade}";
                UpdateCostFoodStorage();
            }
        }
    }

    public void UpgradeResourceStorage()
    {
        if (resourceStorageUpgrade < 5)
        {
            int[] Cost = ResourceStorageUpCost();
            if (Cost[0] <= Scrap && Cost[1] <= Wood && Cost[2] <= Cloth)
            {
                resourceStorageUpgrade++;
                Scrap -= Cost[0];
                Wood -= Cost[1];
                Cloth -= Cost[2];

                int[] Values = ResourceStorageUpValue();
                MaxScrap = Values[0];
                MaxWood = Values[1];
                MaxCloth = Values[2];
                MaxMedicine = Values[3];
                MaxAmmo = Values[4];

                Text ResourcesNoofUpgrades = GameObject.Find("/Upgrades/BASE/VALUES/Expandresources/NumberofUpgrades").GetComponent<Text>();
                Text MaxScrapText = GameObject.Find("/Home/VALUES/ScrapMetal/Max").GetComponent<Text>();
                Text MaxWoodText = GameObject.Find("/Home/VALUES/Wood/Max").GetComponent<Text>();
                Text MaxClothText = GameObject.Find("/Home/VALUES/Cloth/Max").GetComponent<Text>();
                Text MaxAmmoText = GameObject.Find("/Home/VALUES/Ammo/Max").GetComponent<Text>();
                Text MaxMedicineText = GameObject.Find("/Home/VALUES/Meds/Max").GetComponent<Text>();

                MaxScrapText.text = $"{(int)MaxScrap}";
                MaxWoodText.text = $"{(int)MaxWood}";
                MaxClothText.text = $"{(int)MaxCloth}";
                MaxAmmoText.text = $"{(int)MaxAmmo}";
                MaxMedicineText.text = $"{(int)MaxMedicine}";
                ResourcesNoofUpgrades.text = $"{(int)resourceStorageUpgrade}";
                UpdateCostResourceStorage();
            }
        }
    }

    public void UpgradeWaterCollector()
    {
        if (waterCollectorUpgrade < 5)
        {
            int[] Cost = WaterCollectorUpCost();
            if (Cost[0] <= Scrap && Cost[1] <= Wood && Cost[2] <= Cloth)
            {
                waterCollectorUpgrade++;
                Scrap -= Cost[0];
                Wood -= Cost[1];
                Cloth -= Cost[2];
                waterPerCycle = WaterCollectorUpValue();

                Text WaterCollectorNoofUpgrades = GameObject.Find("/Upgrades/BASE/VALUES/Improvewater/NumberofUpgrades").GetComponent<Text>();
                Text waterPerCycleText = GameObject.Find("/Home/VALUES/WaterMade").GetComponent<Text>();

                waterPerCycleText.text = $"{(int)waterPerCycle}";
                WaterCollectorNoofUpgrades.text = $"{(int)waterCollectorUpgrade}";

                UpdateCostWaterCollector();
            }
        }
    }

    public void UpgradeFarm()
    {
        if (farmUpgrade < 5)
        {
            int[] Cost = FarmUpCost();
            if (Cost[0] <= Scrap && Cost[1] <= Wood && Cost[2] <= Cloth)
            {
                farmUpgrade++;
                Scrap -= Cost[0];
                Wood -= Cost[1];
                Cloth -= Cost[2];
                foodPerCycle = FarmUpValue();

                Text FarmNoofUpgrades = GameObject.Find("/Upgrades/BASE/VALUES/Improvefarm/NumberofUpgrades").GetComponent<Text>();
                Text foodPerCycleText = GameObject.Find("/Home/VALUES/FarmMade").GetComponent<Text>();

                foodPerCycleText.text = $"{(int)foodPerCycle}";
                FarmNoofUpgrades.text = $"{(int)farmUpgrade}";

                UpdateCostFarm();
            }
        }
    }
    //------------------------------------------------------------------------------
    // UPDATE COSTS
    //------------------------------------------------------------------------------
    private void UpdateCostFoodStorage()
    {
        Text FoodStorageMetalCost = GameObject.Find("/Upgrades/BASE/VALUES/Expandfood/MetalValue").GetComponent<Text>();
        Text FoodStorageClothCost = GameObject.Find("/Upgrades/BASE/VALUES/Expandfood/ClothValue").GetComponent<Text>();
        Text FoodStorageWoodCost = GameObject.Find("/Upgrades/BASE/VALUES/Expandfood/WoodValue").GetComponent<Text>();

        if (foodStorageUpgrade < 5)
        {
            int[] Cost = FoodStorageUpCost();
            FoodStorageMetalCost.text = $"{(int)Cost[0]}";
            FoodStorageClothCost.text = $"{(int)Cost[1]}";
            FoodStorageWoodCost.text = $"{(int)Cost[2]}";
        }
        if (foodStorageUpgrade == 5)
        {
            FoodStorageMetalCost.text = "MAX";
            FoodStorageClothCost.text = "MAX";
            FoodStorageWoodCost.text = "MAX";
        }
    }

    private void UpdateCostResourceStorage()
    {
        Text ResourceStorageScrapCost = GameObject.Find("/Upgrades/BASE/VALUES/Expandresources/MetalValue").GetComponent<Text>();
        Text ResourceStorageClothCost = GameObject.Find("/Upgrades/BASE/VALUES/Expandresources/ClothValue").GetComponent<Text>();
        Text ResourceStorageWoodCost = GameObject.Find("/Upgrades/BASE/VALUES/Expandresources/WoodValue").GetComponent<Text>();

        if (resourceStorageUpgrade < 5)
        {
            int[] Cost = ResourceStorageUpCost();
            ResourceStorageScrapCost.text = $"{(int)Cost[0]}";
            ResourceStorageClothCost.text = $"{(int)Cost[1]}";
            ResourceStorageWoodCost.text = $"{(int)Cost[2]}";
        }
        if (resourceStorageUpgrade == 5)
        {
            ResourceStorageScrapCost.text = "MAX";
            ResourceStorageClothCost.text = "MAX";
            ResourceStorageWoodCost.text = "MAX";
        }
    }

    private void UpdateCostWaterCollector()
    {
        Text WaterCollectorScrapCost = GameObject.Find("/Upgrades/BASE/VALUES/Improvewater/MetalValue").GetComponent<Text>();
        Text WaterCollectorClothCost = GameObject.Find("/Upgrades/BASE/VALUES/Improvewater/ClothValue").GetComponent<Text>();
        Text WaterCollectorWoodCost = GameObject.Find("/Upgrades/BASE/VALUES/Improvewater/WoodValue").GetComponent<Text>();

        if (waterCollectorUpgrade < 5)
        {
            int[] Cost = WaterCollectorUpCost();
            WaterCollectorScrapCost.text = $"{(int)Cost[0]}";
            WaterCollectorClothCost.text = $"{(int)Cost[1]}";
            WaterCollectorWoodCost.text = $"{(int)Cost[2]}";
        }
        if (waterCollectorUpgrade == 5)
        {
            WaterCollectorScrapCost.text = "MAX";
            WaterCollectorClothCost.text = "MAX";
            WaterCollectorWoodCost.text = "MAX";

        }
    }

    private void UpdateCostFarm()
    {
        Text FarmScrapCost = GameObject.Find("/Upgrades/BASE/VALUES/Improvefarm/MetalValue").GetComponent<Text>();
        Text FarmClothCost = GameObject.Find("/Upgrades/BASE/VALUES/Improvefarm/ClothValue").GetComponent<Text>();
        Text FarmWoodCost = GameObject.Find("/Upgrades/BASE/VALUES/Improvefarm/WoodValue").GetComponent<Text>();

        if (farmUpgrade < 5)
        {
            int[] Cost = FarmUpCost();
            FarmScrapCost.text = $"{(int)Cost[0]}";
            FarmClothCost.text = $"{(int)Cost[1]}";
            FarmWoodCost.text = $"{(int)Cost[2]}";
        }
        if (farmUpgrade == 5)
        {
            FarmScrapCost.text = "MAX";
            FarmClothCost.text = "MAX";
            FarmWoodCost.text = "MAX";
        }
    }

    //------------------------------------------------------------------------------
    private void UpdateCostBackpack()
    {
        Text BackpackScrapCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Backpack/MetalValue").GetComponent<Text>();
        Text BackpackClothCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Backpack/ClothValue").GetComponent<Text>();
        Text BackpackWoodCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Backpack/WoodValue").GetComponent<Text>();
        Text BackpackNoofUpgrades = GameObject.Find("/Upgrades/CHARACTER/VALUES/Backpack/NumberofUpgrades").GetComponent<Text>();
        if (BacpackUpgrade == 5)
        {
            BackpackScrapCost.text = "MAX";
            BackpackClothCost.text = "MAX";
            BackpackWoodCost.text = "MAX";
            BackpackNoofUpgrades.text = "5";
        }
        else
        {
            int[] Cost = BackpackUpCost();
            BackpackScrapCost.text = $"{(int)Cost[0]}";
            BackpackClothCost.text = $"{(int)Cost[1]}";
            BackpackWoodCost.text = $"{(int)Cost[2]}";
            BackpackNoofUpgrades.text = $"{(int)BacpackUpgrade}";
        }
    }
    private void UpdateCostAccuracy()
    {
        Text AccuracyScrapCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Accuracy/MetalValue").GetComponent<Text>();
        Text AccuracyClothCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Accuracy/ClothValue").GetComponent<Text>();
        Text AccuracyWoodCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Accuracy/WoodValue").GetComponent<Text>();
        Text AccuracyNoofUpgrades = GameObject.Find("/Upgrades/CHARACTER/VALUES/Accuracy/NumberofUpgrades").GetComponent<Text>();

        if (AccuracyUpgrade == 5)
        {
            AccuracyScrapCost.text = "MAX";
            AccuracyClothCost.text = "MAX";
            AccuracyWoodCost.text = "MAX";
            AccuracyNoofUpgrades.text = "5";
        }
        else
        {
            int[] Cost = AccuracyUpCost();
            AccuracyScrapCost.text = $"{(int)Cost[0]}";
            AccuracyClothCost.text = $"{(int)Cost[1]}";
            AccuracyWoodCost.text = $"{(int)Cost[2]}";
            AccuracyNoofUpgrades.text = $"{(int)AccuracyUpgrade}";
        }
    }
    private void UpdateCostStrength()
    {
        Text StrengthFoodCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Strength/FoodValue").GetComponent<Text>();
        Text StrengthWaterCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Strength/WaterValue").GetComponent<Text>();
        Text StrengthNoofUpgrades = GameObject.Find("/Upgrades/CHARACTER/VALUES/Strength/NumberofUpgrades").GetComponent<Text>();

        if (StrengthUpgrade == 5)
        {
            StrengthFoodCost.text = "MAX";
            StrengthWaterCost.text = "MAX";
            StrengthNoofUpgrades.text = "5";
        }
        else
        {
            int[] Cost = StrengthUpCost();
            StrengthFoodCost.text = $"{(int)Cost[0]}";
            StrengthWaterCost.text = $"{(int)Cost[1]}";
            StrengthNoofUpgrades.text = $"{(int)StrengthUpgrade}";
        }
    }
    private void UpdateCostPerception()
    {
        Text PerceptionFoodCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Perception/FoodValue").GetComponent<Text>();
        Text PerceptionWaterCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Perception/WaterValue").GetComponent<Text>();
        Text PerceptionNoofUpgrades = GameObject.Find("/Upgrades/CHARACTER/VALUES/Perception/NumberofUpgrades").GetComponent<Text>();
        if (PerceptionUpgrade == 5)
        {
            PerceptionFoodCost.text = "MAX";
            PerceptionWaterCost.text = "MAX";
            PerceptionNoofUpgrades.text = "5";
        }
        else
        {
            int[] Cost = PerceptionUpCost();
            PerceptionFoodCost.text = $"{(int)Cost[0]}";
            PerceptionWaterCost.text = $"{(int)Cost[1]}";
            PerceptionNoofUpgrades.text = $"{(int)PerceptionUpgrade}";
        }
    }
    private void UpdateCostSpeed()
    {
        Text SpeedFoodCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Speed/FoodValue").GetComponent<Text>();
        Text SpeedWaterCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Speed/WaterValue").GetComponent<Text>();
        Text SpeedNoofUpgrades = GameObject.Find("/Upgrades/CHARACTER/VALUES/Speed/NumberofUpgrades").GetComponent<Text>();
        if (SpeedUpgrade == 5)
        {
            SpeedFoodCost.text = "MAX";
            SpeedWaterCost.text = "MAX";
            SpeedNoofUpgrades.text = "5";
        }
        else
        {
            int[] Cost = SpeedUpCost();
            SpeedFoodCost.text = $"{(int)Cost[0]}";
            SpeedWaterCost.text = $"{(int)Cost[1]}";
            SpeedNoofUpgrades.text = $"{(int)SpeedUpgrade}";
        }
    }
    private void UpdateCostEndurance()
    {
        Text EnduranceFoodCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Endurance/FoodValue").GetComponent<Text>();
        Text EnduranceWaterCost = GameObject.Find("/Upgrades/CHARACTER/VALUES/Endurance/WaterValue").GetComponent<Text>();
        Text EnduranceNoofUpgrades = GameObject.Find("/Upgrades/CHARACTER/VALUES/Endurance/NumberofUpgrades").GetComponent<Text>();
        if (EnduranceUpgrade == 5)
        {
            EnduranceFoodCost.text = "MAX";
            EnduranceWaterCost.text = "MAX";
            EnduranceNoofUpgrades.text = "5";
        }
        else
        {
            int[] Cost = EnduranceUpCost();
            EnduranceFoodCost.text = $"{(int)Cost[0]}";
            EnduranceWaterCost.text = $"{(int)Cost[1]}";
            EnduranceNoofUpgrades.text = $"{(int)EnduranceUpgrade}";
        }
    }
    //------------------------------------------------------------------------------
    // UPDATE TEXTS
    //------------------------------------------------------------------------------
    public void UpdateValues()
    {
        Text ScrapText = GameObject.Find("/Home/VALUES/ScrapMetal/Stock").GetComponent<Text>();
        ScrapText.text = $"{Scrap}";

        Text WoodText = GameObject.Find("/Home/VALUES/Wood/Stock").GetComponent<Text>();
        WoodText.text = $"{Wood}";

        Text ClothText = GameObject.Find("/Home/VALUES/Cloth/Stock").GetComponent<Text>();
        ClothText.text = $"{Cloth}";

        Text MedicineText = GameObject.Find("/Home/VALUES/Meds/Stock").GetComponent<Text>();
        MedicineText.text = $"{Medicine}";

        Text AmmoText = GameObject.Find("/Home/VALUES/Ammo/Stock").GetComponent<Text>();
        AmmoText.text = $"{Ammo}";

        Text WaterText = GameObject.Find("/Home/VALUES/Water/Stock").GetComponent<Text>();
        WaterText.text = $"{Water}";

        Text FoodText = GameObject.Find("/Home/VALUES/Food/Stock").GetComponent<Text>();
        FoodText.text = $"{Food}";

        Text HungerText = GameObject.Find("/Home/VALUES/Hunger").GetComponent<Text>();
        HungerText.text = $"{Hunger}";

        Text ThirstText = GameObject.Find("/Home/VALUES/Thirst").GetComponent<Text>();
        ThirstText.text = $"{Thirst}";

        Text HealthText = GameObject.Find("/Home/VALUES/Health").GetComponent<Text>();
        HealthText.text = $"{Health}";

        Text ArmorText = GameObject.Find("/Home/VALUES/Armor").GetComponent<Text>();
        ArmorText.text = $"{Armor}";

        waterMadeText.tooltext = $"Water collected: {wateCollected}";

        foodMadeText.tooltext = $"Food produced: {foodCollected}";
    }

    private void ApplyUpgrades()
    {

        Text ResourcesNoofUpgrades = GameObject.Find("/Upgrades/BASE/VALUES/Expandresources/NumberofUpgrades").GetComponent<Text>();
        Text MaxScrapText = GameObject.Find("/Home/VALUES/ScrapMetal/Max").GetComponent<Text>();
        Text MaxWoodText = GameObject.Find("/Home/VALUES/Wood/Max").GetComponent<Text>();
        Text MaxClothText = GameObject.Find("/Home/VALUES/Cloth/Max").GetComponent<Text>();
        Text MaxAmmoText = GameObject.Find("/Home/VALUES/Ammo/Max").GetComponent<Text>();
        Text MaxMedicineText = GameObject.Find("/Home/VALUES/Meds/Max").GetComponent<Text>();

        Text MaxWaterText = GameObject.Find("/Home/VALUES/Water/Max").GetComponent<Text>();
        Text MaxFoodText = GameObject.Find("/Home/VALUES/Food/Max").GetComponent<Text>();
        ToolTipTrigger waterPerCycleText = GameObject.Find("/Home/BUTTONS/CollectWater").GetComponent<ToolTipTrigger>();
        ToolTipTrigger foodPerCycleText = GameObject.Find("/Home/BUTTONS/CollectFood").GetComponent<ToolTipTrigger>();
        Text foodProducedText = GameObject.Find("/Home/VALUES/FarmMade").GetComponent<Text>();
        Text waterProducedText = GameObject.Find("/Home/VALUES/WaterMade").GetComponent<Text>();

        //Text foodStorageUpText;
        //
    }

    public void UpdateCosts()
    {
        UpdateCostFoodStorage();
        UpdateCostResourceStorage();
        UpdateCostWaterCollector();
        UpdateCostFarm();
    }

    public void UpdateCostsChar()
    {
        UpdateCostBackpack();
        UpdateCostAccuracy();
        UpdateCostStrength();
        UpdateCostPerception();
        UpdateCostSpeed();
        UpdateCostEndurance();
    }

    //------------------------------------------------------------------------------
    // UPGRADE COSTS
    //------------------------------------------------------------------------------
    private int[] FoodStorageUpCost()
    {
        int metalCost = 0;
        int clothCost = 0;
        int woodCost = 0;

        switch (foodStorageUpgrade)
        {
            case 0:
                metalCost = 30;
                clothCost = 10;
                woodCost = 50;
                break;
            case 1:
                metalCost = 110;
                clothCost = 90;
                woodCost = 160;
                break;
            case 2:
                metalCost = 180;
                clothCost = 140;
                woodCost = 220;
                break;
            case 3:
                metalCost = 240;
                clothCost = 200;
                woodCost = 280;
                break;
            case 4:
                metalCost = 310;
                clothCost = 240;
                woodCost = 350;
                break;
            default:
                metalCost = -1;
                clothCost = -1;
                woodCost = -1;
                break;
        }

        int[] Cost = { metalCost, clothCost, woodCost };
        return Cost;
    }

    private int[] ResourceStorageUpCost()
    {
        int metalCost = 0;
        int clothCost = 0;
        int woodCost = 0;

        switch (resourceStorageUpgrade)
        {
            case 0:
                metalCost = 50;
                clothCost = 10;
                woodCost = 30;
                break;
            case 1:
                metalCost = 160;
                clothCost = 90;
                woodCost = 110;
                break;
            case 2:
                metalCost = 220;
                clothCost = 140;
                woodCost = 180;
                break;
            case 3:
                metalCost = 280;
                clothCost = 200;
                woodCost = 240;
                break;
            case 4:
                metalCost = 350;
                clothCost = 240;
                woodCost = 310;
                break;
            default:
                metalCost = -1;
                clothCost = -1;
                woodCost = -1;
                break;
        }

        int[] Cost = { metalCost, clothCost, woodCost };
        return Cost;
    }

    private int[] WaterCollectorUpCost()
    {
        int metalCost = 0;
        int clothCost = 0;
        int woodCost = 0;

        switch (waterCollectorUpgrade)
        {
            case 0:
                metalCost = 50;
                clothCost = 30;
                woodCost = 10;
                break;
            case 1:
                metalCost = 160;
                clothCost = 110;
                woodCost = 90;
                break;
            case 2:
                metalCost = 220;
                clothCost = 180;
                woodCost = 140;
                break;
            case 3:
                metalCost = 280;
                clothCost = 240;
                woodCost = 200;
                break;
            case 4:
                metalCost = 350;
                clothCost = 310;
                woodCost = 240;
                break;
            default:
                metalCost = -1;
                clothCost = -1;
                woodCost = -1;
                break;
        }

        int[] Cost = { metalCost, clothCost, woodCost };
        return Cost;
    }

    private int[] FarmUpCost()
    {
        int metalCost = 0;
        int clothCost = 0;
        int woodCost = 0;

        switch (farmUpgrade)
        {
            case 0:
                metalCost = 50;
                clothCost = 30;
                woodCost = 10;
                break;
            case 1:
                metalCost = 160;
                clothCost = 110;
                woodCost = 90;
                break;
            case 2:
                metalCost = 220;
                clothCost = 180;
                woodCost = 140;
                break;
            case 3:
                metalCost = 280;
                clothCost = 240;
                woodCost = 200;
                break;
            case 4:
                metalCost = 350;
                clothCost = 310;
                woodCost = 240;
                break;
            default:
                metalCost = -1;
                clothCost = -1;
                woodCost = -1;
                break;
        }

        int[] Cost = { metalCost, clothCost, woodCost };
        return Cost;
    }

    // Character upgrades

    private int[] BackpackUpCost()
    {
        int metalCost = 0;
        int clothCost = 0;
        int woodCost = 0;

        switch (BacpackUpgrade)
        {
            case 0:
                metalCost = 10;
                clothCost = 20;
                woodCost = 5;
                break;
            case 1:
                metalCost = 20;
                clothCost = 100;
                woodCost = 10;
                break;
            case 2:
                metalCost = 40;
                clothCost = 180;
                woodCost = 20;
                break;
            case 3:
                metalCost = 80;
                clothCost = 270;
                woodCost = 40;
                break;
            case 4:
                metalCost = 160;
                clothCost = 350;
                woodCost = 80;
                break;
            default:
                metalCost = -1;
                clothCost = -1;
                woodCost = -1;
                break;
        }

        int[] Cost = { metalCost, clothCost, woodCost };
        return Cost;
    }

    private int[] AccuracyUpCost()
    {
        int metalCost = 0;
        int clothCost = 0;
        int woodCost = 0;

        switch (AccuracyUpgrade)
        {
            case 0:
                metalCost = 20;
                clothCost = 10;
                woodCost = 5;
                break;
            case 1:
                metalCost = 100;
                clothCost = 20;
                woodCost = 10;
                break;
            case 2:
                metalCost = 180;
                clothCost = 40;
                woodCost = 20;
                break;
            case 3:
                metalCost = 270;
                clothCost = 80;
                woodCost = 40;
                break;
            case 4:
                metalCost = 350;
                clothCost = 160;
                woodCost = 80;
                break;
            default:
                metalCost = -1;
                clothCost = -1;
                woodCost = -1;
                break;
        }

        int[] Cost = { metalCost, clothCost, woodCost };
        return Cost;
    }

    private int[] StrengthUpCost()
    {
        int foodCost = 0;
        int waterCost = 0;

        switch (StrengthUpgrade)
        {
            case 0:
                foodCost = 40;
                waterCost = 10;
                break;
            case 1:
                foodCost = 100;
                waterCost = 40;
                break;
            case 2:
                foodCost = 160;
                waterCost = 100;
                break;
            case 3:
                foodCost = 240;
                waterCost = 160;
                break;
            case 4:
                foodCost = 350;
                waterCost = 220;
                break;
            default:
                foodCost = -1;
                waterCost = -1;
                break;
        }

        int[] Cost = { foodCost, waterCost };
        return Cost;
    }

    private int[] PerceptionUpCost()
    {
        int foodCost = 0;
        int waterCost = 0;

        switch (PerceptionUpgrade)
        {
            case 0:
                foodCost = 10;
                waterCost = 40;
                break;
            case 1:
                foodCost = 40;
                waterCost = 100;
                break;
            case 2:
                foodCost = 100;
                waterCost = 160;
                break;
            case 3:
                foodCost = 160;
                waterCost = 240;
                break;
            case 4:
                foodCost = 220;
                waterCost = 350;
                break;
            default:
                foodCost = -1;
                waterCost = -1;
                break;
        }

        int[] Cost = { foodCost, waterCost };
        return Cost;
    }

    private int[] SpeedUpCost()
    {
        int foodCost = 0;
        int waterCost = 0;

        switch (SpeedUpgrade)
        {
            case 0:
                foodCost = 40;
                waterCost = 10;
                break;
            case 1:
                foodCost = 100;
                waterCost = 40;
                break;
            case 2:
                foodCost = 160;
                waterCost = 100;
                break;
            case 3:
                foodCost = 240;
                waterCost = 160;
                break;
            case 4:
                foodCost = 350;
                waterCost = 220;
                break;
            default:
                foodCost = -1;
                waterCost = -1;
                break;
        }

        int[] Cost = { foodCost, waterCost };
        return Cost;
    }

    private int[] EnduranceUpCost()
    {
        int foodCost = 0;
        int waterCost = 0;

        switch (EnduranceUpgrade)
        {
            case 0:
                foodCost = 10;
                waterCost = 40;
                break;
            case 1:
                foodCost = 40;
                waterCost = 100;
                break;
            case 2:
                foodCost = 100;
                waterCost = 160;
                break;
            case 3:
                foodCost = 160;
                waterCost = 240;
                break;
            case 4:
                foodCost = 220;
                waterCost = 350;
                break;
            default:
                foodCost = -1;
                waterCost = -1;
                break;
        }

        int[] Cost = { foodCost, waterCost };
        return Cost;
    }
    //------------------------------------------------------------------------------
    // UPGRADE VALUES
    //------------------------------------------------------------------------------
    private int[] FoodStorageUpValue()
    {
        int MaxWater = 0;
        int MaxFood = 0;

        switch (foodStorageUpgrade)
        {
            case 1:
                MaxWater = 130;
                MaxFood = 130;
                break;
            case 2:
                MaxWater = 170;
                MaxFood = 170;
                break;
            case 3:
                MaxWater = 220;
                MaxFood = 220;
                break;
            case 4:
                MaxWater = 280;
                MaxFood = 280;
                break;
            case 5:
                MaxWater = 350;
                MaxFood = 350;
                break;
            default:
                MaxWater = 100;
                MaxFood = 100;
                break;
        }

        int[] Values = { MaxWater, MaxFood };
        return Values;
    }

    private int[] ResourceStorageUpValue()
    {
        int MaxScrap = 0;
        int MaxCloth = 0;
        int MaxWood = 0;
        int MaxMeds = 0;
        int MaxAmmo = 0;

        switch (resourceStorageUpgrade)
        {
            case 1:
                MaxScrap = 170;
                MaxCloth = 170;
                MaxWood = 170;
                MaxMeds = 12;
                MaxAmmo = 130;
                break;
            case 2:
                MaxScrap = 240;
                MaxCloth = 240;
                MaxWood = 240;
                MaxMeds = 14;
                MaxAmmo = 160;
                break;
            case 3:
                MaxScrap = 310;
                MaxCloth = 310;
                MaxWood = 310;
                MaxMeds = 16;
                MaxAmmo = 190;
                break;
            case 4:
                MaxScrap = 380;
                MaxCloth = 380;
                MaxWood = 380;
                MaxMeds = 18;
                MaxAmmo = 250;
                break;
            case 5:
                MaxScrap = 450;
                MaxCloth = 450;
                MaxWood = 450;
                MaxMeds = 20;
                MaxAmmo = 310;
                break;
            default:
                MaxScrap = 100;
                MaxCloth = 100;
                MaxWood = 100;
                MaxMeds = 10;
                MaxAmmo = 100;
                break;
        }

        int[] Values = { MaxScrap, MaxCloth, MaxWood, MaxMeds, MaxAmmo };
        return Values;
    }

    private int WaterCollectorUpValue()
    {
        int waterPerCycle = 0;

        switch (waterCollectorUpgrade)
        {
            case 1:
                waterPerCycle = 70;
                break;
            case 2:
                waterPerCycle = 100;
                break;
            case 3:
                waterPerCycle = 140;
                break;
            case 4:
                waterPerCycle = 190;
                break;
            case 5:
                waterPerCycle = 250;
                break;
            default:
                waterPerCycle = 50;
                break;
        }

        return waterPerCycle;
    }

    private int FarmUpValue()
    {
        int foodPerCycle = 0;

        switch (farmUpgrade)
        {
            case 1:
                foodPerCycle = 70;
                break;
            case 2:
                foodPerCycle = 100;
                break;
            case 3:
                foodPerCycle = 140;
                break;
            case 4:
                foodPerCycle = 190;
                break;
            case 5:
                foodPerCycle = 250;
                break;
            default:
                foodPerCycle = 50;
                break;
        }

        return foodPerCycle;
    }

    //------------------------------------------------------------------------------
    // OTHER METHODS
    //------------------------------------------------------------------------------
    public void OnDeath()
    {
        Hunger = 80;
        Thirst = 80;
        Health = 80;
        Armor = 10;

        BacpackUpgrade = 0;
        AccuracyUpgrade = 0;
        StrengthUpgrade = 0;
        PerceptionUpgrade = 0;
        SpeedUpgrade = 0;
        EnduranceUpgrade = 0;

        UpdateValues();
        //UpdateCostsChar();
    }
}