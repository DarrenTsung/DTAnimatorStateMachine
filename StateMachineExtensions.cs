using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTAnimatorStateMachine {
	public static class StateMachineExtensions {
		public static IEnumerable<IStateBehaviour<TStateMachine>> GetStateBehaviours<TStateMachine>(this TStateMachine stateMachine, Animator animator) {
			StateMachineBehaviour[] behaviours = animator.GetBehaviours<StateMachineBehaviour>();
			foreach (StateMachineBehaviour behaviour in behaviours) {
				IStateBehaviour<TStateMachine> stateBehaviour = behaviour as IStateBehaviour<TStateMachine>;
				if (stateBehaviour == null) {
					continue;
				}

				yield return stateBehaviour;
			}
		}

		public static void ConfigureAllStateBehaviours<TStateMachine>(this TStateMachine stateMachine, Animator animator) {
			foreach (IStateBehaviour<TStateMachine> behaviour in stateMachine.GetStateBehaviours(animator)) {
				behaviour.InitializeWithContext(animator, stateMachine);
			}
		}

		public static void DisableAllStateBehaviours<TStateMachine>(this TStateMachine stateMachine, Animator animator) {
			foreach (IStateBehaviour<TStateMachine> behaviour in stateMachine.GetStateBehaviours(animator)) {
				behaviour.Disable();
			}
		}

		public static void EnableAllStateBehaviours<TStateMachine>(this TStateMachine stateMachine, Animator animator) {
			foreach (IStateBehaviour<TStateMachine> behaviour in stateMachine.GetStateBehaviours(animator)) {
				behaviour.Enable();
			}
		}
	}
}