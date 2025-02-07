using System;
using AMAK.Entities;
using Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class RobotAgent : Agent
{
	[SerializeField] private GridMap gridMap;
	private Vector2Int _onGridPosition;
	public Vector2Int StartPosition;

	#region Perceptions

	private bool _isOverABox;

	#endregion

	private BoxEntity _heldBox;
	private bool _isOverADropArea;
	private Vector2 _targetPosition;
	private bool _isMoving;


	public override void Setup()
	{
		SetPosition(StartPosition);
	}

	private void SetPosition(Vector2Int onGridPosition)
	{
		gridMap.Remove(_onGridPosition, this);
		_isMoving = true;
		_targetPosition = new Vector3(onGridPosition.x, onGridPosition.y);
	}

	public override void Cleanup()
	{
		gridMap.Remove(_onGridPosition, this);
	}

	public override void OnPerceive()
	{
		if (_isMoving) return;
		_isOverABox = gridMap.ContainsBox(_onGridPosition);
		_isOverADropArea = gridMap.IsDropArea(_onGridPosition);
	}

	public override void OnDecideAndAct()
	{
		if (_isMoving)
		{
			if (Vector2.Distance(transform.position, _targetPosition) < 0.1f)
			{
				_isMoving = false;
				_onGridPosition = new Vector2Int((int) _targetPosition.x, (int) _targetPosition.y);
				gridMap.Add(_onGridPosition, this);
			}
			else
			{
				var direction = (_targetPosition - (Vector2) transform.position).normalized;
				transform.position += (Vector3) direction * 0.2f;
			}
		}
		else
		{
			if (_isOverABox && _heldBox == null)
			{
				TakeBox();
			}
			else if (_isOverADropArea && _heldBox != null)
			{
				Drop();
			}
			else
			{
				var newPosition = GetRandomPosition(_onGridPosition);
				if (gridMap.IsValid(newPosition) && !gridMap.ContainsRobotAgent(newPosition) &&
				    !gridMap.ContainsWall(newPosition))
					SetPosition(newPosition);
			}
		}
	}

	private void Drop()
	{
		if (_heldBox == null) return;
		Destroy(_heldBox.gameObject);
		_heldBox = null;
	}

	private void TakeBox()
	{
		if (_heldBox != null) return;
		var box = gridMap.GetBox(_onGridPosition);
		if (box == null) return;
		box.Take(this);
		_heldBox = box;
	}

	private Vector2Int GetRandomPosition(Vector2Int position)
	{
		var random = Random.Range(0, 4);
		switch (random)
		{
			case 0:
				return new Vector2Int(position.x, position.y + 1);
			case 1:
				return new Vector2Int(position.x, position.y - 1);
			case 2:
				return new Vector2Int(position.x + 1, position.y);
			case 3:
				return new Vector2Int(position.x - 1, position.y);
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}