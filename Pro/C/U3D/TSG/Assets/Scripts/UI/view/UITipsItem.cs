using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITipsItem : UIBase
{
    public override UILayerType PanelLayerType { get { return UILayerType.Tips; } }
    public Text contentText;
    public override void Init()
    {
        base.Init();

    }
    private float showTime = 0;
    public void Show(string content)
    {
        contentText.text = content;
        showTime = 2;
    }
    public void OnUpdate(float time)
    {
        showTime -= time;
        if (showTime<=0)
        {
            CSTipsManager.Singleton.Hide(this);
        }
    }
}
