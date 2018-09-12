﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class WallController : MonoBehaviour 
{

	public enum WallType
	{
		Null = 0,
		Empty,
		CenterWall,
		LeftEdgeWall,
		RightEdgeWall,
		BottomEdgeWall,
		TopEdgeWall,
		TopLeftInsideWall,
		TopRightInsideWall,
		BottomLeftInsideWall,
		BottomRightInsideWall
	}
	private MapController mapController;

	private Tilemap tilemap;
	
	private WallType[,] tileSchematic;

	private int MapSize_x;
	private int MapSize_y;

	// Use this for initialization
	void Start () 
	{
		tilemap = GetComponent<Tilemap>();
		mapController = transform.parent.parent.gameObject.GetComponent<MapController>();

		MapSize_x = mapController.MapSize_x;
		MapSize_y = mapController.MapSize_y;

		tileSchematic = GenerateSchematic();
		FillTilemap(tileSchematic);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	WallType[,] GenerateSchematic()
	{
		WallType [,] schematic = new WallType[MapSize_x, MapSize_y];
		
		Random.InitState(42);

		// Fill edges
		for (int x = 0; x < MapSize_x; x++)
		{
			schematic[x, 0] = WallType.TopEdgeWall;
			schematic[x, MapSize_y - 1] = WallType.BottomEdgeWall;
		}
		for (int y = 0; y < MapSize_y; y++)
		{
			schematic[0, y] = WallType.RightEdgeWall;
			schematic[MapSize_x - 1, y] = WallType.LeftEdgeWall;
		}

		// Set corners
		schematic[0, 0] = WallType.TopRightInsideWall;
		schematic[MapSize_x - 1, 0] = WallType.TopLeftInsideWall;
		schematic[0, MapSize_y - 1] = WallType.BottomRightInsideWall;
		schematic[MapSize_x - 1, MapSize_y - 1] = WallType.BottomLeftInsideWall;

		// Add random rocks (example)
		// for(int x = 0; x < MapSize_x; x++)
		// {
		// 	for (int y = 0; y < MapSize_y; y++)
		// 	{
		// 		if(schematic[x, y] == WallType.Null)
		// 		{
		// 			if(Random.Range(0,100) < 5)
		// 			{
		// 				schematic[x, y] = WallType.CenterWall;
		// 			}
		// 		}
		// 	}
		// }

		return schematic;
	}

	void FillTilemap(WallType[,] schematic)
	{
		for(int x = 0; x < schematic.GetLength(0); x++)
		{
			for (int y = 0; y < schematic.GetLength(1); y++)
			{
				switch (schematic[x,y])
				{
					case WallType.CenterWall:
					{
						PaintTile(x, y, mapController.MiddleTile);
						break;
					}
					case WallType.LeftEdgeWall:
					{
						PaintTile(x, y, mapController.LeftEdgeTile);
						break;
					}
					case WallType.TopEdgeWall:
					{
						PaintTile(x, y, mapController.TopEdgeTile);
						break;
					}
					case WallType.RightEdgeWall:
					{
						PaintTile(x, y, mapController.RightEdgeTile);
						break;
					}
					case WallType.BottomEdgeWall:
					{
						PaintTile(x, y, mapController.BottomEdgeTile);
						break;
					}
					case WallType.TopLeftInsideWall:
					{
						PaintTile(x, y, mapController.TopLeftInsideTile);
						break;
					}
					case WallType.TopRightInsideWall:
					{
						PaintTile(x, y, mapController.TopRightInsideTile);
						break;
					}
					case WallType.BottomLeftInsideWall:
					{
						PaintTile(x, y, mapController.BottomLeftInsideTile);
						break;
					}
					case WallType.BottomRightInsideWall:
					{
						PaintTile(x, y, mapController.BottomRightInsideTile);
						break;
					}
					case WallType.Null:
					{
						// This space intentionally left blank (wasn't filled during map generation)
						break;
					}
					default:
					{
						Debug.Log(string.Format("Un-paintable tile[{0}] at X:{1}, Y:{2}.", System.Enum.GetName(typeof(WallType), schematic[x, y]), x, y));
						break;
					}
				}
			}
		}
	}

	void PaintTile(int x, int y, TileBase tile)
	{
		tilemap.SetTile(mapController.MapOrigin + new Vector3Int(x, y, 0), tile);
	}
}
