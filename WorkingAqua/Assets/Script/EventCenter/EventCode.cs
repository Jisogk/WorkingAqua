/// <summary>
/// 事件码
/// 指定事件类型
/// 总有一种这东西要写一百行的感觉（
/// </summary>

public enum EventCode
{
    //游戏事件类型
    OnGetItem, //得到道具的事件
    OnPause, //游戏暂停事件
    OnGoldChange, //金钱改变的事件
    OnManagerInitFinish, //单例初始化完成事件，用于安排初始化顺序，之后改

    //GUI响应相关类型
    OnBagOpen, //打开背包界面的事件
    OnEquipOpen, //打开装备界面的事件
    OnBagUIUpdate, //刷新背包显示的事件
    OnShopOpen, //打开商店界面的事件
    ShowWarningMsg, //显示提示信息的事件
}
