using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{

    public GameObject ball;
    Rigidbody2D rb;
    public float moveSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, ball.transform.position, moveSpeed * Time.deltaTime));
    }
}
