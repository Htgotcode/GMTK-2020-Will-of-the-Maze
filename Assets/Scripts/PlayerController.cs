using DialogueEditor;
using System;
using System.Diagnostics.PerformanceData;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private float xMove = 10.5f;
    private float yMove = 10.5f;
    LayerMask layerWalls = 1 << 8;
    LayerMask layerFog = 1 << 9;
    private Rigidbody2D rb;
    RaycastHit2D hit;

    [SerializeField] private Tilemap tilemapFog;
    [SerializeField] private TileBase lightFog;
    [SerializeField] private TileBase blankFog;
    private SpriteRenderer sr;
    [SerializeField] private GameObject compass;
    [SerializeField] private Sprite player_0;
    [SerializeField] private Sprite player_1;
    [SerializeField] private Sprite player_2;
    [SerializeField] private Sprite player_3;
    [SerializeField] private Text txtCount;
    private GameObject minimap;
    private bool mapOpen;
    private GameObject controls;
    private BoxCollider2D bc2D;
    private float counter;
    private float stepCount = 0;
    [SerializeField] private AudioClip audioMove;
    private AudioSource audioSource;

    private GameManager gm;

    [SerializeField] private NPCConversation Conversation_1;
    [SerializeField] private NPCConversation Conversation_2;

    [SerializeField] private GameObject nav;
    [SerializeField] private GameObject navTarget;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        minimap = GameObject.Find("Map");
        minimap.SetActive(false);
        controls = GameObject.Find("Controls");
        controls.SetActive(true);
        bc2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        var dir = navTarget.transform.position - nav.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        nav.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetKeyDown(KeyCode.W)) {
            sr.sprite = player_1;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.up, 1f, layerWalls);
            if (hit.collider == null) {
                yMove += 1f;
                audioSource.PlayOneShot(audioMove);
                stepCount += 1;
                UpdateFog();
            }
        }
        if (Input.GetKeyDown(KeyCode.A)) {
            sr.sprite = player_2;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.left, 1f, layerWalls);
            if (hit.collider == null) {
                xMove -= 1f;
                audioSource.PlayOneShot(audioMove);
                stepCount += 1;
                UpdateFog();
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            sr.sprite = player_0;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.down, 1f, layerWalls);

            if (hit.collider == null) {
                yMove -= 1f;
                audioSource.PlayOneShot(audioMove);
                stepCount += 1;
                UpdateFog();
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            sr.sprite = player_3;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.right, 1f, layerWalls);
            if (hit.collider == null) {
                xMove += 1f;
                audioSource.PlayOneShot(audioMove);
                stepCount += 1;
                UpdateFog();
            }
        }
        if (Input.GetKeyDown(KeyCode.M) && mapOpen == false) {
            minimap.SetActive(true);
            mapOpen = true;
        } else if (Input.GetKeyDown(KeyCode.M) && mapOpen == true) {
            minimap.SetActive(false);
            mapOpen = false;
        }

        rb.MovePosition(new Vector2(xMove, yMove));
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
            minimap.SetActive(false);
            controls.SetActive(false);
            txtCount.text = stepCount.ToString();
        }
    }

    private void UpdateFog() {

        //line 1
        RaycastHit2D hitFog1;
        hitFog1 = Physics2D.Raycast(bc2D.transform.localPosition, new Vector2(1,0), 1f, layerFog);
            if (hitFog1.collider != null) {
                var tpos = tilemapFog.WorldToCell(hitFog1.point);
                var tileInfo = tilemapFog.GetTile(tpos).name;
                if (tileInfo.Equals("FogOfWar_Dark")) {
                    tilemapFog.SetTile(tpos, lightFog);
                }
        }
        //line 2
        RaycastHit2D hitFog2;
        hitFog2 = Physics2D.Raycast(bc2D.transform.localPosition, new Vector2(0, 1), 1f, layerFog);
        if (hitFog2.collider != null) {
            var tpos = tilemapFog.WorldToCell(hitFog2.point);
            var tileInfo = tilemapFog.GetTile(tpos).name;
            if (tileInfo.Equals("FogOfWar_Dark")) {
                tilemapFog.SetTile(tpos, lightFog);
            }
        }
        //line 3
        RaycastHit2D hitFog3;
        hitFog3 = Physics2D.Raycast(bc2D.transform.localPosition, new Vector2(-1, 0), 1f, layerFog);
        if (hitFog3.collider != null) {
            var tpos = tilemapFog.WorldToCell(hitFog3.point);
            var tileInfo = tilemapFog.GetTile(tpos).name;
            if (tileInfo.Equals("FogOfWar_Dark")) {
                tilemapFog.SetTile(tpos, lightFog);
            }
        }
        //line 4
        RaycastHit2D hitFog4;
        hitFog4 = Physics2D.Raycast(bc2D.transform.localPosition, new Vector2(0, -1), 1f, layerFog);
        if (hitFog4.collider != null) {
            var tpos = tilemapFog.WorldToCell(hitFog4.point);
            var tileInfo = tilemapFog.GetTile(tpos).name;
            if (tileInfo.Equals("FogOfWar_Dark")) {
                tilemapFog.SetTile(tpos, lightFog);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Dialogue")) {
            counter += 1;
            if (counter == 1) {
                ConversationManager.Instance.StartConversation(Conversation_1);
                GameObject.Find("Dialogue Trigger").SetActive(false);
            } else if (counter == 2) {
                ConversationManager.Instance.StartConversation(Conversation_2);
                GameObject.Find("Dialogue Trigger (1)").SetActive(false);
            }
        }
        if (collision.tag.Equals("Compass")) {
            nav.GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("compass").SetActive(false);
        }
    }
}

