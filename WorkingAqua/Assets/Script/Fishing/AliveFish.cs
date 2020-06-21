using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveFish : MonoBehaviour
{
    public Item Fish;
    public SpriteRenderer TheRim;
    public SpriteRenderer TheSector;

    // Use this for initialization
    void Awake()
    {
        GenerateRimSprite(TheRim, 60, 0.7f, Color.white);

        GenerateSectorSprite(TheSector, 60, 0.7f, 0.5f, Color.green);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 生成环形
    /// </summary>
    /// <param name="sr"></param> 本地的sprite渲染器
    /// <param name="size"></param> 外直径，其实是对应正方形Texture2D的边长，单位为像素
    /// <param name="emptyRate"></param> 留白率，指中心空白圆半径与大圆半径之比
    /// <param name="color"></param> 颜色，指圆环的颜色，一般取白色
    public void GenerateRimSprite(SpriteRenderer sr, int size, float emptyRate, Color color)
    {
        float radius = size / 2;
        float innerRadius = emptyRate * radius;
        Texture2D t = new Texture2D(size, size); //生成Texture2D
        Vector2 center = new Vector2(radius, radius); //生成圆心坐标

        //遍历每个像素点
        for (int w = 0; w < size; w++)
        {
            for (int h = 0; h < size; h++)
            {
                Color c;
                Vector2 v = new Vector2(w, h) - center; //计算当前像素点的位置向量
                float dis = v.magnitude; //计算像素点与圆心的距离

                if (dis < radius && dis > innerRadius) //如果像素点在环形范围内
                {
                    c = color; //则颜色为指定颜色
                }
                else//如果不在
                {
                    c = new Color(0, 0, 0, 0);//颜色不重要，重点是要透明
                }

                t.SetPixel(w, h, c); //对像素点赋值
            }
        }
        t.Apply();//生成完整的Texture2D

        Sprite pic = Sprite.Create(t, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f)); //利用Texture2D生成Sprite
        sr.sprite = pic; //将Sprite赋值给Render
    }

    /// <summary>
    /// 生成环形的扇形
    /// </summary>
    /// <param name="sr"></param>本地的sprite渲染器
    /// <param name="size"></param>外直径，其实是对应正方形Texture2D的边长，单位为像素
    /// <param name="emptyRate"></param>留白率，指中心空白圆半径与大圆半径之比
    /// <param name="range"></param>扇形大小，指扇形中心角度与180度的比值，不会超过180度吧，不会吧不会吧
    /// <param name="Color"></param>颜色，指扇形的颜色，一般取绿色
    public void GenerateSectorSprite(SpriteRenderer sr, int size, float emptyRate, float range, Color color)
    {
        float radius = size / 2;
        float innerRadius = emptyRate * radius;
        Texture2D t = new Texture2D(size, size); //生成Texture2D
        Vector2 center = new Vector2(radius, radius); //生成圆心坐标

        //遍历每个像素点
        for (int w = 0; w < size; w++)
        {
            for (int h = 0; h < size; h++)
            {
                Color c;
                float angle;
                Vector2 hori = new Vector2(1, 0); //建立水平参考向量
                float angleRange = 180 * range; //计算扇形角度

                //计算像素点所在的角度范围
                float minAngle = 90 - angleRange / 2;
                float maxAngle = 90 + angleRange / 2;

                Vector2 v = new Vector2(w, h) - center; //计算当前像素点的位置向量
                float dis = v.magnitude; //计算像素点与圆心的距离

                //计算像素点与水平方向的夹角
                Vector3 cross = Vector3.Cross(v, hori);
                if (cross.z < 0)
                {
                    angle = -Vector2.Angle(v, hori);
                }
                else
                {
                    angle = Vector2.Angle(v, hori);
                }

                if (dis < radius && dis > innerRadius) //如果像素点在环形范围内
                {
                    if (angle > minAngle && angle < maxAngle)//并且在指定角度范围内
                    {
                        c = color; //则颜色为指定颜色
                    }
                    else //如果不在
                    {
                        c = new Color(0, 0, 0, 0);//颜色不重要，重点是要透明
                    }
                }
                else//如果不在
                {
                    c = new Color(0, 0, 0, 0);//颜色不重要，重点是要透明
                }

                t.SetPixel(w, h, c); //对像素点赋值
            }
        }
        t.Apply();//生成完整的Texture2D

        Sprite pic = Sprite.Create(t, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f)); //利用Texture2D生成Sprite
        sr.sprite = pic; //将Sprite赋值给Render
    }
}
