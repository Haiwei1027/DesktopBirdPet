using UnityEngine;

public class KeyboardControlledMovement : MonoBehaviour
{
    public float speed;

    Transform view;

    Vector2 input;

    // Start is called before the first frame update
    void Start()
    {
        view = Camera.main.transform;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        Vector3 direction = (view.right * input.x + Vector3.Cross(Vector3.down, view.right) * input.y).normalized;
        transform.position = transform.position + (direction * speed * Time.fixedDeltaTime);
        if (direction.magnitude != 0)
        {
            transform.forward = direction;
        }
        */
    }
}
