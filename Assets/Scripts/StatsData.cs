using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatsData
{
    public int Scrap;
    public int MaxScrap;

    public int Wood;
    public int MaxWood;

    public int Cloth;
    public int MaxCloth;

    public float Medicine;
    public float MaxMedicine;

    public float Ammo;
    public float MaxAmmo;

    public int Water;
    public int MaxWater;

    public int Food;
    public int MaxFood;

    public int Hunger;
    public int Thirst;

    public float Health;
    public float Armor;

    public int waterPerCycle;
    public int wateCollected;

    public int foodPerCycle;
    public int foodCollected;

    public int BacpackUpgrade;
    public int AccuracyUpgrade;
    public int StrengthUpgrade;
    public int PerceptionUpgrade;
    public int SpeedUpgrade;
    public int EnduranceUpgrade;


    public int foodStorageUpgrade;
    public int resourceStorageUpgrade;
    public int waterCollectorUpgrade;
    public int farmUpgrade;

    public StatsData (Stats stat)
    {
        Scrap = stat.Scrap;
        MaxScrap = stat.MaxScrap;

        Wood = stat.Wood;
        MaxWood = stat.MaxWood;

        Cloth = stat.Cloth;
        MaxCloth = stat.MaxCloth;

        Medicine = stat.Medicine;
        MaxMedicine = stat.MaxMedicine;

        Ammo = stat.Ammo;
        MaxAmmo = stat.MaxAmmo;

        Water = stat.Water;
        MaxWater = stat.MaxWater;

        Food = stat.Food;
        MaxFood = stat.MaxFood;

        Hunger = stat.Hunger;
        Thirst = stat.Thirst;

        Health = stat.Health;
        Armor = stat.Armor;

        waterPerCycle = stat.waterPerCycle;
        wateCollected = stat.wateCollected;

        foodPerCycle = stat.foodPerCycle;
        foodCollected = stat.foodCollected;

        BacpackUpgrade = stat.BacpackUpgrade;
        AccuracyUpgrade = stat.AccuracyUpgrade;
        StrengthUpgrade = stat.StrengthUpgrade;
        PerceptionUpgrade = stat.PerceptionUpgrade;
        SpeedUpgrade = stat.SpeedUpgrade;
        EnduranceUpgrade = stat.EnduranceUpgrade;


        foodStorageUpgrade = stat.foodStorageUpgrade;
        resourceStorageUpgrade = stat.resourceStorageUpgrade;
        waterCollectorUpgrade = stat.waterCollectorUpgrade;
        farmUpgrade = stat.farmUpgrade;
    }
}
