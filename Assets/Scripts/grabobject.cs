using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabobject : MonoBehaviour
{
    private bool isGrabbing = false;
    private GameObject grabbedObject;
    private float grabDistance = 3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isGrabbing)
            {
                ReleaseObject();
            }
            else
            {
                GrabObject();
            }
        }

        if (isGrabbing)
        {
            UpdateObjectPosition();
        }
    }

 void GrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance))
        {
            if (hit.collider.CompareTag("Grabbable"))
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                isGrabbing = true;
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject = null;
            isGrabbing = false;
        }
    }

    void UpdateObjectPosition()
    {
        if (grabbedObject != null)
        {
            Vector3 newPosition = transform.position + transform.forward * grabDistance;
            grabbedObject.GetComponent<Rigidbody>().MovePosition(newPosition);
        }
    }
}

