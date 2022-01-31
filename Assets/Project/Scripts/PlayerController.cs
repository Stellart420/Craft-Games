using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    bool isStarted => GameController.Instance.IsStarted;
    [SerializeField] float speed = 6f;
    bool isGoingRight = true;
    bool isAlive = true;

    Rigidbody rigidbody;

    Vector3 startPos;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    void Start()
    {
        GameController.Instance.RestartAction += Reset;
        rigidbody = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    void Update()
    {
        if (isStarted && isAlive)
        {
            Move();
        }
    }

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        transform.position = startPos;
        isAlive = true;
        isGoingRight = true;
    }

    public void ChangeTurn()
    {
        isGoingRight = !isGoingRight;
    }

    void Move()
    {
        if (isGoingRight)
        {
            rigidbody.velocity = (Vector3.right * speed) + Physics.gravity;
        }
        else
        {
            rigidbody.velocity = (Vector3.forward * speed) + Physics.gravity;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathZone")
        {
            isAlive = false;
            rigidbody.velocity = Physics.gravity;
            GameController.Instance.GameOver();
        }
    }
}
