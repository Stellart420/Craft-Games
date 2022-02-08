using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Platform : MonoBehaviour, IPooledObject
{
    Rigidbody rb;
    Collider col;
    bool isActivated;

    public UnityAction EnterOnActiveAction;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (col == null)
            col = GetComponentInChildren<Collider>();
    }

    public void Init(string _name, UnityAction enter_on_active_action)
    {
        name = _name;
        EnterOnActiveAction += () => enter_on_active_action();
    }
    public void Init(Vector3 _pos)
    {
        transform.position = _pos;
    }

    public void Init(UnityAction enter_on_active_action)
    {
        EnterOnActiveAction += () => enter_on_active_action();
    }

    public void Reset()
    {
        isActivated = false;
        EnterOnActiveAction = null;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y < Constants.deathZone)
        {
            isActivated = false;
            EnterOnActiveAction = null;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (!isActivated)
            {
                EnterOnActiveAction?.Invoke();
                isActivated = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (isActivated && rb.isKinematic)
            {
                col.isTrigger = true;
                rb.isKinematic = false;

            }
        }
    }

    public void OnObjectSpawn()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (col == null)
            col = GetComponentInChildren<Collider>();

        rb.isKinematic = true;
        col.isTrigger = false;
    }
}
