using Entities;
using UnityEngine;

namespace Map
{
	public class MapLoader : MonoBehaviour
	{
		[SerializeField] private GridMap gridMap;
		[SerializeField] private BoxEntity boxPrefab;
		[SerializeField] private WallEntity wallEntity;
		[SerializeField] private GameObject dropAreaPrefab;

		public enum WallType
		{
			TwoCorridors,
			SnakePath
		}

		[SerializeField] private WallType wallType = WallType.TwoCorridors;

		private void Awake()
		{
			gridMap.Init();

			for (int x = 0; x < 3; x++)
			{
				for (int y = GridMap.Height / 3; y < 2 * GridMap.Height / 3; y++)
					Instantiate(boxPrefab).SetPosition(new Vector2Int(x, y));
			}

			for (int x = 0; x < GridMap.Width; x++)
			{
				for (int y = 0; y < GridMap.Height; y++)
				{
					if (gridMap.IsDropArea(new Vector2Int(x, y)))
						Instantiate(dropAreaPrefab, new Vector3(x, y), Quaternion.identity);
				}
			}

			switch (wallType)
			{
				case WallType.TwoCorridors:
					for (int x = 2 * GridMap.Width / 5; x < 3 * GridMap.Width / 5; x++)
					{
						for (int y = 1; y < GridMap.Height - 1; y++)
							Instantiate(wallEntity).SetPosition(new Vector2Int(x, y));
					}

					break;
				case WallType.SnakePath:
					
					for (int x = 2 * GridMap.Width / 5; x < 3 * GridMap.Width / 5; x++)
					{
						for (int y = 1; y < GridMap.Height - 1; y++)
						{
							if (x<GridMap.Width/2+1 && y==GridMap.Height/2 || x>=GridMap.Width/2 && y==GridMap.Height/2-1)
								continue;
							Instantiate(wallEntity).SetPosition(new Vector2Int(x, y));
						}
					}
					break;
			}
		}
	}
}