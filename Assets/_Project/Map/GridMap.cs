using System.Collections.Generic;
using System.Linq;
using AMAK.Entities;
using Entities;
using UnityEngine;

[CreateAssetMenu(fileName = "GridMap", menuName = "Scriptable Objects/GridMap")]
public class GridMap : ScriptableObject
{
	public const int Width = 32;
	public const int Height = 18;
	private List<Entity>[][] _entitiesGrid;

	public void Init()
	{
		_entitiesGrid = new List<Entity>[Height][];
		for (int i = 0; i < _entitiesGrid.Length; i++)
		{
			_entitiesGrid[i] = new List<Entity>[Width];
			for (int j = 0; j < _entitiesGrid[i].Length; j++)
			{
				_entitiesGrid[i][j] = new List<Entity>();
			}
		}
	}

	public void Add(Vector2Int onGridPosition, Entity entity)
	{
		_entitiesGrid[onGridPosition.y][onGridPosition.x].Add(entity);
	}

	public void Remove(Vector2Int onGridPosition, Entity entity)
	{
		if (_entitiesGrid[onGridPosition.y][onGridPosition.x].Contains(entity))
			_entitiesGrid[onGridPosition.y][onGridPosition.x].Remove(entity);
	}

	public bool IsEmpty(Vector2Int onGridPosition)
	{
		return _entitiesGrid[onGridPosition.y][onGridPosition.x] == null;
	}

	public bool IsValid(Vector2Int onGridPosition)
	{
		return onGridPosition.x is >= 0 and < Width && onGridPosition.y is >= 0 and < Height;
	}

	public bool ContainsRobotAgent(Vector2Int position)
	{
		return IsValid(position) && _entitiesGrid[position.y][position.x].Any(entity => entity is RobotAgent);
	}

	public bool ContainsBox(Vector2Int onGridPosition)
	{
		return IsValid(onGridPosition) &&
		       _entitiesGrid[onGridPosition.y][onGridPosition.x].Any(entity => entity is BoxEntity);
	}
	
	public bool ContainsWall(Vector2Int onGridPosition)
	{
		return IsValid(onGridPosition) &&
		       _entitiesGrid[onGridPosition.y][onGridPosition.x].Any(entity => entity is WallEntity);
	}

	public BoxEntity GetBox(Vector2Int onGridPosition)
	{
		var box = _entitiesGrid[onGridPosition.y][onGridPosition.x].FirstOrDefault(entity => entity is BoxEntity);
		_entitiesGrid[onGridPosition.y][onGridPosition.x].Remove(box);
		return (BoxEntity) box;
	}

	public bool IsDropArea(Vector2Int onGridPosition)
	{
		return onGridPosition.x is >= Width - 3 and < Width &&
		       onGridPosition.y is >= Height / 3 and < 2 * Height / 3;
	}
}