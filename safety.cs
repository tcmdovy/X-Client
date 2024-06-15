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
    internal class SafetyMods
    {
        public static void EnterSafety()
        {
            buttonsType = 7;
        }
        public static void RPCProtection()
        {
            
            if (PhotonNetwork.InRoom;)
            {
                GorillaNot.instance.rpcCallLimit = int.MaxValue;
                PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
                PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
                PhotonNetwork.OpCleanRpcBuffer(GorillaTagger.Instance.myVRRig);
                PhotonNetwork.RemoveBufferedRPCs(GorillaTagger.Instance.myVRRig.ViewID, null, null);
            }
     
    }
        public static void AntiReport()
        {
            try
            {
                foreach (GorillaPlayerScoreboardLine line in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    if (line.linePlayer == NetworkSystem.Instance.LocalPlayer)
                    {
                        Transform report = line.reportButton.gameObject.transform;
                        foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                        {
                            if (vrrig != GorillaTagger.Instance.offlineVRRig)
                            {
                                float D1 = Vector3.Distance(vrrig.rightHandTransform.position, report.position);
                                float D2 = Vector3.Distance(vrrig.leftHandTransform.position, report.position);

                                float threshold = 0.35f;

                                if (D1 < threshold || D2 < threshold)
                                {
                                    PhotonNetwork.Disconnect();
                                    RPCProtection();
                                    NotifiLib.SendNotification("<color=grey>[</color><color=purple>ANTI-REPORT Active</color><color=grey>]</color> <color=white> you have been disconnected.</color>");
                                }
                            }
                        }
                    }
                }
            }
            catch { } // Not connected
        }
    }
    }

