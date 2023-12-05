using XIVSlothComboX.CustomComboNS.Functions;

namespace XIVSlothComboX.Combos.PvE;

public class RaidBuff
{
    internal const ushort

        // Heals
        强化药 = 49,
        灼热之光 = 2703,
        
        连环计 = 1221,
        
        
        
        //忍者夺取
        攻其不备 = 3254,
        受伤加重 = 638,
        
        技巧舞步结束TechnicalFinish = 1822,
        
        留空 = 0;


    public static bool 爆发期()
    {
        if (CustomComboFunctions.HasEffect(强化药))
        {
            return true;
        }
        
        if (CustomComboFunctions.HasEffect(灼热之光))
        {
            return true;
        }  
        
        if (CustomComboFunctions.HasEffect(技巧舞步结束TechnicalFinish))
        {
            return true;
        }
        
        
        if (CustomComboFunctions.FindTargetEffectAny(攻其不备) is not null)
        {
            return true;
        }
        
        if (CustomComboFunctions.FindTargetEffectAny(连环计) is not null)
        {
            return true;
        }
        
        return false;
    }
}