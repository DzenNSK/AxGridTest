using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace SlotMachine
{
    [State("SMStateDecelerate")]
    public class SMStateDecelerate : FSMState
    {
        [Enter]
        private void Enter()
        {
            Debug.Log("Breaking");
            Settings.Invoke("StartBreaking");
        }

        [Bind("AlignSpeedReached")]
        private void Transition()
        {
            Parent.Change("SMStateAlignAwaiting");
        }
    }
}
