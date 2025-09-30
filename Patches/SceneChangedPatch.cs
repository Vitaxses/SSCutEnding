using System.Linq;
using HarmonyLib;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CutEnding.Patches;

public class SceneChangedPatch
{
    public static void OnSceneChanged(Scene to, LoadSceneMode lsm)
    {
        if (to.name.Equals("Cradle_03"))
        {
            var bossScene = GameObject.Find("Boss Scene");
            if (bossScene == null) return;

            var deathSequence = bossScene.transform.GetChild(11).gameObject;
            var fsm = FSMUtility.GetFSM(deathSequence).Fsm;
            var state1 = fsm.GetState("Bind Or Needolin");
            if (state1 == null) return;

            var timeVar = new FsmFloat("Time") { Value = 2f };
            fsm.Variables.FloatVariables = fsm.Variables.FloatVariables.AddItem(timeVar).ToArray();

            var failEvent = new FsmEvent("FAILED_INPUT");
            fsm.Events = fsm.Events.AddItem(failEvent).ToArray();

            state1.Actions = state1.Actions.AddItem
            (
                new Wait
                {
                    time = timeVar,
                    finishEvent = failEvent
                }
            ).ToArray();

            FsmState state2 = fsm.GetState("Death Slashes Up E");
            if (state2 == null) return;

            state1.Transitions = state1.Transitions.AddItem
            (
                new FsmTransition
                {
                    FsmEvent = failEvent,
                    ToState = state2.Name,
                    ToFsmState = state2
                }
            ).ToArray();

            state2.Actions = state2.Actions.AddItem
            (
                new CallActionFsm
                (
                    () =>
                    {
                        deathSequence.transform.GetChild(0).gameObject.SetActive(false);
                    }
                )
            ).ToArray();
        }
    }

}