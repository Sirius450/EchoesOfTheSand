using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : InteractableObjectBase
{
    Rigidbody rb;
    BoxCollider boxCollider;
    //public List<GameObject> listInteractible/* = new List<GameObject>()*/ = ;
    public bool transport;
    [SerializeField] GameObject point;
    public GameObject cargo;
    public bool inCargo;
    public bool findCargo;
    public GameObject[] cargoObjects;
    public float dist = 999;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = rb.GetComponent<BoxCollider>();
        //cargoObjects.Add(GameObject.FindWithTag("Cargo"));


        cargoObjects = GameObject.FindGameObjectsWithTag("Cargo");

    }

    private void Update()
    {
        dist = 999;


        foreach (GameObject obj in cargoObjects)
        {
            if (Vector3.Distance(transform.position, obj.transform.position) < dist)
            {
                dist = Vector3.Distance(transform.position, obj.transform.position);
                if (dist < 1f)
                {
                    cargo = obj;
                    findCargo = true;

                }
                else
                {
                    cargo = null;
                    findCargo = false;
                }
            }
        }
        Debug.DrawLine(transform.position, cargo.transform.position, Color.magenta);
    }

    public override void Interact()
    {
        Debug.Log("yo it's work " + this.gameObject.name);


        if (point != null)
        {
            if (!transport)
            {
                rb.isKinematic = true;
                boxCollider.isTrigger = true;
                transport = true;
                inCargo = false;

                transform.parent = point.transform;
                transform.position = point.transform.position;
                transform.rotation = point.transform.rotation;


            }
            else
            {

                if (findCargo)
                {
                    transform.parent = cargo.transform;
                    transform.position = cargo.transform.position;
                    transform.rotation = cargo.transform.rotation;

                    rb.isKinematic = true;
                    boxCollider.isTrigger = true;
                    inCargo = true;
                    transport = false;
                }
                else
                {
                    rb.isKinematic = false;
                    boxCollider.isTrigger = false;
                    transport = false;
                    transform.parent = null;
                }


            }
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Interactible")
    //    {
    //        listInteractible.Add(collision.gameObject);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Cargo"))
    //    {
    //        listInteractible.Add(other.gameObject);
    //    }
    //}
}
