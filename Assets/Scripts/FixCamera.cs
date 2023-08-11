using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Vector3 cameraRightMiddle = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector3 cameraRightMiddle = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)); 
        //cameraRightMiddle.z = transform.position.z;
        transform.position = cameraRightMiddle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
