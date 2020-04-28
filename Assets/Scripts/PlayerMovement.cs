
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Config Parameters")]
    public float speed;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    { 
        // Debug.Log(transform.up);
       Move();
       Rotate();
    }

    void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        _rigidbody2D.velocity = new Vector2(inputX, inputY) * speed;
        
        _animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);
        
        // move with upt. transform
        // Vector3 newPosition = transform.position;
        // newPosition.x += speed * Time.deltaTime * inputX;
        // transform.position = newPosition; 
    }

    void Rotate()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = transform.position - mouseWorldPosition ;
        transform.up = direction;
    }
}
