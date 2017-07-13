using System;
using System.Collections;
using UnityEngine;

namespace DTAnimatorStateMachine {
	public class DTStateMachineBehaviour<TStateMachine> : StateMachineBehaviour, IStateBehaviour<TStateMachine> {
		// PRAGMA MARK - IStateBehaviour<TStateMachine> Implementation
		void IStateBehaviour<TStateMachine>.InitializeWithContext(Animator animator, TStateMachine stateMachine) {
			stateMachine_ = stateMachine;
			OnInitialized();
		}

		void IStateBehaviour<TStateMachine>.Enable() {
			enabled_ = true;
		}

		void IStateBehaviour<TStateMachine>.Disable() {
			DisableAndExit();
		}


		// PRAGMA MARK - StateMachineBehaviour Lifecycle
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			if (active_ || !enabled_) {
				return;
			}

			OnStateEntered();
			active_ = true;
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			if (!active_ || !enabled_) {
				return;
			}

			active_ = false;
			OnStateExited();
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
			if (!enabled_) {
				return;
			}

			OnStateUpdated();
		}


		// PRAGMA MARK - Internal
		protected TStateMachine StateMachine_ {
			get { return stateMachine_; }
		}

		private TStateMachine stateMachine_;
		private bool active_ = false;
		private bool enabled_ = true;

		private void OnDisable() {
			DisableAndExit();
		}

		private void DisableAndExit() {
			if (!enabled_) {
				return;
			}

			if (active_) {
				OnStateExited();
				active_ = false;
			}
			enabled_ = false;
		}

		protected virtual void OnInitialized() {
			// stub
		}

		protected virtual void OnStateEntered() {
			// stub
		}

		protected virtual void OnStateExited() {
			// stub
		}

		protected virtual void OnStateUpdated() {
			// stub
		}
	}
}