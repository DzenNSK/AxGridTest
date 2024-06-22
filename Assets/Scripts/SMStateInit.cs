using AxGrid;
using AxGrid.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlotMachine
{
    [State("SMStateInit")]
    public class SMStateInit : FSMState
    {
        [Enter]
        private void Enter()
        {
            Settings.Model.Set("SpinAllowed", false);
            Settings.Model.Set("StopAllowed", false);
            Parent.Change("SMStateIdle");
        }
    }
}
