using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
	// CAMERA
	private GameObject cameraObject; // The main camera

	public int cameraX; // X coord of the camera in tiles
	public int cameraY; // Y coord of the camera in tiles

	public int chunkX; // Chunk the camera is in

	private static int renderRadiusX = 24; // Area tiles are rendered from the camera horizontally
	private static int renderRadiusY = 12; // Area tiles are rendered from the camera vertically

	public GameObject player; // The player character

	// TILES
	public Dictionary<int, Column> columns = new Dictionary<int, Column>(); // Loaded tiles
	public Dictionary<int, Chunk> chunks = new Dictionary<int, Chunk>(); // Loaded chunks

	// WORLD GENERATION
	private static float seed = 0.123f; // World generation seed
    private static int groundLevel = 64; // Average height of the terrain equidistant from the minimum and maximum
    private static int groundDepth = 4; // Depth of the ground layer	

	// Method to check is tile exists at location
	bool IsTileSolid(int x, int i)
	{
		return Tiles.tileType[columns[x].row[i].tileId].isSolid;
	}

	// Method to unrender tile (remove tile gameobject)
	void UnrenderTile(int x, int y)
	{
		if (columns[x].tileObjects.ContainsKey(y))
		{
			Destroy(columns[x].tileObjects[y]);
		}
		if (columns[x].tileObjects.ContainsKey(y + 128))
		{
			Destroy(columns[x].tileObjects[y + 128]);
		}
	}

	int GetTransition(int x, int y, int i)
	{
		int transition = 0; // Spritesheet sprite number

		// Check top side
		bool top = false;
		if (y != 127)
		{
			top = IsTileSolid(x, i + 1);
		}
		else
		{
			top = false;
		}

		// Check right side
		bool right = IsTileSolid(x + 1, i);

		// Check bottom side
		bool bottom = false;
		if (y != 0)
		{
			bottom = IsTileSolid(x, i - 1);
		}
		else
		{
			bottom = true;
		}

		// Check left side
		bool left = IsTileSolid(x - 1, i);
		
		// Get transition from confgiruation of sides
		if (top)
		{
			if (right)
			{
				if (bottom)
				{
					if (left) // Top, Right, Bottom, Left
					{
						transition = 9;
					}
					else // Top, Right, Left
					{
						transition = 8;
					}
				}
				else 
				{
					if (left) // Top, Right, Left
					{
						transition = 17;
					}
					else // Top, Right
					{
						transition = 16;
					}
				}
			}
			else
			{
				if (bottom)
				{
					if (left) // Top, Bottom, Left
					{
						transition = 10;
					}
					else // Top, Bottom
					{
						transition = 24;
					}
				}
				else
				{
					if (left) // Top, Left
					{
						transition = 18;
					}
					else // Top
					{
						transition = 19;
					}
				}
			}
		}
		else
		{
			if (right)
			{
				if (bottom)
				{
					if (left) // Right, Bottom, Left
					{
						transition = 1;
					}
					else // Right, Bottom
					{
						transition = 0;
					}
				}
				else
				{
					if (left) // Right, Left
					{
						transition = 25;
					}
					else // Right
					{
						transition = 11;
					}
				}
			}
			else
			{
				if (bottom)
				{
					if (left) // Bottom, Left
					{
						transition = 2;
					}
					else // Bottom
					{
						transition = 3;
					}
				}
				else
				{
					if (left) // Left
					{
						transition = 26;
					}
					else // None
					{
						transition = 27;
					}
				}
			}
		}

		return transition;
	}

	GameObject CreateTile(int x, int y, int i, int transition)
	{
		GameObject tileObject = new GameObject();

		TileData tile = columns[x].row[i];
		Tile tileType = Tiles.tileType[tile.tileId];
		SpriteRenderer tileSprite = tileObject.AddComponent<SpriteRenderer>();

		if (y == i)
		{
			tileSprite.sortingLayerName = "Foreground";
			
			if (transition != 9 && tileType.isSolid)
			{
				BoxCollider2D collider = tileObject.AddComponent<BoxCollider2D>();
				collider.size = new Vector2(0.99f, 1f);
			}
		}
		else
		{
			transition += 32;
			tileSprite.sortingLayerName = "Background"; 
		}

		if (tileType.isSolid == false)
		{
			// Override
			transition = tile.tileValue;
		}

		tileSprite.sprite = Tiles.tileType[tile.tileId].sprites[transition];

		tileObject.transform.position = new Vector3((float)x + 0.5f, (float)y + 0.5f, 0);

		return tileObject;
	}

	// Method to render a tile (create tile gameobject)
	void RenderTile(int x, int y)
	{	
		TileData foregroundTile = columns[x].row[y];
		TileData backgroundTile = columns[x].row[y + 128];

		bool renderBackground = false;

		if (foregroundTile.tileId != 0)
		{
			 int transition = GetTransition(x, y, y);

			 if (transition != 9 | Tiles.tileType[foregroundTile.tileId].isOpaque == false)
			 {
				  renderBackground = true;
			 }

			 columns[x].tileObjects[y] = CreateTile(x, y, y, transition);
		}
		else
		{
			renderBackground = true;
		}

		if (renderBackground && columns[x].row[y + 128].tileId != 0)
		{
			columns[x].tileObjects[y + 128] = CreateTile(x, y, y + 128, GetTransition(x, y, y + 128) );
		}
	}

	public void SetTile(int x, int y, int i, int id, int value)
	{
		columns[x].row[i].tileId = id;
		columns[x].row[i].tileValue = value;

		UnrenderTile(x, y);
		RenderTile(x, y);
		
		if (y != 127)
		{
			UnrenderTile(x, y + 1);
			RenderTile(x, y + 1);
		}

		if (y != 0)
		{
			UnrenderTile(x, y - 1);
			RenderTile(x, y - 1);
		}
		UnrenderTile(x + 1, y);
		RenderTile(x + 1, y);
		UnrenderTile(x - 1, y);
		RenderTile(x - 1, y);
	}

    void UnrenderArea(int startX, int endX, int startY, int endY)
    {
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                UnrenderTile(x, y);
            }
        }
    }

    void RenderArea(int startX, int endX, int startY, int endY)
    {
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                RenderTile(x, y);
            }
        }
    }

	void SaveChunk(int chunk)
	{

	}

	void UnloadChunk(int chunk)
	{
		chunks[chunk] = null;

		int x = chunk * 128;

		for (int i = 0; i <= 127; i++)
        {
			int xi = x + i;
            columns[xi] = null;
        }
	}

	void LoadChunk(int chunk)
	{
		
	}

	void GenerateChunk(int chunk)
	{
		int x = chunk * 128;

		// Iterate through each column
		for (int i = 0; i <= 127; i++)
        {
            int xi = x + i;
            float xf = (float)xi;

			// Generate height value through Perlin noise
            int height = groundLevel + Mathf.FloorToInt(Mathf.PerlinNoise(xf / 32f, seed) * 16f);

			// Create a new column to hold data 
            Column column = new Column();

			// Fill in underground tiles
            for (int y = 0; y < height - groundDepth; y++)
            {
                column.row[y].tileId = 5; 
                column.row[y + 128].tileId = 5;
            }

			// Fill in ground tiles
            for (int y = height - groundDepth; y < height; y++)
            {
                column.row[y].tileId = 2; // Grassy Dirt
                column.row[y + 128].tileId = 2;
            }

			// Fill in overground tiles
            for (int y = height; y < 128; y++)
            {
                column.row[y].tileId = 0;
                column.row[y + 128].tileId = 0;
            }

            columns[xi] = column;
        }
	}

	void NewChunk(int chunk)
	{
        bool hasData = false;

        if (hasData)
        {
			LoadChunk(chunk);
        }
        else
        {
            GenerateChunk(chunk);
        }
	}

	public void AutoSave()
	{
		foreach (KeyValuePair<int, Chunk> chunk in chunks)
		{
			SaveChunk(chunk.Key);
		}
	}

    void Update()
    {
		// Track camera tile position and current chunk 
		int newcameraX = Mathf.FloorToInt(cameraObject.transform.position.x);
		int newcameraY = Mathf.FloorToInt(cameraObject.transform.position.y);
		int newChunkX = Mathf.FloorToInt(cameraObject.transform.position.x/128f);

		// If the player has entered a different chunk, create a new chunk one ahead and remove the chunk one behind the previous chunk
        if (newChunkX != chunkX)
        {
            NewChunk(newChunkX + (newChunkX - chunkX));
			UnloadChunk(chunkX - (newChunkX - chunkX));

            chunkX = newChunkX;
        }

		// If camera has moved horizontally, render line in camera direction and unrender line in the opposite direction
        if (newcameraX != cameraX)
        {
            int offsetX = (int)Mathf.Sign(newcameraX - cameraX);

            RenderArea(newcameraX + (renderRadiusX * offsetX), newcameraX + (renderRadiusX * offsetX), newcameraY - renderRadiusY, newcameraY + renderRadiusY);
            UnrenderArea(cameraX - (renderRadiusX * offsetX), cameraX - (renderRadiusX * offsetX), cameraY - renderRadiusY, cameraY + renderRadiusY);

            cameraX = newcameraX;
        }

		// If camera has moved vertically, render line in camera direction and unrender line in the opposite direction
        if (newcameraY != cameraY)
        {
            int offsetY = (int)Mathf.Sign(newcameraY - cameraY);

            RenderArea(newcameraX - renderRadiusX, newcameraX + renderRadiusX, newcameraY + (renderRadiusY * offsetY), newcameraY + (renderRadiusY * offsetY));
            UnrenderArea(cameraX - renderRadiusX, cameraX + renderRadiusX, cameraY - (renderRadiusY * offsetY), cameraY - (renderRadiusY * offsetY));

            cameraY = newcameraY;
        }
    }

    void Start()
    {
		// Set the chunk the cameraObject is in and their tile position
        chunkX = Mathf.FloorToInt(cameraObject.transform.position.x / 128f);
        cameraX = Mathf.FloorToInt(cameraObject.transform.position.x);
        cameraY = Mathf.FloorToInt(cameraObject.transform.position.y);

		// Create a new chunk at spawn
		NewChunk(chunkX - 1);
        NewChunk(chunkX);
		NewChunk(chunkX + 1);
    }

	void Awake()
	{
		// Cache the camera
		cameraObject = Camera.main.gameObject;
	}
}
