using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicMusic : MonoBehaviour
{
    public static DynamicMusic Instance;

    private AudioSource[] Music;
    private HealthAndArmor hp;
    private bool isAttacked = false;

    private GameObject [] Enemys;
    private float spawnTime = 0f;

    private int currentClip = 0;
    private int nextClip = 0;

    public float spawnTimer = 30f;
    private float changeTimer = 10f;

    private bool changing = false;

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

        Music = GetComponents<AudioSource>();
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthAndArmor>();

        AddNewEnemys();

        if(hp.GetHealth() <= 40f) currentClip = 1;
        Music[currentClip].mute = false;
        Music[currentClip].volume = 1;
    }

    private void OnLevelWasLoaded(int level)
    {
        if(SceneManager.GetActiveScene().name == "MainMenuScene" || SceneManager.GetActiveScene().name == "HomeBase")
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            Music = GetComponents<AudioSource>();
            hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthAndArmor>();

            AddNewEnemys();

            if (hp.GetHealth() <= 40f) currentClip = 1;
            Music[currentClip].mute = false;
            Music[currentClip].volume = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnTime)
        {
            AddNewEnemys();
            spawnTimer = 0f;
        }

        float health = hp.GetHealth();
        isAttacked = ChcekState();

        if (health <= 40f && currentClip != 1 && !isAttacked) nextClip = 1;
        else if (health > 40f && currentClip != 0 && !isAttacked) nextClip = 0;
        else if (health > 40f && currentClip != 2 && isAttacked) nextClip = 2;
        else if (health <= 40f && currentClip != 3 && isAttacked) nextClip = 3;

        if(nextClip != currentClip && !changing)
        {
            Music[nextClip].mute = false;
            changeTimer = 0f;
            changing = true;
        }
        changeTimer += Time.deltaTime;

        if(changeTimer < 10f && changing)
        {
            Music[nextClip].volume = 0.1f * changeTimer;
            Music[currentClip].volume = 1 -0.1f * changeTimer;
        }

        if(changeTimer > 10f && changing)
        {
            changing = false;
            Music[currentClip].mute = true;
            Music[currentClip].volume = 0f;
            Music[nextClip].volume = 1f;
            currentClip = nextClip;
        }
    }

    private bool ChcekState()
    {
        bool isChased = false;

        foreach(GameObject enemy in Enemys)
        {
            Animator enemyAnimator = enemy.GetComponent<Animator>();
            if (enemyAnimator.enabled && (enemyAnimator.GetBool("IsChasing") || enemyAnimator.GetBool("IsAttacking")))
            {
                isChased = true;
            }
        }

        return isChased;
    }

    private void AddNewEnemys()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
