using GorillaNetworking;
using Photon.Pun;
using PlayFab.ClientModels;
using PlayFab;
using System.Text.RegularExpressions;
using StupidTemplate.Notifications;
using static StupidTemplate.Menu.Main;
using UnityEngine;


namespace StupidTemplate.Mods
{
    internal class OPMods
    {
        public static void EnterOP()
        {
            buttonsType = 2;
        }


        public class AntiBanManager : MonoBehaviour
        {
            public static void AntiBan()
            {
                // Check if the player is in a room
                if (PhotonNetwork.CurrentRoom == null)
                {
                    Debug.LogError("Error: Current room is null.");
                    NotifiLib.SendNotification("<color=red>Error: Current room is null.</color>");
                    return;
                }

                bool isRoomOpen = !PhotonNetwork.CurrentRoom.IsOpen;
                if (isRoomOpen)
                {
                    NotifiLib.ClearAllNotifications();
                    NotifiLib.SendNotification("[<color=purple>AntiBan</color>] <color=red>Antiban is currently detected in private rooms i think.</color>");
                }
                else
                {
                    // Ensure that "gameMode" key exists in the custom properties
                    if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("gameMode"))
                    {
                        string currentGameMode = GorillaComputer.instance.currentGameMode.Value;
                        string originalGameMode = PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString();
                        string modifiedGameMode = originalGameMode.Replace(currentGameMode, "MODDED_MODDED" + currentGameMode);

                        ExitGames.Client.Photon.Hashtable hashtable = new ExitGames.Client.Photon.Hashtable
                {
                    { "gameMode", modifiedGameMode }
                };

                        NotifiLib.SendNotification("<color=red>Setting Masterclient</color>...");
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                        bool isMasterClient = PhotonNetwork.IsMasterClient;

                        if (isMasterClient)
                        {
                            NotifiLib.SendNotification("<color=green>Master Has been set</color>...");
                        }

                        PhotonNetwork.CurrentRoom.IsOpen = false;
                        PhotonNetwork.CurrentRoom.IsVisible = false;

                        NotifiLib.SendNotification("<color=red>Setting Gamemode</color>...");
                        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);

                        bool isModded = PhotonNetwork.CurrentRoom.CustomProperties["gameMode"].ToString().Contains("MODDED");
                        if (isModded)
                        {
                            NotifiLib.SendNotification("<color=green>Gamemode Has been set</color>...");
                        }

                        ExecuteCloudScriptRequest executeCloudScriptRequest = new ExecuteCloudScriptRequest
                        {
                            FunctionName = "RoomClosed",
                            FunctionParameter = new
                            {

                                GameId = PhotonNetwork.CurrentRoom.Name,
                                Region = Regex.Replace(PhotonNetwork.CloudRegion, "[^a-zA-Z0-9]", "").ToUpper(),
                                ActorNr = PhotonNetwork.LocalPlayer.ActorNumber,
                                ActorCount = 0,
                                UserId = PhotonNetwork.LocalPlayer.UserId,
                                AppVersion = PhotonNetwork.AppVersion,
                                AppId = PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime,
                                Type = "Close"
                            }
                        };

                        PlayFabClientAPI.ExecuteCloudScript(executeCloudScriptRequest,
                            result =>
                            {
                                NotifiLib.ClearAllNotifications();
                                NotifiLib.SendNotification("<color=purple>Antiban Executed!</color>");
                            },
                            error =>
                            {
                                NotifiLib.ClearAllNotifications();
                                NotifiLib.SendNotification("[<color=red>AntiBan</color>] Malfunction Disconnect");
                                PhotonNetwork.Disconnect();
                            });
                    }
                    else
                    {
                        NotifiLib.SendNotification("<color=red>Error: 'gameMode' not found in CustomProperties.</color>");
                    }
                }
            }



            public static void AutoAntiBan()
            {


            }

            public static void ProjectileSettings()
            {

            }

            public static void RightHand()
            {

            }

            public static void LeftHand()
            {

            }

            public static void EnableFPSCounter()
            {

            }

            public static void DisableFPSCounter()
            {

            }

            public static void EnableNotifications()
            {

            }

            public static void DisableNotifications()
            {

            }

            public static void EnableDisconnectButton()
            {

            }

            public static void DisableDisconnectButton()
            {

            }
        }
    }
}
