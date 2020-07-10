using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float xMove = -1.5f;
    private float yMove = -0.5f;


    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W)) {
            yMove += 1;
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            xMove -= 1;
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            yMove -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            xMove += 1;
        }

        rb.MovePosition(new Vector2(xMove, yMove));
    }
}
