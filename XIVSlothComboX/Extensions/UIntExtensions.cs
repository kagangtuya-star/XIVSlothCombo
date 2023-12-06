using XIVSlothComboX.CustomComboNS.Functions;

namespace XIVSlothComboX.Extensions
{
    internal static class UIntExtensions
    {
        internal static bool LevelChecked(this uint value) => CustomComboFunctions.LevelChecked(value);
        internal static bool ActionReady(this uint value) => CustomComboFunctions.ActionReady(value);
        internal static bool GCDActionReady(this uint value,uint gcdActionId)
        {
            if (!CustomComboFunctions.LevelChecked(value))
            {
                return false;
            }

            if (CustomComboFunctions.GetCooldownRemainingTime(value) <= 0 ||
                ( CustomComboFunctions.GetCooldownRemainingTime(value) <= CustomComboFunctions.GetCooldownRemainingTime(gcdActionId) 
                  && CustomComboFunctions.GetCooldownRemainingTime(gcdActionId) < 1.5f ))
            {
                return true;
            } 
            return false;
        }
        
        
        
    }
}
