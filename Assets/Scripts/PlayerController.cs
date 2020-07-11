using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float xMove = -1.5f;
    private float yMove = -0.5f;
    LayerMask layerWalls = 1 << 8;
    private Rigidbody2D rb;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            hit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, layerWalls);
            if (hit.collider == null) {
                yMove += 1;
                transform.rotation = Quaternion.Euler(0,0, -90);
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, layerWalls);
            if (hit.collider == null) {
                xMove -= 1;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layerWalls);
            if (hit.collider == null) {
                yMove -= 1;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layerWalls);
            if (hit.collider == null) {
                xMove += 1;
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }

        rb.MovePosition(new Vector2(xMove, yMove));
    }
}

