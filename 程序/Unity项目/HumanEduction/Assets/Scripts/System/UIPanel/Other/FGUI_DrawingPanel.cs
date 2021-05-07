using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FGUI_DrawingPanel : BasePanel
{   
    //拿到各组件
    private GButton btnSave;
    private GButton btnReturn;
    private GButton btnToColorizePanel;
    private GButton btnEraser;
    private GButton btnPen;
    private GSlider grayLevelSlider;

    private GLoader gray_Level_Pre;
    public FGUI_DrawingPanel(string packageName,
        UIPanelType uIPanelType, UIManager uIManager) : base(packageName,uIPanelType,uIManager)
    {
    }

      protected override void OnInitPanel(){
        //动画可能后期不需要
        Transition t = panelMask.GetTransition("hide_mask");
        t.Play();

        //得到按钮并注册按钮事件
        btnSave = contentPane.GetChild("Button_Save").asButton;
        btnReturn = contentPane.GetChild("Button_return").asButton;
        btnToColorizePanel = contentPane.GetChild("Button_ToColorizePanel").asButton;
        btnEraser = contentPane.GetChild("Button_Eraser").asButton;
        btnPen = contentPane.GetChild("Button_Pen").asButton;


        btnSave.onClick.Add(()=>{
            GameManager.Instance.paintFunction.Save();
        });
        //撤销按钮注册
        btnToColorizePanel.onClick.Add(()=>{
            GameManager.Instance.paintFunction.undoOper();
        });
        //反撤销按钮注册
        btnReturn.onClick.Add(()=>{
            GameManager.Instance.paintFunction.redoOper();
        });
        //橡皮擦事件注册
        btnEraser.onClick.Add(()=>{
            GameManager.Instance.paintFunction.isPen = false;

        });
        btnPen.onClick.Add(()=>{
            GameManager.Instance.paintFunction.isPen = true;

        });


        //关于Slider事件
        grayLevelSlider = contentPane.GetChild("GrayLevelSlider").asSlider;
        grayLevelSlider.value = 55;
        grayLevelSlider.onChanged.Add(()=>{
            GameManager.Instance.paintFunction.changeGrayLevel((float)grayLevelSlider.value);
            changeGrayLevelPrePicture();
        });

        //关于更改图片
        gray_Level_Pre = contentPane.GetChild("Gray_Level_Pre").asLoader;
        gray_Level_Pre.url = "Sprites/grid_sprite";
        gray_Level_Pre.color = new Color((float)grayLevelSlider.value/255,(float)grayLevelSlider.value/255,(float)grayLevelSlider.value/255);

    }

    private void changeGrayLevelPrePicture(){
        gray_Level_Pre.color = new Color((float)grayLevelSlider.value/255,(float)grayLevelSlider.value/255,(float)grayLevelSlider.value/255);
    }


}
