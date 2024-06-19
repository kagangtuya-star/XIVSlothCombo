using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dalamud;
using Dalamud.Game;
using Dalamud.Game.ClientState.Objects.Enums;
using Dalamud.Game.ClientState.Objects.SubKinds;
using Dalamud.Game.ClientState.Objects.Types;
using Dalamud.Game.Command;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using Dalamud.Memory;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.Game;
using XIVSlothComboX.Combos;
using XIVSlothComboX.Combos.PvE;
using XIVSlothComboX.Core;
using XIVSlothComboX.CustomComboNS.Functions;
using XIVSlothComboX.Data;
using XIVSlothComboX.Extensions;
using XIVSlothComboX.Services;
using XIVSlothComboX.Window;
using XIVSlothComboX.Window.Tabs;
using Status = Dalamud.Game.ClientState.Statuses.Status;

namespace XIVSlothComboX
{
    /// <summary> Main plugin implementation. </summary>
    public sealed partial class XIVSlothComboX : IDalamudPlugin
    {
        private const string Command = "/scombo";

        private readonly ConfigWindow _ConfigWindow;

        private readonly TextPayload starterMotd = new("[Sloth Message of the Day] ");


        private uint autoActionId = 0;

        // private bool isAuto = false;

        private CancellationTokenSource autoTokenSource = new(); // 令牌对象
        private CancellationToken autoToken;


        /// <summary> Initializes a new instance of the <see cref="XIVSlothComboX"/> class. </summary>
        /// <param name="pluginInterface"> Dalamud plugin interface. </param>
        public XIVSlothComboX(DalamudPluginInterface pluginInterface)
        {
            pluginInterface.Create<Service>();

            Service.Configuration = pluginInterface.GetPluginConfig() as PluginConfiguration ?? new PluginConfiguration();
            Service.Address = new PluginAddressResolver();
            Service.Address.Setup(Service.SigScanner);

            if (Service.Configuration.Version == 4)
                UpgradeConfig4();

            Service.ComboCache = new CustomComboCache();
            Service.IconReplacer = new IconReplacer();
            ActionWatching.Enable();
            Combos.JobHelpers.AST.Init();

            _ConfigWindow = new();

            Service.Interface.UiBuilder.Draw += DrawUI;
            Service.Interface.UiBuilder.OpenConfigUi += OnOpenConfigUi;
            Service.Interface.UiBuilder.OpenMainUi += OpenMainUi;

            Service.CommandManager.AddHandler(Command,
                new CommandInfo(OnCommand)
                {
                    HelpMessage = "Open a window to edit custom combo settings.",
                    ShowInHelp = true,
                });

            Service.ClientState.Login += PrintLoginMessage;
            // Service.ClientState.Login += TestFeatures.function;

            if (Service.ClientState.IsLoggedIn)
            {
                ResetFeatures();
            }

            // Service.Framework.Update += OnFramework;

            autoToken = autoTokenSource.Token; // 开关绑

            // TestFeatures.function();
            KillRedundantIDs();

            Service.IconManager = new IconManager();


        }


        private static void KillRedundantIDs()
        {
            List<int> redundantIDs = Service.Configuration.EnabledActions.Where(x => int.TryParse(x.ToString(), out _)).OrderBy(x => x).Cast<int>()
                .ToList();
            foreach (int id in redundantIDs)
            {
                Service.Configuration.EnabledActions.RemoveWhere(x => (int)x == id);
            }

            Service.Configuration.Save();
        }

        private void ResetFeatures()
        {
            // Enumerable.Range is a start and count, not a start and end.
            // Enumerable.Range(Start, Count)
            // Service.Configuration.ResetFeatures("v3.0.17.0_NINRework", Enumerable.Range(10000, 100).ToArray());
            // Service.Configuration.ResetFeatures("v3.0.17.0_DRGCleanup", Enumerable.Range(6100, 400).ToArray());
            // Service.Configuration.ResetFeatures("v3.0.18.0_GNBCleanup", Enumerable.Range(7000, 700).ToArray());
        }

        private void DrawUI()
        {
            _ConfigWindow.Draw();
        }

        private void OpenMainUi()
        {
            _ConfigWindow.Visible = true;
        }

        private void OnOpenConfigUi()
        {
            _ConfigWindow.Visible = !_ConfigWindow.Visible;
        }

        private void PrintLoginMessage()
        {
            // Task.Delay(TimeSpan.FromSeconds(5)).ContinueWith(task => ResetFeatures());
            if (!Service.Configuration.HideMessageOfTheDay)
                Task.Delay(TimeSpan.FromSeconds(3)).ContinueWith(task => PrintMotD());
        }

        private void PrintMotD()
        {
            Service.ChatGui.PrintError(AboutUs.Ban);
            // Service.ChatGui.PrintError($"[XIVSlothComboX] 和 [XIVSlothCombo]冲突，请关闭或者删掉一个");
        }

        /// <inheritdoc/>
        public string Name => "XIVSlothComboX";

        /// <inheritdoc/>
        public void Dispose()
        {
            autoActionId = 0;
            // isAuto = false;


            autoTokenSource.Cancel();

            _ConfigWindow?.Dispose();

            Service.CommandManager.RemoveHandler(Command);

            Service.Interface.UiBuilder.OpenConfigUi -= OnOpenConfigUi;
            Service.Interface.UiBuilder.Draw -= DrawUI;

            Service.IconReplacer?.Dispose();
            Service.ComboCache?.Dispose();
            Service.IconManager?.Dispose();
            ActionWatching.Dispose();


            Combos.JobHelpers.AST.Dispose();

            // Service.Framework.Update -= OnFramework;

            Service.ClientState.Login -= PrintLoginMessage;
            // Service.ClientState.Login -= TestFeatures.function;

            // TestFeatures.Dispose();
        }


        private void OnCommand(string command, string arguments)
        {
            string[]? argumentsParts = arguments.Split();
            var setOutChat = Service.Configuration.SetOutChat;
            PlayerCharacter? localPlayer = Service.ClientState.LocalPlayer;

            // Service.ChatGui.Print(argumentsParts[0].ToLower());

            switch (argumentsParts[0].ToLower())
            {
                case "unsetall": // unset all features
                {
                    foreach (CustomComboPreset preset in Enum.GetValues<CustomComboPreset>())
                    {
                        Service.Configuration.EnabledActions.Remove(preset);
                    }

                    Service.ChatGui.Print("All UNSET");
                    Service.Configuration.Save();
                    break;
                }

                case "set_custom_int_value": // unset all features
                {
                    string? targetPreset = argumentsParts[1].ToString();
                    int.TryParse(argumentsParts[2].ToString(), out int oder);
                    PluginConfiguration.SetCustomIntValue(targetPreset, oder);
                    Service.Configuration.Save();
                    break;
                }

                case "set_smn": // unset all features
                {
                    int.TryParse(argumentsParts[1].ToString(), out int oder);
                    PluginConfiguration.SetCustomIntValue(SMN.Config.召唤顺序, oder);
                    Service.Configuration.Save();
                    break;
                }


                case "set": // set a feature
                {
                    // if (!Service.Condition[Dalamud.Game.ClientState.Conditions.ConditionFlag.InCombat])
                    {
                        string? targetPreset = argumentsParts[1].ToLowerInvariant();
                        foreach (CustomComboPreset preset in Enum.GetValues<CustomComboPreset>())
                        {
                            if (preset.ToString().ToLowerInvariant() != targetPreset)
                                continue;

                            Service.Configuration.EnabledActions.Add(preset);
                            if (setOutChat)
                            {
                                Service.ChatGui.Print($"{preset} SET");
                            }
                        }

                        Service.Configuration.Save();
                    }

                    // else
                    // {
                    //     Service.ChatGui.PrintError("Features cannot be set in combat.");
                    // }

                    break;
                }

                case "toggle": // toggle a feature
                {
                    // if (!Service.Condition[Dalamud.Game.ClientState.Conditions.ConditionFlag.InCombat])
                    {
                        string? targetPreset = argumentsParts[1].ToLowerInvariant();
                        foreach (CustomComboPreset preset in Enum.GetValues<CustomComboPreset>())
                        {
                            if (preset.ToString().ToLowerInvariant() != targetPreset)
                                continue;

                            if (Service.Configuration.EnabledActions.Contains(preset))
                            {
                                Service.Configuration.EnabledActions.Remove(preset);

                                if (setOutChat)
                                {
                                    Service.ChatGui.Print($"{preset} UNSET");
                                }
                            }

                            else
                            {
                                Service.Configuration.EnabledActions.Add(preset);
                                if (setOutChat)
                                {
                                    Service.ChatGui.Print($"{preset} SET");
                                }
                            }
                        }

                        Service.Configuration.Save();
                    }

                    // else
                    // {
                    //     Service.ChatGui.PrintError("Features cannot be toggled in combat.");
                    // }

                    break;
                }

                case "unset": // unset a feature
                {
                    // if (!Service.Condition[Dalamud.Game.ClientState.Conditions.ConditionFlag.InCombat])
                    {
                        string? targetPreset = argumentsParts[1].ToLowerInvariant();
                        foreach (CustomComboPreset preset in Enum.GetValues<CustomComboPreset>())
                        {
                            if (preset.ToString().ToLowerInvariant() != targetPreset)
                                continue;

                            Service.Configuration.EnabledActions.Remove(preset);
                            if (setOutChat)
                            {
                                Service.ChatGui.Print($"{preset} UNSET");
                            }
                        }

                        Service.Configuration.Save();
                    }

                    // else
                    // {
                    //     Service.ChatGui.PrintError("Features cannot be unset in combat.");
                    // }

                    break;
                }

                case "list": // list features
                {
                    string? filter = argumentsParts.Length > 1 ? argumentsParts[1].ToLowerInvariant() : "all";

                    if (filter == "set") // list set features
                    {
                        foreach (bool preset in Enum.GetValues<CustomComboPreset>().Select(preset => Service.Configuration.IsEnabled(preset)))
                        {
                            if (setOutChat)
                            {
                                Service.ChatGui.Print(preset.ToString());
                            }
                        }
                    }

                    else if (filter == "unset") // list unset features
                    {
                        foreach (bool preset in Enum.GetValues<CustomComboPreset>().Select(preset => !Service.Configuration.IsEnabled(preset)))
                        {
                            if (setOutChat)
                            {
                                Service.ChatGui.Print(preset.ToString());
                            }
                        }
                    }

                    else if (filter == "all") // list all features
                    {
                        foreach (CustomComboPreset preset in Enum.GetValues<CustomComboPreset>())
                        {
                            if (setOutChat)
                            {
                                Service.ChatGui.Print(preset.ToString());
                            }
                        }
                    }

                    else
                    {
                        Service.ChatGui.PrintError("Available list filters: set, unset, all");
                    }

                    break;
                }

                case "enabled": // list all currently enabled features
                {
                    foreach (CustomComboPreset preset in Service.Configuration.EnabledActions.OrderBy(x => x))
                    {
                        if (int.TryParse(preset.ToString(), out int pres)) continue;
                        if (setOutChat)
                        {
                            Service.ChatGui.Print($"{(int)preset} - {preset}");
                        }
                    }

                    break;
                }

                case "debug": // debug logging
                {
                    try
                    {
                        string? specificJob = argumentsParts.Length == 2 ? argumentsParts[1].ToLower() : "";

                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                        using StreamWriter file = new($"{desktopPath}/SlothDebug.txt", append: false); // Output path

                        file.WriteLine("START DEBUG LOG");
                        file.WriteLine("");
                        file.WriteLine($"Plugin Version: {GetType().Assembly.GetName().Version}"); // Plugin version
                        file.WriteLine("");
                        file.WriteLine($"Installation Repo: {RepoCheckFunctions.FetchCurrentRepo()?.InstalledFromUrl}"); // Installation Repo
                        file.WriteLine("");
                        file.WriteLine($"Current Job: "
                                       + // Current Job
                                       $"{localPlayer.ClassJob.GameData.Name} / "
                                       + // - Client Name
                                       $"{localPlayer.ClassJob.GameData.NameEnglish} / "
                                       + // - EN Name
                                       $"{localPlayer.ClassJob.GameData.Abbreviation}"); // - Abbreviation
                        file.WriteLine($"Current Job Index: {localPlayer.ClassJob.GameData.JobIndex}"); // Job Index
                        file.WriteLine("");
                        file.WriteLine($"Current Zone: {Service.ClientState.TerritoryType}"); // Current zone location
                        file.WriteLine($"Current Party Size: {Service.PartyList.Length}"); // Current party size
                        file.WriteLine("");
                        file.WriteLine($"START ENABLED FEATURES");

                        int i = 0;
                        if (string.IsNullOrEmpty(specificJob))
                        {
                            foreach (CustomComboPreset preset in Service.Configuration.EnabledActions.OrderBy(x => x))
                            {
                                if (int.TryParse(preset.ToString(), out _))
                                {
                                    i++;
                                    continue;
                                }

                                file.WriteLine($"{(int)preset} - {preset}");
                            }
                        }

                        else
                        {
                            foreach (CustomComboPreset preset in Service.Configuration.EnabledActions.OrderBy(x => x))
                            {
                                if (int.TryParse(preset.ToString(), out _))
                                {
                                    i++;
                                    continue;
                                }

                                if (preset.ToString()[..3].ToLower() == specificJob
                                    || // Job identifier
                                    preset.ToString()[..3].ToLower() == "all"
                                    || // Adds in Globals
                                    preset.ToString()[..3].ToLower() == "pvp") // Adds in PvP Globals
                                    file.WriteLine($"{(int)preset} - {preset}");
                            }
                        }

                        file.WriteLine($"END ENABLED FEATURES");
                        file.WriteLine("");
                        file.WriteLine($"Redundant IDs found: {i}");

                        if (i > 0)
                        {
                            file.WriteLine($"START REDUNDANT IDs");
                            foreach (CustomComboPreset preset in Service.Configuration.EnabledActions.Where(x => int.TryParse(x.ToString(), out _))
                                         .OrderBy(x => x))
                            {
                                file.WriteLine($"{(int)preset}");
                            }

                            file.WriteLine($"END REDUNDANT IDs");
                            file.WriteLine("");
                        }

                        file.WriteLine($"Status Effect Count: {localPlayer.StatusList.Count(x => x != null)}");

                        if (localPlayer.StatusList.Length > 0)
                        {
                            file.WriteLine($"START STATUS EFFECTS");
                            foreach (Status? status in localPlayer.StatusList)
                            {
                                file.WriteLine($"ID: {status.StatusId}, COUNT: {status.StackCount}, SOURCE: {status.SourceId}");
                            }

                            file.WriteLine($"END STATUS EFFECTS");
                        }

                        file.WriteLine("END DEBUG LOG");
                        Service.ChatGui.Print("Please check your desktop for SlothDebug.txt and upload this file where requested.");

                        break;
                    }

                    catch (Exception ex)
                    {
                        Dalamud.Logging.PluginLog.Error(ex, "Debug Log");
                        Service.ChatGui.Print("Unable to write Debug log.");
                        break;
                    }
                }

                case "action":
                {
                    autoActionId = 0;
                    try
                    {
                        autoActionId = uint.Parse(argumentsParts[1]);
                    }
                    catch (Exception exception)
                    {
                        autoActionId = 0;
                        Dalamud.Logging.PluginLog.Error(exception, "Debug Log");
                    }


                    break;
                }

                case "auto1":
                {
                    autoActionId = 0;
                    autoTokenSource.Cancel();

                    if (localPlayer == null)
                        return;

                    switch (localPlayer.ClassJob.Id)
                    {
                        case GNB.JobID:
                        {
                            autoActionId = GNB.利刃斩KeenEdge;
                            break;
                        }

                        case DRK.JobID:
                        {
                            autoActionId = DRK.Souleater;
                            break;
                        }

                        case PLD.JobID:
                        {
                            autoActionId = PLD.先锋剑FastBlade;
                            break;
                        }

                        case WAR.JobID:
                        {
                            autoActionId = WAR.StormsPath;
                            break;
                        }

                        case DNC.JobID:
                        {
                            autoActionId = DNC.瀑泻Cascade;
                            break;
                        }

                        case MCH.JobID:
                        {
                            autoActionId = MCH.分裂弹SplitShot;
                            break;
                        }
                    }

                    if (autoActionId != 0)
                    {
                        autoTokenSource = new();
                        autoToken = autoTokenSource.Token;
                    }

                    Task.Run(async delegate
                        {
                            while (!autoToken.IsCancellationRequested)
                            {
                                Random random = new Random();
                                var interval = random.Next(32, 64);

                                await Service.Framework.RunOnFrameworkThread(() =>
                                {
                                    if (!Service.ClientState.IsLoggedIn)
                                    {
                                        autoActionId = 0;
                                        autoTokenSource.Cancel();
                                    }

                                    unsafe
                                    {
                                        var targetObjectId = localPlayer.TargetObjectId;
                                        if (autoActionId != 0)
                                        {
                                            if (targetObjectId != 0)
                                            {
                                                GameObject? targetObject = Service.ClientState.LocalPlayer.TargetObject;

                                                if (targetObject != null && targetObject is BattleChara battleChara)
                                                {
                                                    if (battleChara.ObjectKind == ObjectKind.BattleNpc)
                                                    {
                                                        {
                                                            ActionManager.Instance()->UseAction(ActionType.Action, autoActionId, targetObjectId);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                });
                                await Task.Delay(TimeSpan.FromMilliseconds(interval));
                            }
                        },
                        autoToken);
                    break;
                }

                case "auto0":
                {
                    // isAuto = false;
                    autoActionId = 0;
                    autoTokenSource.Cancel();

                    break;
                }

                case "fakename":
                {
                    if (argumentsParts.Length > 1)
                    {
                        if (localPlayer != null)
                        {
                            unsafe
                            {
                                FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject* Struct =
                                    (FFXIVClientStructs.FFXIV.Client.Game.Object.GameObject*)localPlayer.Address;
                                SafeMemory.WriteBytes((IntPtr)Struct->Name, SeStringUtils.NameText(argumentsParts[1]));
                            }
                        }
                    }

                    break;
                }
                
                case "useitem":
                {
                    Dalamud.Logging.PluginLog.Error("1");
                    CustomComboFunctions.Useitem(4551);

                    break;
                }

                default:
                {
                    _ConfigWindow.Visible = !_ConfigWindow.Visible;
                    PvEFeatures.HasToOpenJob = true;
                    if (argumentsParts[0].Length > 0)
                    {
                        var jobname = ConfigWindow.groupedPresets.Where(x =>
                                x.Value.Any(y => y.Info.JobShorthand.Equals(argumentsParts[0].ToLower(), StringComparison.CurrentCultureIgnoreCase)))
                            .FirstOrDefault().Key;
                        var header = $"{jobname} - {argumentsParts[0].ToUpper()}";
                        Dalamud.Logging.PluginLog.Debug($"{jobname}");
                        PvEFeatures.HeaderToOpen = header;
                    }

                    break;
                }
            }

            Service.Configuration.Save();
        }

        private static void UpgradeConfig4()
        {
            Service.Configuration.Version = 5;
            Service.Configuration.EnabledActions = Service.Configuration.EnabledActions4.Select(preset => (int)preset switch
                {
                    27 => 3301,
                    75 => 3302,
                    73 => 3303,
                    25 => 2501,
                    26 => 2502,
                    56 => 2503,
                    70 => 2504,
                    71 => 2505,
                    110 => 2506,
                    95 => 2507,
                    41 => 2301,
                    42 => 2302,
                    63 => 2303,
                    74 => 2304,
                    33 => 3801,
                    31 => 3802,
                    34 => 3803,
                    43 => 3804,
                    50 => 3805,
                    72 => 3806,
                    103 => 3807,
                    44 => 2201,
                    0 => 2202,
                    1 => 2203,
                    2 => 2204,
                    3 => 3201,
                    4 => 3202,
                    57 => 3203,
                    85 => 3204,
                    20 => 3701,
                    52 => 3702,
                    96 => 3703,
                    97 => 3704,
                    22 => 3705,
                    30 => 3706,
                    83 => 3707,
                    84 => 3708,
                    23 => 3101,
                    24 => 3102,
                    47 => 3103,
                    58 => 3104,
                    66 => 3105,
                    102 => 3106,
                    54 => 2001,
                    82 => 2002,
                    106 => 2003,
                    17 => 3001,
                    18 => 3002,
                    19 => 3003,
                    87 => 3004,
                    88 => 3005,
                    89 => 3006,
                    90 => 3007,
                    91 => 3008,
                    92 => 3009,
                    107 => 3010,
                    108 => 3011,
                    5 => 1901,
                    6 => 1902,
                    59 => 1903,
                    7 => 1904,
                    55 => 1905,
                    86 => 1906,
                    69 => 1907,
                    48 => 3501,
                    49 => 3502,
                    68 => 3503,
                    53 => 3504,
                    93 => 3505,
                    101 => 3506,
                    94 => 3507,
                    11 => 3401,
                    12 => 3402,
                    13 => 3403,
                    14 => 3404,
                    15 => 3405,
                    81 => 3406,
                    60 => 3407,
                    61 => 3408,
                    64 => 3409,
                    65 => 3410,
                    109 => 3411,
                    29 => 2801,
                    37 => 2802,
                    39 => 2701,
                    40 => 2702,
                    8 => 2101,
                    9 => 2102,
                    10 => 2103,
                    78 => 2104,
                    79 => 2105,
                    67 => 2106,
                    104 => 2107,
                    35 => 2401,
                    36 => 2402,
                    76 => 2403,
                    77 => 2404,
                    _ => 0,
                })
                .Where(id => id != 0)
                .Select(id => (CustomComboPreset)id)
                .ToHashSet();
            Service.Configuration.EnabledActions4 = new();
            Service.Configuration.Save();
        }
    }
}