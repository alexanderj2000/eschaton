using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private Rigidbody2D rb;
	private SpriteRenderer sr;

	public GameObject terrain;
	private Terrain terrainScript;
    
    private static float maxHealth = 100f;
    private static float maxHunger = 100f;
    private static float maxThirst = 100f;

    public float health;
    public float hunger;
    public float thirst;

	public bool isAlive;

	private static float defaultWalkSpeed = 10f;
	private static float defaultJumpPower = 15f;

	public float walkSpeed;
	public float jumpPower;

	public bool isGrounded;

    public int selectedTile = 0;

    void Update()
    {
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1.01f), -Vector2.up, 0.1f);	

		if (hit.collider != null)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}

        if (isAlive)
        {
			// Walk Left
			if (Input.GetKeyDown("a") | Input.GetKey("a"))
			{
				sr.flipX = true;
				rb.velocity = new Vector2(-walkSpeed, rb.velocity.y);
			}
			else if (Input.GetKeyUp("a"))
			{
				rb.velocity = new Vector2(0, rb.velocity.y);
			}

			// Walk Right
			if (Input.GetKeyDown("d") | Input.GetKey("d"))
			{
				sr.flipX = false;
				rb.velocity = new Vector2(walkSpeed, rb.velocity.y);
			}
			else if (Input.GetKeyUp("d"))
			{
				rb.velocity = new Vector2(0, rb.velocity.y);
			}

			// Jumping
			if (Input.GetKeyDown("space") && isGrounded)
			{
				//rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
				rb.velocity = new Vector2(rb.velocity.x, jumpPower);
			}

			// Tile Painter
			if (Input.GetKeyDown("0"))
			{
				selectedTile = 0;
			}
			else if (Input.GetKeyDown("1"))
			{
				selectedTile = 2;
			}
			else if (Input.GetKeyDown("2"))
			{
				selectedTile = 3;
			}
			else if (Input.GetKeyDown("3"))
			{
				selectedTile = 4;
			}
			else if (Input.GetKeyDown("4"))
			{
				selectedTile = 5;
			}
			else if (Input.GetKeyDown("5"))
			{
				selectedTile = 6;
			}
			else if (Input.GetKeyDown("6"))
			{
				selectedTile = 7;
			}
			else if (Input.GetKeyDown("7"))
			{
				selectedTile = 8;
			}
			else if (Input.GetKeyDown("8"))
			{
				selectedTile = 9;
			}

			// Tile Painting
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			int mouseX = Mathf.FloorToInt(mousePos.x);
			int mouseY = Mathf.FloorToInt(mousePos.y);

			if (Input.GetMouseButtonDown(0))
			{
				terrainScript.SetTile(mouseX, mouseY, mouseY, selectedTile, 0);
			}
			else if (Input.GetMouseButtonDown(1))
			{
				terrainScript.SetTile(mouseX, mouseY, mouseY + 128, selectedTile, 0);
			}
        }
    }

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		terrainScript = terrain.GetComponent<Terrain>();
    }
}
