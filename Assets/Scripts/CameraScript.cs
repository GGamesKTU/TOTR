using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Transform player;


    [SerializeField]
    private float speed = 0.15f;

    [SerializeField]
    private float movementrange = 9.5f;

    public float xMin;
    public float xMax;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    Vector3 lastmouse = new Vector3(0, 0, 0);

    private void LateUpdate()
    {
        Vector3 mouse = new Vector3(0,0,0);

        if ((Input.mousePosition.x <= Screen.width && Input.mousePosition.x > 0) && (Input.mousePosition.y <= Screen.height && Input.mousePosition.y > 0))
        {
            mouse = new Vector3(Input.mousePosition.x - (Screen.width / 2f), 0, 0);
        }
        else
        {
            mouse = lastmouse;
        }

        Vector3 offset = new Vector3(player.position.x, 2.3f, -10);
        Vector3 desiredPosition = offset + mouse / (Screen.width / movementrange);

        if (mouse != Vector3.zero)
        {
            lastmouse = mouse;
        }

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, xMin, xMax);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed);
    }
}
