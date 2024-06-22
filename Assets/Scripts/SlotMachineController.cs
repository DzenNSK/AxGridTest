using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using System;
using UnityEngine;

namespace SlotMachine
{
    public class SlotMachineController : MonoBehaviourExtBind
    {
        [SerializeField]
        [Tooltip("Acceleration for running start")]
        private float runAcceleration;

        [SerializeField]
        [Tooltip("Acceleration for stop wheel")]
        private float stopAcceleration;

        [SerializeField]
        [Tooltip("Max (working) wheel velocity")]
        private float maxVelocity;

        [SerializeField]
        [Tooltip("Velocity for align before stop")]
        private float alignVelocity;

        [SerializeField]
        private float alignTolerance;

        [SerializeField]
        private SlotSequence sequenceData;

        [SerializeField]
        [Tooltip("Offset between slots")]
        private float slotsOffset;

        [SerializeField]
        private uint additionalSlots;

        [SerializeField]
        [Tooltip("Segment prefab")]
        private SlotItem itemPrefab;

        [SerializeField]
        private RectTransform maskTransform;

        private float runningVelocity;
        private SlotItem[] items;
        private SlotItem alignedSlot;
        private float flipPosition;
        private Action updateAction;
        [SerializeField] private RectTransform slot;

        [OnAwake]
        private void CreateFSM()
        {
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new SMStateInit());
            Settings.Fsm.Add(new SMStateIdle());
            Settings.Fsm.Add(new SMStateAccelerate());
            Settings.Fsm.Add(new SMStateRunning());
            Settings.Fsm.Add(new SMStateDecelerate());
            Settings.Fsm.Add(new SMStateAlignAwaiting());
        }

        [OnStart]
        private void Init()
        {
            PopulateItems();
            Settings.Fsm.Start("SMStateInit");
        }

        private void PopulateItems()
        {
            if(sequenceData == null)
            {
                Debug.LogError("[SlotMachineController] No sequence data");
                return;
            }
            sequenceData.Reset();

            if(itemPrefab == null)
            {
                Debug.LogError("[SlotMachineController] No item prefab");
                return;
            }

            items = new SlotItem[additionalSlots * 2 + 1];
            var position = additionalSlots * slotsOffset;
            flipPosition = -additionalSlots * slotsOffset;

            for (int i = 0; i < items.Length; i++)
            {
                var inst = GameObject.Instantiate<SlotItem>(itemPrefab, maskTransform);
                var trans = inst.transform as RectTransform;
                trans.anchoredPosition = new Vector2(0, position);
                position -= slotsOffset;
                inst.SetType(sequenceData.GetNext());
                inst.gameObject.SetActive(true);
                items[i] = inst;
            }
        }

        [OnUpdate]
        private void UpdateMe()
        {
            Settings.Fsm.Update(Time.deltaTime);
            updateAction?.Invoke();
        }

        [Bind("StartRunning")]
        private void OnRunningBegin()
        {
            updateAction = () => RunningUpdate(Time.deltaTime);
        }

        [Bind("StartBreaking")]
        private void OnBreakingBegin()
        {
            updateAction = () => StoppingUpdate(Time.deltaTime);
        }

        [Bind("WheelStop")]
        private void OnWheelStop()
        {
            runningVelocity = 0f;
            updateAction = null;
            Settings.Invoke("SpinFinished", alignedSlot);
        }

        private void RunningUpdate(float delta)
        {
            runningVelocity = Mathf.Min(maxVelocity, runningVelocity + runAcceleration * delta);
            UpdateMovement(delta);
        }

        private void StoppingUpdate(float delta)
        {
            runningVelocity = Mathf.Max(alignVelocity, runningVelocity - stopAcceleration * delta);
            if (runningVelocity <= alignVelocity) Settings.Invoke("AlignSpeedReached");
            UpdateMovement(delta);
        }

        private void UpdateMovement(float delta)
        {
            SlotItem aligned = null;
            var shift = runningVelocity * delta;
            for(int i = items.GetUpperBound(0); i >= 0; i--)
            {
                var item = items[i];
                var trans = item.transform as RectTransform;
                var pos = new Vector2(0, trans.anchoredPosition.y - shift);
                trans.anchoredPosition = pos;
                if (pos.y < flipPosition) 
                {
                    FlipItem(i);
                    continue;
                }

                if(Mathf.Abs(trans.anchoredPosition.y) <= alignTolerance)
                {
                    aligned = item;
                }
            }

            if(aligned != null && aligned != alignedSlot)
            {
                alignedSlot = aligned;
                Settings.Invoke("SlotAligned");
            }
        }

        private void FlipItem(int index)
        {
            var item = items[index];
            var trans = item.transform as RectTransform;
            index++;
            if (index == items.Length) index = 0;

            var nextTransform = items[index].transform as RectTransform;
            trans.anchoredPosition = new Vector2(0, nextTransform.anchoredPosition.y + slotsOffset);
            item.SetType(sequenceData.GetNext());
        }
    }
}
