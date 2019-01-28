/// <summary>UI层级</summary>
public enum UILayerType
{
    /// <summary>底层</summary>//基本没有
    Base,
    /// <summary>最底层</summary>//基本没有
    Resident,
    /// <summary>界面层</summary>//主界面
    Panel,
    /// <summary>窗口层</summary>//各种一级窗口
    Window,
    /// <summary>次级窗口层</summary>
    SecondaryWindow,
    /// <summary>提示层</summary>
    Tips,
    /// <summary>新手引导层</summary>
    Guide,
    /// <summary>界面最顶层</summary>
    TopWindow,
    /// <summary> 断线重连</summary>
    Connect,
    /// <summary>弹框提示</summary>
    Hint,
}