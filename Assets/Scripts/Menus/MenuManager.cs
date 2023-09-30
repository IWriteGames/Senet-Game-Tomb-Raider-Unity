using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Awake()
    {
        MenuDefault();
    }

    public void OpenCredits()
    {
        buttonsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void MenuDefault()
    {
        buttonsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }


}
