using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlotMachine
{
    [RequireComponent(typeof(Button))]
    public class StopButton : MonoBehaviourExtBind
    {
        [SerializeField] private Color32 activeTextColor;
        [SerializeField] private Color32 inactiveTextColor;

        private Button button;
        private TMP_Text text;

        [OnStart]
        private void Subscribe()
        {
            button = GetComponent<Button>();
            text = GetComponentInChildren<TMP_Text>();
            button?.onClick.AddListener(OnClick);
            OnStateChange(Settings.Model.Get("StopAllowed", false));
        }

        private void OnClick()
        {
            Settings.Invoke("StopButtonClicked");
        }

        [Bind("OnStopAllowedChanged")]
        private void OnStateChange(bool state)
        {
            button.interactable = state;
            text.color = state ? activeTextColor : inactiveTextColor;
        }
    }
}
