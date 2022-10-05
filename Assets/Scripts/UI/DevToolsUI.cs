using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevToolsUI : MonoBehaviour
{
    public bool DevToolsOpened { get; private set; }

    [SerializeField]
    private GameObject _DevToolsPanel;

    private void Awake()
    {
        if (_DevToolsPanel.activeSelf)
        {
            DevToolsOpened = true;
        }
        else
        {
            DevToolsOpened = false;
        }
    }

    public void ToggleDevToolsPanel()
    {
        if (DevToolsOpened)
        {
            _DevToolsPanel.SetActive(false);
        }
        else
        {
            _DevToolsPanel.SetActive(true);
        }
        DevToolsOpened = !DevToolsOpened;
    }
}
