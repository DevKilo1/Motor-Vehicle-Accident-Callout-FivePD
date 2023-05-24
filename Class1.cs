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

namespace Motor_Vehicle_Accident
{
    [Guid("40CFB46C-2E74-441F-A9B3-4B537C39BD04")]
    [CalloutProperties("Motorfordonsolycka: Stadsparkerade fordon", "DevKilo", "1.0")]
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
            ShortName = "Motorfordonsolycka";
            CalloutDescription = "En olycka har rapporterats.";
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

                    ShowNetworkedNotification("~b~Förlåt!~f~ Den här länktexten avslutades på grund av ett ~r~internt fel~f~.", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Hint", 10f);
                    EndCallout();
                }
            } else if (vehicle2.Exists() && driver2.Exists())
            {
                if (driver2.Position.DistanceTo(Location) > 50f)
                {
                    Location = Game.PlayerPed.Position;
                    await BaseScript.Delay(500);
                    ShowNetworkedNotification("~b~Förlåt!~f~ Den här länktexten avslutades på grund av ett ~r~internt fel~f~.", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Hint", 10f);
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
                    db = true; // Tryck på E för att prata med förare som slocknar
                    ShowNetworkedNotification("Tryck på ~y~E~s~ för att prata med ~f~förare som slocknar~s~", "CHAR_CALL911", "CHAR_CALL911", "Dispatch", "Hint", 10f);
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
                                "~f~Polis~s~: Kan du berätta vad som händer här?",
                                "~f~Förare 1~s~: Tack för att du kom så snart, ~f~Polis~s~!",
                                "~f~Förare 1~s~: Den här personen körde min bil och det gjorde min dag så mycket värre än den redan är!",
                                "~f~Polis~s~: Okej, stanna bara här. Jag ska prata med den andra parten.",
                                "~f~Förare 1~s~: Okej."
                            };
                            listdriver2 = new List<string>()
                            {
                                "~f~Polis~s~: Berätta allt från början.",
                                "~f~Förare 2~s~: Jag försökte parkera och jag fick precis den här bilen, jag är ledsen att jag inte menade att göra det här!",
                                "~f~Polis~s~: Okej, det här låter som att du ~r~erkänner ansvaret~s~ för den här olyckan.",
                                "~f~Förare 2~s~: Ja.",
                                "~f~Polis~s~: Ge dem din ~f~försäkring~s~ information åt mig så är vi på väg.",
                                "~f~Förare 2~s~: Herregud, jag är så förbannad!"
                            };
                            for (int i = 0; i < listdriver1.Count; i++)
                            {
                                driver1.Task.ChatTo(Game.PlayerPed);
                                //driver1.Task.PlayAnimation("gestures@m@car@low@casual@ds", "gesture_chat", 5f, -1, AnimationFlags.None);
                                driver1.Task.PlayAnimation("missheistdockssetup1leadinoutig_1", "lsdh_ig_1_argue_wade");
                                if (listdriver1[i].Contains("~f~Förare"))
                                    driver1.PlayAmbientSpeech("APOLOGY_NO_TROUBLE", SpeechModifier.Standard);
                                if (listdriver1[i].Contains("~f~Polis"))
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
                                "~f~Polis~s~: Hej, jag fick ett samtal. Berätta vad du vet.",
                                "~f~Förare 1~s~: Visst! De träffade min bil när de försökte parkera.",
                                "~f~Polis~s~: Okej, vänta här.",
                                "~r~Förare 1~s~: Den ~r~idioten~s~ kommer att få vad de förtjänar!"
                            };
                            for (int i = 0; i < listdriver1.Count; i++)
                            {
                                driver1.Task.ChatTo(Game.PlayerPed);
                                //driver1.Task.PlayAnimation("gestures@m@car@low@casual@ds", "gesture_chat", 5f, -1, AnimationFlags.None);
                                driver1.Task.PlayAnimation("missheistdockssetup1leadinoutig_1", "lsdh_ig_1_argue_wade");
                                if (listdriver1[i].Contains("~f~Förare"))
                                    driver1.PlayAmbientSpeech("CHAT_STATE", SpeechModifier.Standard);
                                if (listdriver1[i].Contains("~f~Polis"))
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
                                "~f~Polis~s~: Berätta allt från början.",
                                "~f~Förare 2~s~: Jag försökte parkera och jag fick precis den här bilen, jag är ledsen att jag inte menade att göra det här!",
                                "~f~Polis~s~: Okej, det här låter som att du ~r~erkänner ansvaret~s~ för den här olyckan.",
                                "~f~Förare 2~s~: Ja.",
                                "~f~Polis~s~: Ge dem din ~f~försäkring~s~ information åt mig så är vi på väg.",
                                "~f~Förare 2~s~: Herregud, jag är så förbannad!"
                            };
                            for (int i = 0; i < listdriver2.Count; i++)
                            {
                                driver2.Task.ChatTo(Game.PlayerPed);
                                //driver2.Task.PlayAnimation("gestures@m@car@low@casual@ds", "gesture_chat", 5f, -1, AnimationFlags.None);
                                driver2.Task.PlayAnimation("missheistdockssetup1leadinoutig_1", "lsdh_ig_1_argue_wade");
                                if (listdriver2[i].Contains("~f~Förare"))
                                    driver2.PlayAmbientSpeech("CHAT_STATE", SpeechModifier.Standard);
                                if (listdriver2[i].Contains("~f~Polis"))
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
                            ShowNetworkedNotification("~y~Oroa dig inte~s~! Den här länktexten ~f~autoclean~s~ på 60 sekunder", "CHAR_CALL911", "CHAR_CALL911", "RLC", "Försäkran", 10f);
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
