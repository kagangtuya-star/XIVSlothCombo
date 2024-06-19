using System;
using System.Collections.Generic;
using XIVSlothComboX.CustomComboNS.Functions;

namespace XIVSlothComboX.Data;

public class CustomTimeline
{
    public int Index;
    
    public bool Enable;

    public  uint JobId;
    
    public  string Name;

    public List<CustomAction> ActionList=new List<CustomAction>();

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(obj, this)) return true;
        if (ReferenceEquals(obj, null)) return false;
        if (obj is CustomTimeline tCustomTimeline)
        {
            return JobId == tCustomTimeline.JobId && Name == tCustomTimeline.Name;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(JobId, Name);
    }
}

public class CustomAction
{
    public uint ActionId;

    public double UseTimeStart;
    public double UseTimeEnd;
    
    public int TargetType;

    public byte CustomActionType = CustomType.序列;
}

internal static class CustomType
{
    internal  const byte
        序列 = 1,
        时间 = 2;
}


