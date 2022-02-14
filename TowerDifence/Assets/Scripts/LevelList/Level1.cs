using IJunior.TypedScenes;
using System.Collections.Generic;

public class Level1 : BaceLevel
{
    protected override void OnChoiseLevelClick()
    {
        LVL1.Load(LevelConfig);
    }
}
