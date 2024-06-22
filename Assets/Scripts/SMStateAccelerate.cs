using AxGrid;
using AxGrid.FSM;
using UnityEngine;

namespace SlotMachine
{
    [State("SMStateAccelerate")]
    public class SMStateAccelerate : FSMState
    {
        [Enter]
        private void Enter()
        {
            Debug.Log("Acceleration");
            Settings.Model.Set("SpinAllowed", false);
            Settings.Invoke("StartRunning");
        }

        //Есть ли вариант реализации без константы, параметризуемый?!
        [One(3f)]
        private void Transition()
        {
            Parent.Change("SMStateRunning");
        }
    }
}
