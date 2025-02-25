using UnityEngine;

public class InitialCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;

    private void Start()
    {
        DisablePanelInfo();
    }

    public void EnablePanelInfo()
    {
        startPanel.SetActive(false);
    }

    public void DisablePanelInfo()
    {
        startPanel.SetActive(true);
    }
}