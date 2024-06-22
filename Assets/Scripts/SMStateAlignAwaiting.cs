using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace SlotMachine
{
    [State("SMStateAlignAwaiting")]
    public class SMStateAlignAwaiting : FSMState
    {
        [Enter]
        private void Enter()
        {
            Debug.Log("Align awaiting");
        }

        [Bind("SlotAligned")]
        private void Transition()
        {
            Settings.Invoke("WheelStop");
            Parent.Change("SMStateIdle");
        }
    }
}
