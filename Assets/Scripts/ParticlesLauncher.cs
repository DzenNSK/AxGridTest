using AxGrid.Base;
using AxGrid.Model;
using Coffee.UIExtensions;
using UnityEngine;

namespace SlotMachine
{
    [RequireComponent(typeof(UIParticle))]
    public class ParticlesLauncher : MonoBehaviourExtBind
    {
        private UIParticle particleSystem;

        [OnAwake]
        private void Init()
        {
            particleSystem = GetComponent<UIParticle>();
        }

        [Bind("SpinFinished")]
        private void OnSpinFinished(SlotItem result)
        {
            particleSystem.Play();
        }
    }
}
