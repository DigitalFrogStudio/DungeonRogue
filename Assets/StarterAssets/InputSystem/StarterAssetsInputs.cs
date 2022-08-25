using Assets.DungeonRogue.Scripts;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[SerializeField]
		private Animator animator = default;

		[SerializeField]
		private Character character = default;

		[SerializeField]
		private Camera playerCamera = default;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			Vector2 inputVector = value.Get<Vector2>();
			MoveInput(inputVector);
			
			if (inputVector.sqrMagnitude > Vector2.kEpsilon * Vector2.kEpsilon)
			{
				animator.SetBool("IsWalk", true);
			}
			else
			{				
				animator.SetBool("IsWalk", false);
			}
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			bool isSprinting = value.isPressed;

			SprintInput(value.isPressed);

			if (isSprinting == true)
			{
				animator.SetBool("IsRun", true);
				animator.SetBool("IsWalk", false);
			}
			else
			{
				animator.SetBool("IsRun", false);
			}
		}

		public void OnPickUp(InputValue value)
		{
			if (value.isPressed == true)
			{
				Ray cameraRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
				character.AttemptPickUp(cameraRay);
			}
		}

		public void OnSwitchCursorLock(InputValue value)
        {
			if (value.isPressed == true)
			{
				cursorInputForLook = !cursorInputForLook;
				cursorLocked = !cursorLocked;

				SetCursorState(cursorLocked);
			}
        }
#endif

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}