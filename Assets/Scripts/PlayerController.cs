using DialogueEditor;
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
    RaycastHit2D[] hitFog;
    [SerializeField] private Tilemap tilemapFog;
    [SerializeField] private TileBase lightFog;
    [SerializeField] private TileBase blankFog;
    private SpriteRenderer sr;
    [SerializeField] private Sprite player_0;
    [SerializeField] private Sprite player_1;
    [SerializeField] private Sprite player_2;
    [SerializeField] private Sprite player_3;
    [SerializeField] private Text txtCount;
    private GameObject tutCanvas;
    private GameObject minimap;
    private bool mapOpen;
    private GameObject controls;
    private BoxCollider2D bc2D;
    private float counter;
    private float stepCount = 0;
    [SerializeField] private AudioClip audioMove;
    private AudioSource audioSource;

    [SerializeField] private NPCConversation Conversation_1;
    [SerializeField] private NPCConversation Conversation_2;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tutCanvas = GameObject.FindGameObjectWithTag("Tutorial");
        audioSource = GetComponent<AudioSource>();
        minimap = GameObject.Find("Map");
        minimap.SetActive(false);
        controls = GameObject.Find("Controls");
        controls.SetActive(true);
        bc2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
       
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) {
            minimap.SetActive(false);
            controls.SetActive(false);
            hitFog = Physics2D.CircleCastAll(transform.position, 0.5f, Vector2.zero, layerFog);

            for (int i = 0; i < hitFog.Length; i++) {
                if (hitFog[i].collider != null) {
                    var tpos = tilemapFog.WorldToCell(hitFog[i].point);
                    var tileInfo = tilemapFog.GetTile(tpos).name;
                    if (tileInfo.Equals("FogOfWar_Dark")) {
                        tilemapFog.SetTile(tpos, lightFog);
                    }
                    stepCount += 1;
                    txtCount.text = stepCount.ToString();
                }
            } 
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            sr.sprite = player_1;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.up, 1f, layerWalls);
            if (hit.collider == null) {
                yMove += 1f;
                audioSource.PlayOneShot(audioMove);
            }

        }
        if (Input.GetKeyDown(KeyCode.A)) {
            sr.sprite = player_2;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.left, 1f, layerWalls);
            if (hit.collider == null) {
                xMove -= 1f;
                audioSource.PlayOneShot(audioMove);
            }

        }
        if (Input.GetKeyDown(KeyCode.S)) {
            sr.sprite = player_0;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.down, 1f, layerWalls);

            if (hit.collider == null) {
                yMove -= 1f;
                audioSource.PlayOneShot(audioMove);
            }

        }
        if (Input.GetKeyDown(KeyCode.D)) {
            sr.sprite = player_3;
            hit = Physics2D.Raycast(bc2D.transform.position, Vector2.right, 1f, layerWalls);
            if (hit.collider == null) {
                xMove += 1f;
                audioSource.PlayOneShot(audioMove);
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
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        counter += 1;
        if(counter == 1) {
            ConversationManager.Instance.StartConversation(Conversation_1);
        } else if (counter == 2) {
            ConversationManager.Instance.StartConversation(Conversation_2);
        }
    }
}

