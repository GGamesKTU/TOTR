using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Loot
{
    public class Looting : MonoBehaviour
    {
        //Weights of lootable items
        [SerializeField] private float weightScrap = 2;
        [SerializeField] private float weightPlank = 2;
        [SerializeField] private float weightCloth = 2;
        [SerializeField] private float weightWater = 2;
        [SerializeField] private float weightFood = 2;
        private float totalWeight;

        //Number of items that can be looted
        [SerializeField] private int lootableItems = 25;

        //Text variables for UI
        [SerializeField] private Text scrapText;
        [SerializeField] private Text plankText;
        [SerializeField] private Text clothText;
        [SerializeField] private Text waterText;
        [SerializeField] private Text foodText;

        //Other variables
        [SerializeField] private GameObject other;
        [SerializeField] private GameObject lootMessage;
        [SerializeField] private Light triggerLight;

        List<WeightItem> _items;

        bool isTriggered = false;
        bool isLooted = false;

        private Backpack backpack;
        private float multiplier;

        private void Start()
        {
            backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
            multiplier = backpack.PerceptionUp;

            lootableItems = (int)Mathf.Round(lootableItems + (lootableItems * multiplier));

            triggerLight.enabled = false;

            totalWeight = weightCloth + weightFood + weightPlank + weightScrap + weightWater;

            _items = new List<WeightItem>()
            {
                new WeightItem {name = "scrap", weight = weightScrap, quantity = backpack.scrapMetal},
                new WeightItem {name = "plank", weight = weightPlank, quantity = backpack.wood},
                new WeightItem {name = "cloth", weight = weightCloth, quantity = backpack.cloth},
                new WeightItem {name = "water", weight = weightWater, quantity = backpack.water},
                new WeightItem {name = "food" , weight = weightFood , quantity = backpack.food}
            };
        }

        void OnTriggerStay(Collider other)
        {
            if (other.attachedRigidbody && other.CompareTag("Player"))
            {
                if(!isLooted)
                {
                    lootMessage.SetActive(true);
                }

                isTriggered = true;

                if(isLooted == false)
                {
                    triggerLight.enabled = true;
                }
            }
        }
        void OnTriggerExit(Collider other)
        {
            if (other.attachedRigidbody && other.CompareTag("Player"))
            {
                lootMessage.SetActive(false);
                isTriggered = false;

                triggerLight.enabled = false;
            }
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.E) && isTriggered && !isLooted)
            {
                //Should probably add a delay to have a looting "progress"
                avoidWrongNumbers();
                backpack.SaveState();
                startLooting();
                backpack.SaveState();

                triggerLight.enabled = false;
                lootMessage.SetActive(false);
            }
        }

        private void startLooting()
        {
            while(lootableItems > 0)
            {
                float diceRoll = Random.Range(0f, totalWeight);

                foreach (var item in _items)
                {
                    if (item.weight >= diceRoll)
                    {
                        item.quantity++;
                        lootableItems--;
                        break;
                    }

                    diceRoll -= item.weight;
                }
            }
            isLooted = true;
            avoidWrongNumbers();
            updateValues();
        }

        //Updates user interface and backpack with new values
        public void updateValues()
        {
            foreach (var item in _items)
            {
                if(item.name == "scrap")
                {
                    scrapText.text = $"{(int)item.quantity}";
                    backpack.scrapMetal = item.quantity;
                }
                else if (item.name == "plank")
                {
                    plankText.text = $"{(int)item.quantity}";
                    backpack.wood = item.quantity;
                }
                else if (item.name == "cloth")
                {
                    clothText.text = $"{(int)item.quantity}";
                    backpack.cloth = item.quantity;
                }
                else if (item.name == "water")
                {
                    waterText.text = $"{(int)item.quantity}";
                    backpack.water = item.quantity;
                }
                else if (item.name == "food")
                {
                    foodText.text = $"{(int)item.quantity}";
                    backpack.food = item.quantity;
                }
            }
        }

        //Method to update item quantities before looting. This way, different
        //looting places shouldn't ignore what You received from previous loot place
        public void avoidWrongNumbers()
        {
            foreach (var item in _items)
            {
                if (item.name == "scrap")
                {
                    if(item.quantity < backpack.scrapMetal)
                    {
                        item.quantity = backpack.scrapMetal;
                    }

                    if(item.quantity > backpack.scrapMax)
                    {
                        item.quantity = backpack.scrapMax;
                    }
                }
                else if (item.name == "plank")
                {
                    if (item.quantity < backpack.wood)
                    {
                        item.quantity = backpack.wood;
                    }
                    
                    if (item.quantity > backpack.woodMax)
                    {
                        item.quantity = backpack.woodMax;
                    }
                }
                else if (item.name == "cloth")
                {
                    if (item.quantity < backpack.cloth)
                    {
                        item.quantity = backpack.cloth;
                    }
                    
                    if (item.quantity > backpack.clothMax)
                    {
                        item.quantity = backpack.clothMax;
                    }
                }
                else if (item.name == "water")
                {
                    if (item.quantity < backpack.water)
                    {
                        item.quantity = backpack.water;
                    }
                    
                    if (item.quantity > backpack.waterMax)
                    {
                        item.quantity = backpack.waterMax;
                    }
                }
                else if (item.name == "food")
                {
                    if (item.quantity < backpack.food)
                    {
                        item.quantity = backpack.food;
                    }
                    
                    if (item.quantity > backpack.foodMax)
                    {
                        item.quantity = backpack.foodMax;
                    }
                }
            }
        }
    }

    public class WeightItem
    {
        public string name { get; set; }
        public float weight { get; set; }
        public int quantity { get; set; }
    }
}
