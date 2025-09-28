using Mathematics.Week6;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthModifier : MonoBehaviour
{
    #region Variables   

    [Header("Health Modifier Settings")]
    [SerializeField] private int hpVariation;
    [SerializeField] private bool isInstakillObject = false;

    [Header("References")]
    [SerializeField] private HPManager hpManager;

    private ObstacleController _obstacleController;

    #endregion

    #region Events

    public static event Action OnPlayerHealed;
    public static event Action OnPlayerDamaged;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        _obstacleController = GetComponent<ObstacleController>();
        if (hpManager == null)
        {
            hpManager = FindFirstObjectByType<HPManager>();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            ManageHealthChange();
            if (_obstacleController != null)
            {
                Destroy(gameObject);
            }
        }
    }

    #endregion

    #region Custom Methods

    public void ManageHealthChange()
    {
        if (isInstakillObject)
        {
            hpVariation = -100;
            hpManager.RecieveHpVariation(hpVariation);
            OnPlayerDamaged?.Invoke();
        }
        else
        {
            if (hpVariation == 0)
            {
                return;
            }
            else if (hpVariation > 0)
            {
                hpManager.RecieveHpVariation(hpVariation);
                OnPlayerHealed?.Invoke();
            }
            else if (hpVariation < 0)
            {
                if (_obstacleController != null)
                {
                    hpManager.RecieveHpVariation(hpVariation);
                    OnPlayerDamaged?.Invoke();
                }
                else
                {
                    return;
                }
            }
        }
    }

    #endregion
}
