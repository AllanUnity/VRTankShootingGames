using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowSight : UIBase
{
    public RawImage sight;
    public override void Show()
    {
        base.Show();
        sight.texture = CSGame.Instance.tkController.sightTexture;
    }
}
