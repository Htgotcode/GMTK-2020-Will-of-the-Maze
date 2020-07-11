using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    private float xMove = -1.5f;
    private float yMove = -0.5f;
    LayerMask layerWalls = 1 << 8;
    LayerMask layerFog = 1 << 9;
    private Rigidbody2D rb;
    RaycastHit2D hit;
    RaycastHit2D[] hitFog = new RaycastHit2D[9];
    [SerializeField] private Tilemap tilemapFog;
    [SerializeField] private TileBase lightFog;
    [SerializeField] private TileBase blankFog;
    private SpriteRenderer sr;
    [SerializeField] private Sprite player_0;
    [SerializeField] private Sprite player_1;
    [SerializeField] private Sprite player_2;
    [SerializeField] private Sprite player_3;
    private GameObject tutCanvas;
    private GameObject tutText1;
    private GameObject tutText2;
    private GameObject tutTrigger;
    private int counterSB = 0;

    [SerializeField] private AudioClip audioMove;
    private AudioSource audioSource;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tutCanvas = GameObject.FindGameObjectWithTag("Tutorial");
        tutText1 = GameObject.Find("Tutorial Text_1");
        tutText2 = GameObject.Find("Tutorial Text_2");
        tutTrigger = GameObject.Find("Trigger Tutorial");
        tutCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.anyKey) { 
            hitFog = Physics2D.CircleCastAll(transform.position, 1f, transform.position, layerFog);

            for (int i = 0; i < hitFog.Length; i++) {
                if (hitFog[i].collider != null) {
                    var tpos = tilemapFog.WorldToCell(hitFog[i].point);
                    var tileInfo = tilemapFog.GetTile(tpos).name;
                    if (tileInfo.Equals("FogOfWar_Dark")) {
                        tilemapFog.SetTile(tpos, lightFog);
                    } 
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            sr.sprite = player_1;
            hit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f, layerWalls);
            if (hit.collider == null) {
                yMove += 1;
                audioSource.PlayOneShot(audioMove);
            }

        }
        if (Input.GetKeyDown(KeyCode.A)) {
            sr.sprite = player_2;
            hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, layerWalls);
            if (hit.collider == null) {
                xMove -= 1;
                audioSource.PlayOneShot(audioMove);
            }

        }
        if (Input.GetKeyDown(KeyCode.S)) {
            sr.sprite = player_0;
            hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, layerWalls);

            if (hit.collider == null) {
                yMove -= 1;
                audioSource.PlayOneShot(audioMove);
            }

        }
        if (Input.GetKeyDown(KeyCode.D)) {
            sr.sprite = player_3;
            hit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f, layerWalls);
            if (hit.collider == null) {
                xMove += 1;
                audioSource.PlayOneShot(audioMove);
            }

        }

        rb.MovePosition(new Vector2(xMove, yMove));

        if (Input.GetKeyDown(KeyCode.Space)) {
            tutText1.SetActive(false);
            tutText2.SetActive(true);
            counterSB += 1;
            if (counterSB == 2) {
                tutCanvas.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        tutCanvas.SetActive(true);
        tutText2.SetActive(false);
        tutTrigger.SetActive(false);
    }
}

