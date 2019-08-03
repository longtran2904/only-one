using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool activated = false;
    public float rotationSpeed;
    public float speed = 20f;
    private Rigidbody2D rb;
    public float attackRange;
    private Vector2 position;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        float currentRange = Mathf.Abs(gameObject.transform.position.x - position.x);

        if (rb.isKinematic == false)
        {
            transform.localEulerAngles += Vector3.forward * rotationSpeed * Time.deltaTime;
        }

        if (currentRange >= attackRange && activated == false)
        {
            rb.velocity = new Vector2(1f, 0f) * -speed;
            activated = true;
            Debug.Log(currentRange);
        }

        if (currentRange <= attackRange && activated == true)
        {
            rb.gravityScale = 1.0f;
            //rb.velocity = Vector2.zero;
            activated = false;
            Debug.Log(currentRange + "a");
        }
    }

    private void OnTriggerEnter2D(Collider2D hitCollision)
    {
        if (hitCollision.CompareTag("Ground") && ThrowAxe.isPulling == false)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Debug.Log("b");
        }
    }
}

