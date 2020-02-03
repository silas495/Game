using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6;


    private float yaw = 0.0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire")) fire();
        Vector3 movement = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        movement = Vector3.ClampMagnitude(movement, 1) * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void FixedUpdate()
    {
        //turret.localEulerAngles += new Vector3(0, 1, 0) * Time.deltaTime * 30 * Input.GetAxisRaw("RotateTurret");
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.up * -1, out hit, 25.0f))
        //{
        //    transform.eulerAngles = hit.collider.transform.eulerAngles;
        //    transform.Translate(transform.up * -1 * (hit.distance - hoverHeight));
        //}
        yaw += 4 * Input.GetAxis("Mouse X");
        transform.eulerAngles = new Vector3(0, yaw, 0);
    }

    void fire()
    {
        //RaycastHit hit;
        //Debug.DrawRay(barrel.position, barrel.forward, Color.cyan, 1000);
        //if (Physics.Raycast(barrel.position, barrel.forward, out hit, 1000))
        //{
        //    if (hit.collider.gameObject.tag != "Indestructible")
        //    {
        //        Debug.Log(hit.collider.gameObject.name);
        //        Destroy(hit.collider.gameObject);
        //    }
        //}
    }
}
