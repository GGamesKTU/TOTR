using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private float minDmg = 5;

    [SerializeField]
    private float maxDmg = 10;

    [SerializeField]
    private float meleeRange = 1f;

    [SerializeField]
    private Transform target;

    private LayerMask enemyLayer;

    private Animator playerAnimator;

    private Stamina stamina;

    float timer = 0f;

    private Backpack backpack;

    // Start is called before the first frame update
    void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        minDmg = 6 + backpack.StrengthUp *2;
        maxDmg = 10 + backpack.StrengthUp * 2;

        playerAnimator = GetComponent<Animator>();
        enemyLayer = 1 << 8;
        stamina = GetComponent<Stamina>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer != 0)
        {
            timer += Time.deltaTime;
        }

        if(timer >= 0.7f)
        {
            Attack();
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.F) && stamina.GetStamina() >= 10f && timer == 0f)
        {
            playerAnimator.SetTrigger("isMeleeing");
            timer += Time.deltaTime;
        }
    }

    private void Attack()
    {  
        stamina.UseStamina(10f);

        Collider[] hitEnemies = Physics.OverlapSphere(target.position, meleeRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            MobHealth enem = enemy.GetComponent<MobHealth>();
            if(enem)
            {
                enem.TakeDamage(Random.Range(minDmg,maxDmg));
            }
        }

    }
}

