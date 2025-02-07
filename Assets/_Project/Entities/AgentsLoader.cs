
using AMAK;
using UnityEngine;

namespace Entities
{
	public class AgentsLoader : MonoBehaviour
	{
		[SerializeField] private RobotAgent robotAgentPrefab;
		[SerializeField] private MultiAgentSystem multiAgentSystem;

		void Start()
		{
			for (int i = 0; i < 10; i++)
			{
				var robotAgent = Instantiate(robotAgentPrefab);
				robotAgent.StartPosition = new Vector2Int(i, 0);
				robotAgent.EntitiesContainer = multiAgentSystem;
			}
		}
	}
}