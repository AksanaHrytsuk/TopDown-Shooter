using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseClass
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }

    public override void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        GetRig().velocity = new Vector2(inputX, inputY) * speed;
        GetAnimator().SetFloat("Speed", GetRig().velocity.magnitude);
        
    }
    
    public override void Rotate()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = transform.position - mouseWorldPosition ;
        transform.up = direction;
    }
}
