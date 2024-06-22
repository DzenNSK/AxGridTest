using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace SlotMachine
{
    [State("SMStateRunning")]
    public class SMStateRunning : FSMState
    {
        [Enter]
        private void Enter()
        {
            Debug.Log("Break awaiting");
            Settings.Model.Set("StopAllowed", true);
        }

        [Bind("StopButtonClicked")]
        private void Transition()
        {
            Parent.Change("SMStateDecelerate");
        }
    }
}
