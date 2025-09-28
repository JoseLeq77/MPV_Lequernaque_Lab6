using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPManager : MonoBehaviour
{
    [Header("HP Settings")]
    [SerializeField] private int maxLife = 3;
    [SerializeField] private int currentLife;

    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Text hpText;

    private int _recievedHpVariation = 0;

    #region Unity Methods

    private void Awake()
    {
        currentLife = maxLife;
    }

    private void OnEnable()
    {
        HealthModifier.OnPlayerHealed += ApplyLifeChange;
        HealthModifier.OnPlayerDamaged += ApplyLifeChange;
    }

    private void OnDisable()
    {
        HealthModifier.OnPlayerHealed -= ApplyLifeChange;
        HealthModifier.OnPlayerDamaged -= ApplyLifeChange;
    }

    #endregion

    #region Custom Methods

    private void ApplyLifeChange()
    {
        currentLife += _recievedHpVariation;
        _recievedHpVariation = 0;

        if (currentLife > maxLife)
        {
            currentLife = maxLife;
        }
        if (currentLife < 0)
        {
            currentLife = 0;
        }

        UpdateHPText();

        if (currentLife <= 0)
        {
            GameManager.TriggerOnPlayerLose();
        }
    }

    private void UpdateHPText()
    {
        if (hpText != null)
        {
            hpText.text = "HP: " + currentLife;
        }
    }

    public int GetCurrentLife()
    {
        return currentLife;
    }

    public int GetMaxLife()
    {
        return maxLife;
    }

    public void RecieveHpVariation(int hpVariation)
    {
        _recievedHpVariation = hpVariation;
    }

    #endregion

}
