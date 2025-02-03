using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
public class PlayerInputHandler : MonoBehaviour
{
   [Header("Input Action Asset")]
   [SerializeField] private InputActionAsset playerControls;

   [Header("Action Map Name Reference")]
   [SerializeField] private string actionMapName = "Player";


   [Header("Action Name References")]
   [SerializeField] private string move = "Move";
   [SerializeField] private string dash = "Dash";
   [SerializeField] private string interact = "Interact";
   [SerializeField] private string meleeAttack = "MeleeAttack";
   [SerializeField] private string meleeSpecialAttack = "MeleeSpecialAttack";
   private InputAction moveAction;
   private InputAction dashAction;
   private InputAction interactAction;
   private InputAction meleeAttackAction;
   private InputAction meleeSpecialAttackAction;

   public Vector2 MoveInput { get; private set; }
   public bool dashTriggered { get; private set; }
   public bool interactTriggered { get; private set; }

   public static event Action meleeAttackTriggered;
   public static event Action meleeSpecialAttackTriggered;
   public static PlayerInputHandler Instance { get; private set; }

   private void Awake()
   {
      if (Instance == null)
      {
         Debug.Log("PlayerInputHandler Instanced");
         Instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else if (Instance != this)
      {
         Debug.Log("Destroying duplicate PlayerInputHandler");
         Destroy(gameObject);
         return;
      }
      moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
      dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
      interactAction = playerControls.FindActionMap(actionMapName).FindAction(interact);
      meleeAttackAction = playerControls.FindActionMap(actionMapName).FindAction(meleeAttack);
      meleeSpecialAttackAction = playerControls.FindActionMap(actionMapName).FindAction(meleeSpecialAttack);
      RegisterInputActions();
   }
   void RegisterInputActions()
   {
      moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
      moveAction.canceled += context => MoveInput = Vector2.zero;

      dashAction.performed += context => dashTriggered = true;
      dashAction.canceled += context => dashTriggered = false;

      interactAction.performed += context => interactTriggered = true;
      interactAction.canceled += context => interactTriggered = false;

      meleeAttackAction.performed += HandleMeleeAttack;

      meleeSpecialAttackAction.performed += HandleMeleeSpecialAttack;
   }

   private void HandleMeleeAttack(InputAction.CallbackContext context)
   {
      meleeAttackTriggered?.Invoke();
   }
   private void HandleMeleeSpecialAttack(InputAction.CallbackContext context)
   {
      meleeSpecialAttackTriggered?.Invoke();
   }
   private void OnEnable()
   {
      Debug.Log("PlayerInputHandler Enabled");
      moveAction.Enable();
      dashAction.Enable();
      interactAction.Enable();
      meleeAttackAction.Enable();
      meleeSpecialAttackAction.Enable();
   }
   private void OnDisable()
   {
      Debug.Log("PlayerInputHandler Disabled");
      moveAction.Disable();
      dashAction.Disable();
      interactAction.Disable();
      meleeAttackAction.Disable();
      meleeSpecialAttackAction.Disable();

   }
}
