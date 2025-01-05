using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
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

   private InputAction moveAction;
   private InputAction dashAction;
   private InputAction interactAction;

   public Vector2 MoveInput { get; private set; }
   public bool dashTriggered { get; private set; }
   public bool interactTriggered { get; private set; }

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
   }
   private void OnEnable()
   {
      Debug.Log("PlayerInputHandler Enabled");
      moveAction.Enable();
      dashAction.Enable();
      interactAction.Enable();
   }
   private void OnDisable()
   {
      Debug.Log("PlayerInputHandler Disabled");
      moveAction.Disable();
      dashAction.Disable();
      interactAction.Disable();
   }
}
