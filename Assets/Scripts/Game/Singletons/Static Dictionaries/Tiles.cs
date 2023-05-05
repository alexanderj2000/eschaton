using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the data structure for a type of tile
public class Tile
{
	public string name;
	public Sprite[] sprites;
	public bool isOpaque;
	public bool isSolid;

	public Tile(string _name, string _sprites, bool _isOpaque, bool _isSolid)
	{
		name = _name;
		sprites = Resources.LoadAll<Sprite>(_sprites);
		isOpaque = _isOpaque;
		isSolid = _isSolid;
	}
}

// Defines all the tiles in the game
public static class Tiles
{
	public static Tile[] tileType = new Tile[]{
		new Tile("Air", "Art/Tilesets/tileset_grass", false, false),
		new Tile("Water", "Art/Tilesets/tileset_water", false, false),
		new Tile("GrassyDirt", "Art/Tilesets/tileset_grass", true, true),
		new Tile("Dirt", "Art/Tilesets/tileset_dirt", true, true),
		new Tile("Sand", "Art/Tilesets/tileset_sand", true, true),
		new Tile("Stone", "Art/Tilesets/tileset_stone", true, true),
		new Tile("Snow", "Art/Tilesets/tileset_snow", true, true),
		new Tile("Wood", "Art/Tilesets/tileset_wood", true, true),
		new Tile("Wooden Planks", "Art/Tilesets/tileset_planks", true, true),
		new Tile("Leaves", "Art/Tilesets/tileset_leaves", true, true)
	};
}