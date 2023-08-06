using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{

    public GameObject ball;
    Rigidbody2D rb;
    public float moveSpeed = 10;
    public bool isBottom = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = Random.Range(10, 50);
       
        if (ball.transform.position.y + 0.6 < 0 && isBottom)
        {
            MoveTo(ball.transform.position);
        }
        else if (ball.transform.position.y - 0.6 > 0 && !isBottom)
        {
            MoveTo(ball.transform.position);
        }
        else if(ball.transform.position == new Vector3(0, 0))
        {
            MoveTo(ball.transform.position);
        }
        else
        {
            MoveTo(new Vector2(0, isBottom ? -3:3));
        }
   
    }
    void MoveTo(Vector2 pos)
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, pos, moveSpeed * Time.deltaTime));
    }
}
