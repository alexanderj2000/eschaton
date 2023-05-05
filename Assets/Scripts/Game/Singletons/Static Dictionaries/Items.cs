using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the data structure for a type of item
public struct Item
{
	public string name;

	public Item(string _name)
	{
		name = _name;
	}
}

// Defines all the items in the game
public static class Items
{
	public static Item[] item = {
		//new Item("abc", "item")
	};
}