using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class IAIMovement : HH.Movement
{
    private Rigidbody2D rb;
    private GameObject gameObject;
    private GameObject ball;
    private bool isBottom;
    private float puckRadius;
    private Vector2 offset;

    public IAIMovement(GameObject gameObject)
    {
        this.gameObject = gameObject;
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("ball");
        puckRadius = ball.GetComponent<CircleCollider2D>().radius;
        isBottom = gameObject.transform.position.y > 0 ? false:true;
        offset = new Vector2(Random.Range(-0.1f,0.1f),0);
    }
    public override void MoveTo(float moveSpeed = 100)
    {
        
        if (rb == null)
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
        
        moveSpeed = Random.Range(10, 50);

        if (ball.transform.position.y + 0.2 < 0 && isBottom)
        {
            if (Vector2.Distance(rb.transform.position, ball.transform.position) == puckRadius)
            {
                MoveTo(new Vector2(0, isBottom ? -3 : 3),moveSpeed);
            }
            else
            {
                MoveTo(ball.transform.position, moveSpeed);
            }
        }
        else if (ball.transform.position.y - 0.2 > 0 && !isBottom)
        {
            MoveTo(ball.transform.position, moveSpeed);
        }
        else if (ball.transform.position == new Vector3(0, 0))
        {
            MoveTo(ball.transform.position, moveSpeed);
        }
        else
        {
            MoveTo(new Vector2(0, isBottom ? -3 : 3), moveSpeed);
        }

    }
    private void MoveTo(Vector2 pos,float moveSpeed)
    {
        rb.MovePosition(Vector2.MoveTowards(gameObject.transform.position, pos+offset, moveSpeed * Time.deltaTime));
    }

}
