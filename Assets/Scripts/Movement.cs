
using UnityEngine;


public class Movement : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject thisObject;
    Rigidbody2D rb;
    public float moveSpeed = 100;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
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
        if (selectedObject == thisObject)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, mousePos, moveSpeed * Time.deltaTime));

        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject = null;
            rb.velocity = Vector2.zero;
        }
    }

}
