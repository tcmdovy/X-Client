using BepInEx;
using ExitGames.Client.Photon;
using GorillaLocomotion.Climbing;
using Photon.Pun;
using Photon.Realtime;
using StupidTemplate.Menu;
using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Valve.VR;
using static StupidTemplate.Classes.RigManager;
using static StupidTemplate.Menu.Main;
using static UnityEngine.Object;
using static StupidTemplate.Patches.InputManager;

namespace StupidTemplate.Mods
{
    internal class MovementMods
    {
        public static void EnterMovement()
        {
            buttonsType = 5;
        }
        public static void LongArms()
        {
            bool enabled = Main.GetIndex("LongArms").enabled;
            if (enabled) GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(3f, 3f, 3f);
            bool disable = Main.GetIndex("LongArms ").enabled = false;
            if (disable) GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        public static void speedboost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 9;
            GorillaLocomotion.Player.Instance.jumpMultiplier = 9;
        }
        public static void Fly()
        {

            if (ControllerInputPoller.instance.rightControllerPrimaryButton || UnityInput.Current.GetKey(KeyCode.F)) ;
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * Main.flySpeed;
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.zero;
            }
        }
    }
}
//creds to IIDK
        

             
          
                    
