using UnityEngine;

public class InitialCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject startPanel;

    private void Start()
    {
        DisablePanelInfo();
    }

    public void EnablePanelInfo()
    {
        startPanel.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void DisablePanelInfo()
    {
        startPanel.SetActive(true);
        infoPanel.SetActive(false);
    }
}