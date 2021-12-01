using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH {
    public class TileMap : MonoBehaviour
    {

        private int Height = 8;
        private int Width = 8;
        private int cellSize = 6;

        public void Create_TileMap(GameObject tile_prefab, Transform parent)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {

                    GameObject newTile = Instantiate(tile_prefab, new Vector3(x * cellSize, 0, y * cellSize), tile_prefab.transform.rotation);
                    newTile.transform.SetParent(parent);
                }
            }
        }
    }
}

