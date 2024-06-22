using UnityEngine;
using UnityEngine.UI;

namespace SlotMachine
{
    public class SlotItem : MonoBehaviour
    {
        [SerializeField] private Image iconImage;

        private SlotSequence.SlotDescriptor descriptor;

        public SlotSequence.SlotDescriptor SlotDescriptor => descriptor;

        public void SetType(SlotSequence.SlotDescriptor slotDescriptor)
        {
            if (iconImage == null) 
            { 
                Debug.LogError("[SlotItem] iconImage not set"); 
                return;
            }

            descriptor = slotDescriptor;
            iconImage.sprite = slotDescriptor.sprite;
        }
    }
}
