using System.Collections.Generic;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.Game;
using XIVSlothComboX.Core;
using XIVSlothComboX.Data;

namespace XIVSlothComboX.CustomComboNS.Functions
{
    internal abstract partial class CustomComboFunctions
    {
        public const string UseCustomTimeKey = "UseCustomTimeKey";

        public static CustomTimeline? _CustomTimeline;


        /**
         * 当前自定义
         */
        public static readonly List<CustomAction> 序列轴 = new();

        public static readonly List<CustomAction> 时间轴 = new();


        public static void InitCustomTimeline()
        {
            var customIntValue = PluginConfiguration.GetCustomIntValue(UseCustomTimeKey, -1);
            if (customIntValue != -1)
            {
                foreach (var customTimeline in PluginConfiguration.CustomTimelineList)
                {
                    if (customTimeline.Index == customIntValue)
                    {
                        LoadCustomTime(customTimeline);
                    }
                }
            }

        }
        
        public static void LoadCustomTime(CustomTimeline tCustomAction)
        {
            ResetCustomTime();
            
            PluginConfiguration.SetCustomIntValue(UseCustomTimeKey, tCustomAction.Index);
            _CustomTimeline = tCustomAction;
            

            foreach (var customAction in tCustomAction.ActionList)
            {
                switch (customAction.CustomActionType)
                {
                    case CustomType.序列:
                    {
                        序列轴.Add(customAction);
                        break;
                    }

                    case CustomType.时间:
                    {
                        序列轴.Add(customAction);
                        break;
                    }
                }
            }
        }


        public static void ResetCustomTime()
        {
            PluginConfiguration.SetCustomIntValue(UseCustomTimeKey, -1);
            _CustomTimeline = null;
            时间轴.Clear();
            序列轴.Clear();
            ActionWatching.CustomList.Clear();
        }

        public static CustomAction? CustomTimelineFindBy时间轴(uint actionId)
        {
            var seconds = CombatEngageDuration().TotalSeconds;
            foreach (var customAction in 时间轴)
            {
                if (customAction.ActionId == actionId)
                {
                    if (customAction.UseTimeStart < seconds && seconds < customAction.UseTimeEnd)
                    {
                        return customAction;
                    }
                }
            }

            return null;
        }


        public static bool CustomTimelineIsEnable()
        {
            if (LocalPlayer == null)
                return false;

            if (_CustomTimeline == null)
            {
                return false;
            }

            if (LocalPlayer.ClassJob.Id == _CustomTimeline.JobId)
            {
                return true;
            }

            return false;
        }
        
        
        public static unsafe bool Useitem( uint itemId)
        {
            uint a4 = 65535;
            if (InventoryManager.Instance()->GetInventoryItemCount(itemId, true) > 0)
            {
                return ActionManager.Instance()->UseAction(ActionType.Item, itemId + 1000000, LocalPlayer.ObjectId, a4);
            }

            return ActionManager.Instance()->UseAction(ActionType.Item, itemId, LocalPlayer.ObjectId, a4);
        }
    }
}