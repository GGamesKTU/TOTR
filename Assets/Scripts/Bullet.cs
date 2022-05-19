using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float velocity = 20f;
    public float life = 1f;

    [SerializeField]
    private float damage;

    public LayerMask firedByLayer;
    private float lifeTimer;

    private Backpack backpack;

    private void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();

        damage = 20 + backpack.AccuracyUp * 5;
        
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, velocity * Time.deltaTime, firedByLayer))
        {
            transform.position = hit.point;
            Vector3 reflected = Vector3.Reflect(transform.forward, hit.normal);
            Vector3 direction = transform.forward;
            Vector3 vop = Vector3.ProjectOnPlane(reflected, Vector3.forward);
            transform.forward = vop;
            transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);
            Hit(transform.position, direction, reflected, hit.collider);
        }
        else
        {
            transform.Translate(Vector3.forward * velocity * Time.deltaTime);
        }

        if (Time.time > lifeTimer + life)
        {
            Destroy(gameObject);
        }
    }

    private void Hit(Vector3 position, Vector3 direction, Vector3 reflected, Collider collider)
    {
        // Do something here with the object that was hit (collider), e.g. collider.gameObject 
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<MobHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collider.tag != "House1" || collider.tag != "House2" || collider.tag != "House3" || collider.tag != "House4" || collider.tag != "GasStation" || collider.tag != "Store")
        {
            Destroy(gameObject);
        } 
    }

    public void Fire(Vector3 position, Vector3 euler, int layer)
    {
        lifeTimer = Time.time;
        transform.position = position;
        transform.eulerAngles = euler;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Vector3 vop = Vector3.ProjectOnPlane(transform.forward, Vector3.forward);
        transform.forward = vop;
        transform.rotation = Quaternion.LookRotation(vop, Vector3.forward);
    }
}
