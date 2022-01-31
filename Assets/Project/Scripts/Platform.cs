using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody rigidbody;
    Collider collider;
    bool isActivated;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponentInChildren<Collider>();
    }

    void Update()
    {
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isActivated)
            {
                PlatformGenerator.Instance.NextPlatform();
                isActivated = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (isActivated && rigidbody.isKinematic)
            {
                collider.isTrigger = true;
                rigidbody.isKinematic = false;

            }
        }
    }
}
