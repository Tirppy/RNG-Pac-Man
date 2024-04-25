using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MatrixToGrid : MonoBehaviour
{
    public TileBase[] tiles; // Array of tiles for different tile types
    public Tilemap tilemap; // Reference to the Tilemap object

    public void GenerateGrid(int[,] grid)
    {
        // Clear all tiles from the Tilemap
        tilemap.ClearAllTiles();

        int width = grid.GetLength(0);
        int height = grid.GetLength(1);

        // Loop through the matrix
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Skip tiles with value -1
                if (grid[x, y] == -1)
                    continue;

                // Instantiate tile based on matrix value
                int tileValue = grid[x, y];
                if (tileValue >= 0 && tileValue < tiles.Length)
                {
                    TileBase tile = tiles[tileValue];
                    Vector3Int tilePosition = new Vector3Int(x, -y, 0); // Adjust Y position as needed
                    tilemap.SetTile(tilePosition, tile);
                }
                else
                {
                    Debug.LogWarning("Invalid matrix value: " + tileValue);
                }
            }
        }
    }
}