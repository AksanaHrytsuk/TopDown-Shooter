using UnityEngine;

public class Player : BaseClass
{
    public float maxHealth;

    public GameObject _image;
    
    void Update()
    {
        Move();
        Rotate();
    }

    public override void StartAdditional()
    {
        _image.SetActive(false);
    }
    public override void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        if (GetRig() != null)
        {
            GetRig().velocity = new Vector2(inputX, inputY) * speed;
            GetAnimator().SetFloat("Speed", GetRig().velocity.magnitude);
        }
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
        _image.SetActive(true);
    }
  
}
