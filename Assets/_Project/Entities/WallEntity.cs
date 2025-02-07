using AMAK.Entities;
using UnityEngine;

namespace Entities
{
	public class WallEntity : PassiveEntity
	{
		[SerializeField] private GridMap gridMap;


		public void SetPosition(Vector2Int onGridPosition)
		{
			gridMap.Add(onGridPosition, this);
			transform.position = new Vector3(onGridPosition.x, onGridPosition.y);
		}
	}
}