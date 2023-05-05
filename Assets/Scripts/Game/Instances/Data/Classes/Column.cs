using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column
{
	public TileData[] row = new TileData[256];
	public Dictionary<int, GameObject> tileObjects = new Dictionary<int, GameObject>();
}
