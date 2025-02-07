using UnityEngine;
using UnityEngine.UI;

public class ExecutionSpeedController : MonoBehaviour
{
	private Slider _slider;
	[SerializeField] private Scheduler scheduler;

	private void Awake()
	{
		_slider = GetComponent<Slider>();
	}

	private void OnEnable()
	{
		_slider.onValueChanged.AddListener(OnSliderValueChanged);
		OnSliderValueChanged(_slider.value);
	}

	private void OnDisable()
	{
		_slider.onValueChanged.RemoveListener(OnSliderValueChanged);
	}

	private void OnSliderValueChanged(float newValue)
	{
		scheduler.CyclesPerSecond = newValue;
	}
}
