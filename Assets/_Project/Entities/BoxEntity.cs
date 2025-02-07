using AMAK.Entities;
using UnityEngine;

namespace Entities
{
	public class BoxEntity : PassiveEntity
	{
		[SerializeField] private GridMap gridMap;
		private Vector2Int _onGridPosition;

		public void SetPosition(Vector2Int onGridPosition)
		{
			_onGridPosition = onGridPosition;
			gridMap.Add(onGridPosition, this);
			transform.position = new Vector3(onGridPosition.x, onGridPosition.y);
		}

		public void Take(RobotAgent robotAgent)
		{
			gridMap.Remove(_onGridPosition, this);
			transform.parent = robotAgent.transform;
		}
	}
}