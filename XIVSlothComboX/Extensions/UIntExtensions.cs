using XIVSlothComboX.CustomComboNS.Functions;

namespace XIVSlothComboX.Extensions
{
    internal static class UIntExtensions
    {
        internal static bool LevelChecked(this uint value) => CustomComboFunctions.LevelChecked(value);
        internal static bool ActionReady(this uint value) => CustomComboFunctions.ActionReady(value);
    }
}
