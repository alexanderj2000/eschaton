using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the data structure for a type of mob
public class Mob
{
	public string name;
	public Sprite[] sprites;

	public Mob(string _name, string _sprites)
	{
		name = _name;
		sprites = Resources.LoadAll<Sprite>(_sprites);
	}
}

// Defines all the mobs in the game
public static class Mobs
{
	public static Mob[] mob = {
		//new Mob("abc", "item")
	};
}