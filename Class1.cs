using FivePD.API;
using CitizenFX.Core;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Messaging;
using System.Drawing.Text;
using System;
using FivePD.API.Utils;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CitizenFX.Core.UI;
using CitizenFX.Core.Native;

namespace Motor_Vehicle_Accident
{
    [Guid("40CFB46C-2E74-441F-A9B3-4B537C39BD04")]
    [CalloutProperties("Motor Vehicle Accident (NORSK DEVELOPMENT)", "DevKilo", "0.1")]
    public class MVA : Callout
    {
        public static Ped driver1, driver2;
        Blip driver1Blip, driver2Blip;
        private Vehicle vehicle1, vehicle2;
        public static Ped closestPed;
        public static bool calloutLocationIsDriver1 = false;
        public static bool calloutLocationIsDriver2 = false;
        public static bool driver1SpokenTo = false;
        public static bool driver2SpokenTo = false;
        public static List<Ped> attachedPlayers = new List<Ped>();
        public static Vector3 calloutLocation;
        Vector3[] coords = new Vector3[]
{
                new(-146.54f,-748.37f,33.93f),
                new(221.12f,269.84f,105.56f),
                new(-604.75f,-882.76f,25.25f),
};
        public Vector3 GetLocation()
        {
            Vector3 coord = coords[new Random().Next(coords.Length)];

            return coord;


        }

        public MVA()
        {
            InitInfo(GetLocation());
            ShortName = "Motor Vehicle Accident";
            CalloutDescription = "";
            ResponseCode = 2;
            StartDistance = 20f;
            calloutLocation = Location;
        }

        public override async Task OnAccept()
        {
            InitBlip();
            if (Location == coords[0])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new Vector3(-151.15f, -758.45f, 32.96f), 184.94f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new Vector3(-149.12f, -754.12f, 33.57f), 128.26f);
                driver1 = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnSidewalk(vehicle1.Position));
                driver2 = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnSidewalk(vehicle2.Position));
                driver1.AlwaysKeepTask = true;
                driver1.BlockPermanentEvents = true;
                driver2.AlwaysKeepTask = true;
                driver2.BlockPermanentEvents = true;
                if (driver1.Gender == Gender.Male)
                    driver1.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.Loop);
                else
                    driver1.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.Loop);
                if (driver2.Gender == Gender.Male)
                    driver2.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.Loop);
                else
                  driver2.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.Loop);

                driver1Blip = driver1.AttachBlip();
                driver1Blip.Color = BlipColor.MichaelBlue;
            } else if (Location == coords[1])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new Vector3(219.35f,271.01f,105.05f),309.09f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new Vector3(221.36f,268.07f,105.55f),235.01f);
                driver1 = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnSidewalk(vehicle1.Position));
                driver2 = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnSidewalk(vehicle2.Position));
                driver1.AlwaysKeepTask = true;
                driver1.BlockPermanentEvents = true;
                driver2.AlwaysKeepTask = true;
                driver2.BlockPermanentEvents = true;
                if (driver1.Gender == Gender.Male)
                    driver1.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.Loop);
                else
                    driver1.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.Loop);
                if (driver2.Gender == Gender.Male)
                    driver2.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.Loop);
                else
                    driver2.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.Loop);

                driver1Blip = driver1.AttachBlip();
                driver1Blip.Color = BlipColor.MichaelBlue;
            } else if (Location == coords[2])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new Vector3(-610.41f,-877.94f,24.79f),230.53f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new Vector3(-607.85f,-877.07f,25.26f),66.06f);
                driver1 = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnSidewalk(vehicle1.Position));
                driver2 = await SpawnPed(RandomUtils.GetRandomPed(), World.GetNextPositionOnSidewalk(vehicle2.Position));
                driver1.AlwaysKeepTask = true;
                driver1.BlockPermanentEvents = true;
                driver2.AlwaysKeepTask = true;
                driver2.BlockPermanentEvents = true;
                if (driver1.Gender == Gender.Male)
                    driver1.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.Loop);
                else
                    driver1.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.Loop);
                if (driver2.Gender == Gender.Male)
                    driver2.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.Loop);
                else
                    driver2.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.Loop);

                driver1Blip = driver1.AttachBlip();
                driver1Blip.Color = BlipColor.MichaelBlue;
            }
        }

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            ShowNetworkedNotification("Trykk ~y~E ~s~for å snakke med ~f~blipped~s~ Drivere", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Hint", 10f);
            driver1.Task.ClearAllImmediately();
            driver2.Task.ClearAll();
            await BaseScript.Delay(1000);
            driver2.Task.StandStill(-1);
            driver1.Task.TurnTo(closest.Position);
            await BaseScript.Delay(2000);
            driver1.Task.PlayAnimation("rcmpaparazzo1ig_1_waive", "waive_comeback_a", 5f, -1, AnimationFlags.None);
            vehicle1.HasBeenDamagedBy(vehicle2);
            await InteractPed(closest);
        }
        public async Task InteractPed(Ped closest)
        {
            Tick += async () =>
            {
                //Debug.WriteLine("Tick");
                closestPed = closest;
                if (calloutLocationIsDriver1 && Location != driver1.Position)
                    Location = driver1.Position;
                if (calloutLocationIsDriver2 && Location != driver2.Position)
                    Location = driver2.Position;
                if (!calloutLocationIsDriver1 && !calloutLocationIsDriver2 && Location == driver1.Position || !calloutLocationIsDriver1 && !calloutLocationIsDriver2 && Location == driver2.Position)
                    Location = calloutLocation;
                if (closest.IsPlayer && !attachedPlayers.Contains(closest))
                    attachedPlayers.Add(closest);
                attachedPlayers.Add(Game.PlayerPed);
                int chance = new Random().Next(100);
                if (Game.PlayerPed.Position.DistanceTo(driver1.Position) < 2f)
                {
                    if (Game.IsControlJustPressed(0, Control.Pickup))
                    {
                        //Debug.WriteLine("YES");

                        if (driver1SpokenTo) return;
                        List<string> listdriver1;
                        List<string> listdriver2;
                        // THIS IS WHERE I LEFT OFF 12/28/2022 5:26 PM
                        // LISTING DRIVER DIALOGUE BY CHANCE
                        BaseScript.TriggerServerEvent("MVA-TellServerControlPressed", "driver1");
                        calloutLocationIsDriver1 = true;
                        driver1.Task.ClearAllImmediately();
                        await BaseScript.Delay(100);
                        driver1.Task.TurnTo(Game.PlayerPed);
                        await BaseScript.Delay(1000);
                        driver1.Task.LookAt(Game.PlayerPed);
                        await BaseScript.Delay(500);
                        Location = driver1.Position;
                        if (chance < 80)
                        {
                            listdriver1 = new List<string>()
                            {
                                "~f~Offiser~s~: Kan du fortelle meg hva som skjer her?",
                                "~f~Sjåfør 1~s~: Takk for at du kom så snart, ~f~offiser~s~!",
                                "~f~Sjåfør 1~s~: Denne personen traff bilen min og det gjorde dagen min så mye verre enn den allerede er!",
                                "~f~Offiser~s~: Greit, bare bli her. Jeg skal snakke med den andre parten.",
                                "~f~Sjåfør 1~s~: Greit."
                            };
                            listdriver2 = new List<string>()
                            {
                                "~f~Offiser~s~: Fortell meg alt fra begynnelsen.",
                                "~f~Sjåfør 2~s~: Jeg prøvde å parkere og jeg fikk akkurat denne bilen, jeg beklager at jeg ikke mente å gjøre dette!",
                                "~f~Offiser~s~: Greit, dette høres ut som du ~r~innrømmer ansvaret~s~ for denne ulykken.",
                                "~f~Sjåfør 2~s~: Ja.",
                                "~f~Offiser~s~: Gi dem din ~f~forsikring~s~ informasjon for meg, så er vi på vei.",
                                "~f~Sjåfør 2~s~: Åh, herregud, jeg er så forbanna!"
                            };
                            for (int i = 0; i < listdriver1.Count; i++)
                            {
                                driver1.Task.ChatTo(Game.PlayerPed);
//                                driver1.Task.PlayAnimation("misstrevor2", "gang_chatting_idle02_a", 5f, -1, AnimationFlags.None
                                if (listdriver1[i].Contains("~f~Sjåfør"))
                                    driver1.PlayAmbientSpeech("APOLOGY_NO_TROUBLE", SpeechModifier.Standard);
                                if (listdriver1[i].Contains("~f~Offiser"))
                                    Game.PlayerPed.PlayAmbientSpeech("CHAT_RESP", SpeechModifier.ForceNormalClear);
                                ShowDialog(listdriver1[i], 5000, 5f);
                                await BaseScript.Delay(5000);
                            }
                            driver1.Task.ClearAll();
                            driver1Blip.Delete();
                            driver2Blip = driver2.AttachBlip();
                            driver2Blip.Color = BlipColor.MichaelBlue;
                            await BaseScript.Delay(2000);
                            if (driver1.Gender == Gender.Male)
                                driver1.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.Loop);
                            else
                                driver1.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.Loop);
                            Location = calloutLocation;
                        }
                        else
                        {
                            listdriver1 = new List<string>()
                            {
                                "~f~Offiser~s~: Hei, jeg ble oppringt. Fortell meg hva du vet.",
                                "~f~Sjåfør 1~s~: Sikker! De traff bilen min da de prøvde å parkere.",
                                "~f~Offiser~s~: Greit, vent her.",
                                "~r~Sjåfør 1~s~: Den ~r~idioten~s~ får det de fortjener!"
                            };
                            for (int i = 0; i < listdriver1.Count; i++)
                            {
                                driver1.Task.ChatTo(Game.PlayerPed);
                                //driver1.Task.PlayAnimation("misstrevor2", "gang_chatting_idle02_a", 5f, -1, AnimationFlags.None);
                                if (listdriver1[i].Contains("~f~Sjåfør"))
                                    driver1.PlayAmbientSpeech("CHAT_STATE", SpeechModifier.Standard);
                                if (listdriver1[i].Contains("~f~Offiser"))
                                    Game.PlayerPed.PlayAmbientSpeech("CHAT_RESP", SpeechModifier.ForceNormalClear);
                                ShowDialog(listdriver1[i], 5000, 5f);
                                await BaseScript.Delay(5000);
                            }
                            // Driver 1 goes to fight Driver 2
                            listdriver2 = null;
                            driver1.Task.FightAgainst(driver2);
                            driver1Blip.Color = BlipColor.Red;
                            driver2.BlockPermanentEvents = false;
                            Location = calloutLocation;
                        }
                        //
    
                    }
                }
                else if (Game.PlayerPed.Position.DistanceTo(driver2.Position) < 2f)
                {
                    if (Game.IsControlJustPressed(0, Control.Pickup))
                    {
                        //Debug.WriteLine("YES");
                        List<string> listdriver2;
                        if (driver2SpokenTo) return;
                        BaseScript.TriggerServerEvent("MVA-TellServerControlPressed", "driver2");
                        calloutLocationIsDriver2 = true;
                        Location = driver2.Position;
                        driver2.Task.ClearAll();
                        await BaseScript.Delay(500);
                        if (chance < 80)
                        {
                            listdriver2 = new List<string>()
                            {
                                "~f~Offiser~s~: Fortell meg alt fra begynnelsen.",
                                "~f~Sjåfør 2~s~: Jeg prøvde å parkere og jeg fikk akkurat denne bilen, jeg beklager at jeg ikke mente å gjøre dette!",
                                "~f~Offiser~s~: Greit, det høres ut som du ~r~innrømmer ansvaret~s~ for denne ulykken.",
                                "~f~Sjåfør 2~s~: Ja.",
                                "~f~Offiser~s~: Gi dem din ~f~forsikring~s~ informasjon for meg, så er vi på vei.",
                                "~f~Sjåfør 2~s~: Åh, herregud, jeg er så forbanna!"
                            };
                            for (int i = 0; i < listdriver2.Count; i++)
                            {
                                driver2.Task.ChatTo(Game.PlayerPed);
                                //driver2.Task.PlayAnimation("misstrevor2", "gang_chatting_idle02_a", 5f, -1, AnimationFlags.None);
                                if (listdriver2[i].Contains("~f~Driver"))
                                    driver2.PlayAmbientSpeech("CHAT_STATE", SpeechModifier.Standard);
                                if (listdriver2[i].Contains("~f~Offiser"))
                                    Game.PlayerPed.PlayAmbientSpeech("CHAT_RESP", SpeechModifier.ForceNormalClear);
                                ShowDialog(listdriver2[i], 5000, 5f);
                                await BaseScript.Delay(5000);
                            }
                            driver2.Task.ClearAll();
                            await BaseScript.Delay(1000);
                            driver2.Task.GoTo(driver1);
                            while (driver2.Position.DistanceTo(driver1.Position) > 3f)
                                await BaseScript.Delay(500);
                            driver2.Task.TurnTo(driver1);
                            await BaseScript.Delay(1000);
                            driver2.Task.PlayAnimation("gestures@m@car@low@casual@ds", "gesture_hand_right_two", 5f, -1, AnimationFlags.None);
                            await BaseScript.Delay(4000);
                            driver2.Task.ClearAllImmediately();
                            driver1.Task.ClearAllImmediately();
                            await BaseScript.Delay(1000);
                            driver2.Task.CruiseWithVehicle(vehicle2, 40f);
                            driver2Blip.Delete();
                            driver1Blip.Delete();
                            driver1.Task.CruiseWithVehicle(vehicle1, 40f);
                            bool db = false;
                            ShowNetworkedNotification("~y~Ikke bekymre deg~s~! Denne forklaringen vil ~f~autorense~s~ om 60 sekunder", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Assurance", 10f);
                            while (!driver2.IsInVehicle() && !driver1.IsInVehicle())
                            {
                                await BaseScript.Delay(500);
                                if (!db)
                                {
                                    db = true;
                                    await BaseScript.Delay(10000);
                                    if (!driver2.IsInVehicle() && !driver1.IsInVehicle() || driver1.SeatIndex != VehicleSeat.Driver || driver2.SeatIndex != VehicleSeat.Driver)
                                    {
                                        if (driver1.SeatIndex != VehicleSeat.Driver)
                                        {
                                            driver1.Position = vehicle1.Position.Around(5f);
                                            await BaseScript.Delay(200);
                                            driver1.Task.ClearAllImmediately();
                                            await BaseScript.Delay(500);
                                            driver1.Task.CruiseWithVehicle(vehicle1, 40f);
                                        }
                                        if (driver2.SeatIndex != VehicleSeat.Driver)
                                        {
                                            driver2.Position = vehicle2.Position.Around(5f);
                                            await BaseScript.Delay(200);
                                            driver2.Task.ClearAllImmediately();
                                            await BaseScript.Delay(500);
                                            driver2.Task.CruiseWithVehicle(vehicle2, 40f);
                                            
                                        }
                                        await BaseScript.Delay(50000);
                                        if (!driver2.IsInVehicle() && !driver1.IsInVehicle() || driver1.SeatIndex != VehicleSeat.Driver || driver2.SeatIndex != VehicleSeat.Driver)
                                        {
                                            /*if (driver2.Exists())
                                                driver2.Delete();
                                            if (driver1.Exists())
                                                driver1.Delete();
                                            if (vehicle1.Exists())
                                                vehicle1.Delete();
                                            if (vehicle2.Exists())
                                                vehicle2.Delete();*/
                                            EndCallout();
                                        }
                                            
                                    }
                                }
                            }
                            EndCallout();
                            
                            
                        }
                        else
                        {
                            // Driver 1 goes to fight Driver 2
                            listdriver2 = null;
                            driver2.Task.ClearAll();
                            await BaseScript.Delay(1000);
                            if (driver2.Gender == Gender.Male)
                                driver2.Task.PlayAnimation("amb@world_human_stand_impatient@male@no_sign@idle_a", "idle_a", 5f, -1, AnimationFlags.CancelableWithMovement);
                            else
                                driver2.Task.PlayAnimation("amb@world_human_stand_impatient@female@no_sign@idle_a", "idle_a", 3f, -1, AnimationFlags.CancelableWithMovement);

                        }

                        //

                    }
                    
                }
            };
        }
        public override void OnCancelBefore()
        {
            base.OnCancelBefore();
            attachedPlayers.Clear();
            if (driver1Blip != null)
                driver1Blip.Delete();
            if (driver2Blip != null)
                driver2Blip.Delete();
            if (driver1.Exists())
                driver1.Delete();
            if (driver2.Exists())
                driver2.Delete();
            if (vehicle1.Exists())
                vehicle1.Delete();
            if (vehicle2.Exists())
                vehicle2.Delete();
        }
        public override void OnCancelAfter()
        {
            base.OnCancelAfter();
 
        }
        public override void OnBackupReceived(Player player)
        {
            base.OnBackupReceived(player);
            attachedPlayers.Add(player.Character);
        }
        public override void OnPlayerRevokedBackup(Player player)
        {
            base.OnPlayerRevokedBackup(player);
            attachedPlayers.Remove(player.Character);
        }

    }
    public class Plugin : BaseScript
    {

        public Plugin()
        {
            EventHandlers["MVA-TellAllClientsControlPressed"] += HandleEvent_MVATellAllClientsControlPressed;
        }
        private void HandleEvent_MVATellAllClientsControlPressed(EventArgs e)
        {
            if (!MVA.attachedPlayers.Contains(Game.PlayerPed)) return;
            if (e.ToString() == "driver1")
            {
                MVA.driver1SpokenTo = true;
            } else if (e.ToString() == "driver2")
            {
                MVA.driver2SpokenTo = true;
            }

        }
    }
}