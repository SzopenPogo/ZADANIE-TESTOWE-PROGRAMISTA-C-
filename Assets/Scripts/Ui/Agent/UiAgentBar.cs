using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiAgentBar : MonoBehaviour
{
    public static UiAgentBar Instance;

    [field: Header("Containers")]
    [field: SerializeField] private GameObject agentDataContainer;

    [field: Header("Agent Data")]
    [field: SerializeField] private Image icon;
    [field: SerializeField] private TextMeshProUGUI agentName;

    [field: Header("Agent Healthbar")]
    [field: SerializeField] private Slider healthSlider;
    [field: SerializeField] private TextMeshProUGUI healthText;

    private Health currentAgentHealth;
    private Agent currentAgent;

    private void Awake() => Instance = this;

    private void OnDisable() => ResetAgent();
    private void OnDestroy() => ResetAgent();

    private void ResetAgent()
    {
        if (currentAgentHealth != null)
        {
            currentAgentHealth.OnHealthChanged -= UpdateBar;
            currentAgentHealth = null;
        }

        if (currentAgent != null)
            currentAgent = null;
    }

    private void SetHealthBar()
    {
        healthSlider.maxValue = currentAgentHealth.MaxHealth;
        healthSlider.value = currentAgentHealth.CurrentHealth;
        healthText.text = $"{currentAgentHealth.CurrentHealth} / {currentAgentHealth.MaxHealth}";
    }

    public void InitializeBar(Health health, Agent agent)
    {
        if(!agentDataContainer.activeSelf)
            agentDataContainer.SetActive(true);

        if (currentAgentHealth != null || currentAgent != null)
            ResetAgent();

        //Init HealthBar
        currentAgentHealth = health;
        SetHealthBar();
        health.OnHealthChanged += UpdateBar;

        //Init data
        icon.sprite = agent.Icon;
        agentName.text = agent.AgentName;
        currentAgent = agent;
    }

    public void DisableBar()
    {
        ResetAgent();
        agentDataContainer.SetActive(false);
    }

    public void UpdateBar()
    {
        SetHealthBar();
    }
}
