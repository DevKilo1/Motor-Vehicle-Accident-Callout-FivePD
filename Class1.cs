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
using System.Security.Policy;
using System.Linq;
using System.Runtime.Remoting.Channels;
using Hash = CitizenFX.Core.Native.Hash;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices.ComTypes;

namespace Motor_Vehicle_Accident
{
    [Guid("40CFB46C-2E74-441F-A9B3-4B537C39BD04")]
    [CalloutProperties("Motor Vehicle Accident: City Parked Vehicles", "DevKilo", "1.0")]
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
        public static bool endedEarly = true;
        bool finishedSpawning = false;
        public static List<Ped> attachedPlayers = new();
        public static Vector3 calloutLocation;
        List<Vector3> coords = new List<Vector3>()
            {
                new(-146.54f,-748.37f,33.93f),
                new(221.12f,269.84f,105.56f),
                new(-604.75f,-882.76f,25.25f),
                new(242.15f,-146f,62.77f),
                new(-53.45f,-1117.44f,26.01f),
                new(125.63f, -1070.75f, 28.76f),
                new(306.47f,-1099.91f,28.96f),
                new(102.11f,-1396.82f,28.84f)
            };
        public Vector3 GetLocation()
        {
            return coords.OrderBy(x => World.GetDistance(x, Game.PlayerPed.Position)).Skip(new Random().Next(0, 3)).First();
        }
        

        public MVA()
        {
            calloutLocation = GetLocation();
            InitInfo(calloutLocation);
            ShortName = "Motor Vehicle Accident";
            CalloutDescription = "An accident has been reported.";
            ResponseCode = 2;
            StartDistance = 20f;
            
        }


        public override async Task OnAccept()
        {
            InitBlip();
            SpawnStuff();
        }

        private async Task SpawnStuff()
        {
            //Debug.WriteLine("check");
            await BaseScript.Delay(500);
            Function.Call(Hash.CLEAR_AREA_OF_VEHICLES, Location.X, Location.Y, Location.Z, 50f, false, false, false, false, false);
            //Debug.WriteLine("check2");
            await BaseScript.Delay(1000);
            if (Location == coords[0])
            {

                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new Vector3(-151.15f, -758.45f, 32.96f), 184.94f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new Vector3(-149.12f, -754.12f, 33.57f), 128.26f);

                
                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            else if (Location == coords[1])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new Vector3(219.35f, 271.01f, 105.05f), 309.09f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new Vector3(221.36f, 268.07f, 105.55f), 235.01f);

                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            else if (Location == coords[2])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new Vector3(-610.41f, -877.94f, 24.79f), 230.53f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new Vector3(-607.85f, -877.07f, 25.26f), 66.06f);

                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            else if (calloutLocation == coords[3])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new(242.15f, -146f, 62.77f), 288.6f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new(246.92f, -144.44f, 63.67f), 260.01f);

                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            else if (calloutLocation == coords[4])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new(-53.45f, -1117.44f, 26.01f), 354.79f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new(-56.05f, -1114.82f, 26.41f), 207.62f);

                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            else if (calloutLocation == coords[5])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new(125.63f, -1070.75f, 28.76f), 182.66f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new(127.88f, -1066.54f, 29.17f), 332.58f);

                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            else if (calloutLocation == coords[6])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new(306.47f, -1099.91f, 28.96f), 298.63f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new(305.64f, -1103.38f, 29.38f), 140.19f);

                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            else if (calloutLocation == coords[7])
            {
                vehicle1 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.Sedans), new(102.11f, -1396.82f, 28.84f), 315.74f);
                vehicle2 = await SpawnVehicle(RandomUtils.GetRandomVehicle(VehicleClass.SUVs), new(98.86f, -1397.67f, 29.23f), 320.86f);

                driver1 = await World.CreatePed(new(RandomUtils.GetRandomPed()), Location.ClosestPedPlacement());
                driver2 = await World.CreatePed(new(RandomUtils.GetRandomPed()), driver1.Position.Around(10f));
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
            //Debug.WriteLine("check3");
            finishedSpawning = true;
            autoerror();
        }

        private async Task autoerror()
        {
            await BaseScript.Delay(10000);
            if (vehicle1.Exists() && driver1.Exists())
            {
                if (driver1.Position.DistanceTo(Location) > 50f)
                {
                    Location = Game.PlayerPed.Position;
                    await BaseScript.Delay(500); // Förlåt! Den här länktexten avslutades på grund av ett internt fel.

                    ShowNetworkedNotification("~b~Sorry!~f~ This callout was ended due to an ~r~internal error~f~.", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Hint", 10f);
                    EndCallout();
                }
            } else if (vehicle2.Exists() && driver2.Exists())
            {
                if (driver2.Position.DistanceTo(Location) > 50f)
                {
                    Location = Game.PlayerPed.Position;
                    await BaseScript.Delay(500);
                    ShowNetworkedNotification("~b~Sorry!~f~ This callout was ended due to an ~r~internal error~f~.", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Hint", 10f);
                    EndCallout();
                }
            }
        }
        

        public override async void OnStart(Ped closest)
        {
            base.OnStart(closest);
            bool db = false;
            Tick += async () =>
            {
                if (finishedSpawning && !db)
                {
                    db = true; // Tryck på E för att prata med förare som slocknar // Press E to talk to the blipped Drivers
                    ShowNetworkedNotification("Press ~y~E~s~ to talk to the ~f~blipped~s~ Drivers", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Hint", 10f);
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
            };
            
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
                                "~f~Officer~s~: Can you tell me what's going on here?",
                                "~f~Driver 1~s~: Thanks for coming so soon, ~f~officer~s~!",
                                "~f~Driver 1~s~: This person hit my car and it made my day so much worse than it already is!",
                                "~f~Officer~s~: All right, just stay here. I will speak to the other party.",
                                "~f~Driver 1~s~: Alright."
                            };
                            listdriver2 = new List<string>()
                            {
                                "~f~Officer~s~: Tell me everything from the beginning.",
                                "~f~Driver 2~s~: I tried to park and I just got this car, I'm sorry I didn't mean to do this!",
                                "~f~Officer~s~: Right, this sounds like you are ~r~admitting responsibility~s~ for this accident.",
                                "~f~Driver 2~s~: Yes.",
                                "~f~Officer~s~: Give them your ~f~insurance~s~ info for me and we'll be on our way.",
                                "~f~Driver 2~s~: Oh, my God, I'm so pissed!"
                            };
                            for (int i = 0; i < listdriver1.Count; i++)
                            {
                                driver1.Task.ChatTo(Game.PlayerPed);
                                //driver1.Task.PlayAnimation("gestures@m@car@low@casual@ds", "gesture_chat", 5f, -1, AnimationFlags.None);
                                driver1.Task.PlayAnimation("missheistdockssetup1leadinoutig_1", "lsdh_ig_1_argue_wade");
                                if (listdriver1[i].Contains("~f~Driver"))
                                    driver1.PlayAmbientSpeech("APOLOGY_NO_TROUBLE", SpeechModifier.Standard);
                                if (listdriver1[i].Contains("~f~Officer"))
                                    Game.PlayerPed.PlayAmbientSpeech("CHAT_RESP", SpeechModifier.ForceNormalClear);
                                ShowDialog(listdriver1[i], 5000, 5f);
                                await BaseScript.Delay(5000);
                            }
                            driver1.Task.ClearAll();
                            driver1Blip.Delete();
                            driver2Blip = driver2.AttachBlip();
                            driver1SpokenTo = true;
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
                                "~f~Officer~s~: Hello, I got a call. Tell me what you know.",
                                "~f~Driver 1~s~: Sure! They hit my car when they tried to park.",
                                "~f~Officer~s~: Alright, wait here.",
                                "~r~Driver 1~s~: That ~r~idiot~s~ will get what they deserve!"
                            };
                            for (int i = 0; i < listdriver1.Count; i++)
                            {
                                driver1.Task.ChatTo(Game.PlayerPed);
                                //driver1.Task.PlayAnimation("gestures@m@car@low@casual@ds", "gesture_chat", 5f, -1, AnimationFlags.None);
                                driver1.Task.PlayAnimation("missheistdockssetup1leadinoutig_1", "lsdh_ig_1_argue_wade");
                                if (listdriver1[i].Contains("~f~Driver"))
                                    driver1.PlayAmbientSpeech("CHAT_STATE", SpeechModifier.Standard);
                                if (listdriver1[i].Contains("~f~Officer"))
                                    Game.PlayerPed.PlayAmbientSpeech("CHAT_RESP", SpeechModifier.ForceNormalClear);
                                ShowDialog(listdriver1[i], 5000, 5f);
                                await BaseScript.Delay(5000);
                            }
                            // Driver 1 goes to fight Driver 2
                            listdriver2 = null;
                            driver1.Task.FightAgainst(driver2);
                            endedEarly = false;
                            driver1Blip.Color = BlipColor.Red;
                            driver2.BlockPermanentEvents = false;
                            Location = calloutLocation;
                        }
                        //
    
                    }
                }
                else if (driver1SpokenTo && Game.PlayerPed.Position.DistanceTo(driver2.Position) < 2f)
                {
                    if (Game.IsControlJustPressed(0, Control.Pickup))
                    {
                        //Debug.WriteLine("YES");
                        List<string> listdriver2;
                        if (driver2SpokenTo) return;
                        BaseScript.TriggerServerEvent("MVA-TellServerControlPressed", "driver2");
                        calloutLocationIsDriver2 = true;
                        Location = driver2.Position;
                        driver2.Task.ClearAllImmediately();
                        driver2.Task.TurnTo(Game.PlayerPed);
                        await BaseScript.Delay(1000);
                        if (chance < 80)
                        {
                            listdriver2 = new List<string>()
                            {
                                "~f~Officer~s~: Tell me everything from the beginning.",
                                "~f~Driver 2~s~: I tried to park and I just got this car, I'm sorry I didn't mean to do this!",
                                "~f~Officer~s~: Right, this sounds like you are ~r~admitting responsibility~s~ for this accident.",
                                "~f~Driver 2~s~: Yes.",
                                "~f~Officer~s~: Give them your ~f~insurance~s~ info for me and we'll be on our way.",
                                "~f~Driver 2~s~: Oh, my God, I'm so pissed!"
                            };
                            for (int i = 0; i < listdriver2.Count; i++)
                            {
                                driver2.Task.ChatTo(Game.PlayerPed);
                                //driver2.Task.PlayAnimation("gestures@m@car@low@casual@ds", "gesture_chat", 5f, -1, AnimationFlags.None);
                                driver2.Task.PlayAnimation("missheistdockssetup1leadinoutig_1", "lsdh_ig_1_argue_wade");
                                if (listdriver2[i].Contains("~f~Driver"))
                                    driver2.PlayAmbientSpeech("CHAT_STATE", SpeechModifier.Standard);
                                if (listdriver2[i].Contains("~f~Officer"))
                                    Game.PlayerPed.PlayAmbientSpeech("CHAT_RESP", SpeechModifier.ForceNormalClear);
                                ShowDialog(listdriver2[i], 5000, 5f);
                                await BaseScript.Delay(5000);
                            }
                            driver2.Task.ClearAll();
                            driver2SpokenTo = true;
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
                            ShowNetworkedNotification("~y~Don't worry~s~! This callout will ~f~autoclean~s~ in 60 seconds", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Assurance", 10f);
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
            if (endedEarly)
            {
                if (driver1Blip != null)
                {
                    driver1Blip.Delete();
                    driver1Blip = null;
                }

                if (driver2Blip != null)
                {
                    driver2Blip.Delete();
                    driver2Blip = null;
                }

                if (driver1 != null && driver1.Exists())
                    driver1.Delete();
                if (driver2 != null && driver2.Exists())
                    driver2.Delete();
                if (vehicle1 != null && vehicle1.Exists())
                    vehicle1.Delete();
                if (vehicle2 != null && vehicle2.Exists())
                    vehicle2.Delete();
            }
           
            if (driver1SpokenTo)
                driver1SpokenTo = false;
            if (driver2SpokenTo)
                driver2SpokenTo = false;
            endedEarly = false;
        }
        public override Task<bool> CheckRequirements() => Task.FromResult(WorldZone.GetZoneAtPosition(Game.PlayerPed.Position).County.Equals("LosSantos"));
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
