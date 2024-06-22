using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace SlotMachine
{
    [State("SMStateIdle")]
    public class SMStateIdle : FSMState
    {
        [Enter]
        private void Enter()
        {
            Debug.Log("Idle");
            Settings.Model.Set("SpinAllowed", true);
            Settings.Model.Set("StopAllowed", false);
        }

        [Bind("SpinButtonClicked")]
        private void Transition()
        {
            Parent.Change("SMStateAccelerate");
        }
    }
}
