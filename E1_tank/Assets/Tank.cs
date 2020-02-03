﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float hoverHeight = 0.2f;
    public float speed = 15;

    Transform turret;
    Transform barrel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
        turret = transform.Find("Turret");
        barrel = transform.Find("Turret/BarrelTip");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire")) fire();

    }

    private void FixedUpdate()
    {
        turret.localEulerAngles += new Vector3(0, 1, 0) * Time.deltaTime * 30 * Input.GetAxisRaw("RotateTurret");
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward*0.5f, transform.up * -1, out hit, 25.0f)) // cast at the front so we don't risk hitting previous plane after rotation
        {
            //transform.eulerAngles = hit.collider.transform.eulerAngles;
            Debug.Log(hit.normal);
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.Translate(transform.up * -1 * (hit.distance - hoverHeight));
        }

        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        movement = Vector3.ClampMagnitude(movement, 1) * speed * Time.deltaTime;
        transform.Translate(movement);
    }

    void fire()
    {
        RaycastHit hit;
        Debug.DrawRay(barrel.position, barrel.forward, Color.cyan, 1000);
        if (Physics.Raycast(barrel.position, barrel.forward, out hit, 1000))
        {
            if(hit.collider.gameObject.tag != "Indestructible")
            {
                Debug.Log(hit.collider.gameObject.name);
                Destroy(hit.collider.gameObject);
            }
        }
    }
}