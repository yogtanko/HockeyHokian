using UnityEngine;

public class IPlayerMovement : HH.Movement
{

    private Rigidbody2D rb;
    private GameObject selectedObject,gameObject;
    public IPlayerMovement(GameObject gameObject)
    {
        this.gameObject = gameObject;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public override void MoveTo(float moveSpeed = 100)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePos);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
            }
        }
        if (selectedObject == gameObject)
        {
            rb.MovePosition(Vector2.MoveTowards(gameObject.transform.position, mousePos, moveSpeed * Time.deltaTime));
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
            rb.velocity = Vector2.zero;
        }
    }
}