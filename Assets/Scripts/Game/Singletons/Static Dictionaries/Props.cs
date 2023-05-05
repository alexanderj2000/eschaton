using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the data structure for a type of prop
public struct Prop
{
	public string name;
	public Sprite[] sprites;

	public Prop(string _name, string _sprites)
	{
		name = _name;
		sprites = Resources.LoadAll<Sprite>(_sprites);
	}
}

// Defines all the props in the game
public static class Props
{
	public static Prop[] prop = {
		//new Prop("abc", "item")
	};
}