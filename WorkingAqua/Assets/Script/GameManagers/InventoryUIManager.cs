using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public DragItemUI dragItemUI; //实现拖拽功能的UI

    public bool isDrag = false;  //是否在拖拽中
    public bool isShow = false;  //是否显示物品详细信息

    public Vector2 MousePos = Vector2.one; //鼠标在cavans上的位置

    public Canvas canvas;  //UI的cavans

    //没办法的临时变量
    private GridUI prevgrid;
    private GridUI entergrid;

    // Use this for initialization
    void Awake()
    {
        GridUI.OnEnter += GridUI_OnEnter;
        GridUI.OnExit += GridUI_OnExit;
        GridUI.OnLeftBeginDrag += GridUI_OnLeftBeginDrag;
        GridUI.OnLeftEndDrag += GridUI_OnLeftEndDrag;
    }

    // Update is called once per frame
    void Update()
    {

        if (isDrag)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), Input.mousePosition, null, out MousePos);
            dragItemUI.setRectPosition(MousePos);
        }
    }

    /// <summary>
    /// 对事件OnLeftBeginDrag的响应
    /// 在物品被拖动时执行
    /// </summary>
    /// <param name="gridTransfrom"></param>
    public void GridUI_OnLeftBeginDrag(Transform gridTransfrom)
    {
        //判断起始格子
        if (gridTransfrom.gameObject.GetComponent<GridUI>().Type.Equals(GridUI.GridType.Bag))//如果是背包格，从背包中读取物品
        {
            if (InventoryManager.GetItem(gridTransfrom.gameObject.GetComponent<GridUI>().Index) == null)
            {
                return;
            }
            else
            {
                Item item = InventoryManager.GetItem(gridTransfrom.gameObject.GetComponent<GridUI>().Index);
                dragItemUI.UpdateItemImage(item.Icon);
                isDrag = true;
                gridTransfrom.gameObject.GetComponent<GridUI>().ItemUIGo.SetActive(false); //隐藏原有物品
                dragItemUI.Show();
            }
        }
        else if (gridTransfrom.gameObject.GetComponent<GridUI>().Type.Equals(GridUI.GridType.Shop)) //如果是商店格，从商店中读取物品
        {
            if (ShopManager.instance.GetItem(gridTransfrom.gameObject.GetComponent<GridUI>().Index) == null)
            {
                return;
            }
            else
            {
                Item item = ShopManager.instance.GetItem(gridTransfrom.gameObject.GetComponent<GridUI>().Index);
                dragItemUI.UpdateItemImage(item.Icon);
                isDrag = true;
                //商店的东西不隐藏（临时设定）
                //gridTransfrom.gameObject.GetComponent<GridUI>().ItemUIGo.SetActive(false);
                dragItemUI.Show();
            }
        }
    }

    /// <summary>
    /// 对事件OnLeftEndDrag的响应
    /// 在物品拖动结束时执行
    /// </summary>
    /// <param name="prevGridTransfrom"></param>
    /// <param name="enterTransfrom"></param>
    public void GridUI_OnLeftEndDrag(Transform prevGridTransfrom, Transform enterTransfrom)
    {
        isDrag = false;
        dragItemUI.Hide();

        //判断是否进入了有意义的格子
        if (enterTransfrom == null)
        {
            //prevGridTransfrom.gameObject.GetComponent<GridUI>().ItemUIGo.SetActive(true);
        }
        else
        {
            if (enterTransfrom.tag == "Grid")//假如进入了格子
            {
                //获取前后两个GridUI的引用
                prevgrid = prevGridTransfrom.gameObject.GetComponent<GridUI>();
                entergrid = enterTransfrom.gameObject.GetComponent<GridUI>();

                //判断起始格
                if (prevgrid.Type.Equals(GridUI.GridType.Bag) && InventoryManager.GetItem(prevgrid.Index) != null)//如果是背包格且该格有物品
                {
                    //判断进入格
                    if (entergrid.Type.Equals(GridUI.GridType.Bag))//如果进入背包格，则交换位置
                    {
                        InventoryManager.SwitchItem(prevgrid.Index, entergrid.Index);
                    }
                    else if (entergrid.Type.Equals(GridUI.GridType.Shop))//如果是商店格，则调用方法出售道具
                    {
                        string warnStr = string.Format("即将以 {0} Gold 出售 ", InventoryManager.GetItem(prevgrid.Index).BasePrice) + InventoryManager.GetItem(prevgrid.Index).Name + " \n是否确定";
                        EventCenter.Broadcast<string, WarningMsg.TargetMethod>(EventCode.ShowWarningMsg, warnStr, SellItem);
                    }
                    else if (entergrid.Type.Equals(GridUI.GridType.Hand)) //如果是手部装备格
                    {
                        if (InventoryManager.GetItem(prevgrid.Index).Type.Equals(Item.ItemType.Tool) || InventoryManager.GetItem(prevgrid.Index).Type.Equals(Item.ItemType.Weapon)) //物品必须是工具或武器
                        {
                            if (StateManager.instance.Hand == null) //如果手上没东西，直接装备
                            {
                                StateManager.instance.Hand = InventoryManager.GetItem(prevgrid.Index);
                                Debug.Log(StateManager.instance.Hand.Name);
                                InventoryManager.DeleteItem(prevgrid.Index);
                            }
                            else //如果手上有东西，交换
                            {
                                Item buf = InventoryManager.GetItem(prevgrid.Index);
                                InventoryManager.DeleteItem(prevgrid.Index);
                                InventoryManager.SaveItem(StateManager.instance.Hand);
                                StateManager.instance.Hand = buf;
                            }
                        }
                    }
                    else //其他地方无事发生
                    { }
                }
                else if (prevgrid.Type.Equals(GridUI.GridType.Shop) && ShopManager.instance.GetItem(prevgrid.Index) != null)//如果是商店格且该格有物品
                {
                    //判断进入格
                    if (entergrid.Type.Equals(GridUI.GridType.Bag))//如果进入背包格，则调用方法购买道具
                    {
                        string warnStr = string.Format("即将以 {0} Gold 购买 ", ShopManager.instance.GetItem(prevgrid.Index).BasePrice) + ShopManager.instance.GetItem(prevgrid.Index).Name + " \n是否确定";
                        EventCenter.Broadcast<string, WarningMsg.TargetMethod>(EventCode.ShowWarningMsg, warnStr, BuyItem);
                    }
                    else //其他格子无事发生
                    { }
                }
            }
            else
            {
                //prevGridTransfrom.gameObject.GetComponent<GridUI>().ItemUIGo.SetActive(true);
            }
        }


        EventCenter.Broadcast(EventCode.OnBagUIUpdate);
    }

    /// <summary>
    /// 对事件OnEnter的响应
    /// 光标进入物品格时执行
    /// </summary>
    /// <param name="transform"></param>
    public void GridUI_OnEnter(Transform gridTransfrom)
    {
        Item item = InventoryManager.GetItem(gridTransfrom.gameObject.GetComponent<GridUI>().Index);

        if (item == null)
        {
            return;
        }
    }

    /// <summary>
    /// 对事件OnExit的响应
    /// 光标离开物品格时执行
    /// </summary>
    public void GridUI_OnExit()
    { }

    /// <summary>
    /// 出售物品
    /// </summary>
    /// <param name="item"></param>
    public void SellItem()
    {
        InventoryManager.instance.SellItem(prevgrid.Index);
        EventCenter.Broadcast(EventCode.OnBagUIUpdate);
    }

    /// <summary>
    /// 购买物品
    /// </summary>
    public void BuyItem()
    {
        InventoryManager.instance.BuyItem(ShopManager.instance.GetItem(prevgrid.Index), entergrid.Index);
        EventCenter.Broadcast(EventCode.OnBagUIUpdate);
    }
}
