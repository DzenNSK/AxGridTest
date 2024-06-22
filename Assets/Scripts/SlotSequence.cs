using System;
using UnityEngine;

namespace SlotMachine
{
    [CreateAssetMenu(fileName = "SlotSequence", menuName = "Slots/Slot sequence")]
    public class SlotSequence : ScriptableObject
    {
        [Serializable]
        public struct SlotDescriptor
        {
            public Sprite sprite;
            public int prize;
        }

        [SerializeField] private SlotDescriptor[] slots;

        private int index = 0;

        public void Reset()
        {
            index = 0;
        }

        public SlotDescriptor GetNext()
        {
            index++;
            if (index >= slots.Length) index = 0;

            return slots[index];
        }
    }
}
