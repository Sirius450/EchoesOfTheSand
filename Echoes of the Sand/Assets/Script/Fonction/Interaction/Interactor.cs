using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private float detectionRange = 15;
    [SerializeField] private IInteractable interactable;
    bool findBike = false;
    private HashSet<GameObject> listInteractible = new HashSet<GameObject>();

    Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);
    [SerializeField] LayerMask layerMask;


    [SerializeField] float dist = 999;


    void Update()
    {
        dist = 999;

        foreach (GameObject interact in listInteractible)
        {
            //Debug.DrawLine(transform.position, interact.transform.position, Color.green);
            if (Vector3.Distance(transform.position, interact.transform.position) < dist)
            {
                //check if the object is interactable
                IInteractable i = interact.GetComponent<IInteractable>();
                if (i != null)
                {
                    Debug.DrawLine(transform.position, interact.transform.position, Color.red);
                    interactable = i;
                    //findBike = true;
                }

                dist = Vector3.Distance(transform.position, interact.transform.position);
                Debug.Log("le plus proche " + interact.gameObject.name);
            }
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactable?.Interact();
        }

    }

    private void RaycastDetection()     //le ray va pas a la bonne place
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        // Lancer un rayon depuis le centre de l'écran
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, detectionRange, layerMask))
        {
            IInteractable i = hit.collider.GetComponent<IInteractable>();
            if (i != null)
            {
                interactable = i;
            }
            else
            {
                if (!findBike)
                {
                    interactable = null;
                }
            }
            //Debug.Log("Objet touché : " + hit.transform.name + ", Tag : " + hit.transform.tag);
        }
        else
        {
            interactable = null;
        }

        //Ray ray = new Ray(Camera.main.transform.position + transform.up, Camera.main.transform.forward * detectionRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);
        Gizmos.DrawRay(ray.origin, ray.direction * detectionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bike") || other.CompareTag("Interactible"))
        {
            listInteractible.Add(other.gameObject);
        }



    }

}
