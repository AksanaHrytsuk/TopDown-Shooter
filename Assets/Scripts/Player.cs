using UnityEngine;

public class Player : BaseClass
{
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
        if (health > 0)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = transform.position - mouseWorldPosition;
            transform.up = direction;
        }
    }

    public override void Death()
    {
        base.Death();
        Destroy(GetWeapon());
    }
}
