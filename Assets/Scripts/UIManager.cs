using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIWindow> _uiWindows;
    public List<UIWindow> UIWindows => _uiWindows;

    public static UIManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ShowWindow(string windowName)
    {
        bool found = false;

        foreach (var window in _uiWindows)
        {
            if (window.Id == windowName)
            {
                Debug.Log($"Showing window: {windowName}");
                window.Show();
                found = true;
            }
            else
            {
                window.Hide(); // Oculta las demás para que no se encimen
            }
        }

        if (!found)
        {
            Debug.LogError("Window not found: " + windowName);
        }
    }

    public void HideWindow(string windowName)
    {
        foreach (var window in _uiWindows)
        {
            if (window.Id == windowName)
            {
                Debug.Log($"Hiding window: {windowName}");
                window.Hide();
                break;
            }
        }
    }

    // Métodos rápidos opcionales para conectar directamente en los botones sin escribir texto:
    public void OpenSettings() => ShowWindow("settings");
    public void OpenCredits() => ShowWindow("creditos");
    public void OpenMenu() => ShowWindow("menu");
}