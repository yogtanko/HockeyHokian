using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMovement : MonoBehaviour
{

    public GameObject ball;
    Rigidbody2D rb;
    public float moveSpeed = 10;
    public bool isBottom = true;
    private float puckRadius;
    private float timeRemaining = 1;
    private Vector3 offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        puckRadius = GameObject.FindGameObjectsWithTag("ball")[0].GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 1;
            offset = new Vector3(0, Random.Range(-0.1f, 0.1f));
        }
        moveSpeed = Random.Range(10, 50);
       
        if (ball.transform.position.y + 0.6 < 0 && isBottom)
        {
            if (Vector2.Distance(rb.transform.position,ball.transform.position)==puckRadius)
            {
                MoveTo(new Vector2(0, isBottom ? -3 : 3));
            }
            else
            {
                MoveTo(ball.transform.position);
            }
        }
        else if (ball.transform.position.y - 0.6 > 0 && !isBottom)
        {
            MoveTo(ball.transform.position+offset);
        }
        else if(ball.transform.position == new Vector3(0, 0))
        {
            MoveTo(ball.transform.position+offset);
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
