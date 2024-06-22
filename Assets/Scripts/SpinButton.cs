using AxGrid;
using AxGrid.Base;
using AxGrid.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SlotMachine
{
    [RequireComponent(typeof(Button))]
    public class SpinButton : MonoBehaviourExtBind
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
            OnSpinAllowedChanged(Settings.Model.Get("SpinAllowed", false));
        }

        private void OnClick()
        {
            Settings.Invoke("SpinButtonClicked");
        }

        [Bind("OnSpinAllowedChanged")]
        private void OnSpinAllowedChanged(bool state)
        {
            button.interactable = state;
            text.color = state ? activeTextColor : inactiveTextColor;
        }
    }
}
