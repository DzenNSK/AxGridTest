using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

namespace SlotMachine
{
    [RequireComponent(typeof(AudioSource))]
    public class AlignClicker : MonoBehaviourExtBind
    {
        private AudioSource source;

        [OnStart]
        private void Init()
        {
            source = GetComponent<AudioSource>();
        }

        [Bind("SlotAligned")]
        private void PlaySound()
        {
            source?.Play();
        }
    }
}
