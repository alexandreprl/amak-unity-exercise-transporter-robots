using AMAK;
using TMPro;
using UnityEngine;

public class AgentsCountTextController : MonoBehaviour
{
    [SerializeField] private MultiAgentSystem multiAgentSystem;
    private TextMeshProUGUI _textComponent;

    private void Awake()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        multiAgentSystem.OnAgentsListUpdated += UpdateText;
    }

    private void UpdateText()
    {
        _textComponent.text = $"Agents count: {multiAgentSystem.Entities.Count}";
    }

    private void OnDisable()
    {
        multiAgentSystem.OnAgentsListUpdated -= UpdateText;
    }

    private void OnValidate()
    {
        if (multiAgentSystem == null)
        {
            Debug.LogError("MultiAgentSystem reference not set.", gameObject);
        }
    }
}
