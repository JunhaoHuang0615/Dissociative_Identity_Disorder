using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class TestLoading : MonoBehaviour
{
    UIManager currentUIManager;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

        UIPackage.AddPackage("FGUI/Res_Main");
        UIPackage.AddPackage("FGUI/Res_Game");
        UIPackage.AddPackage("FGUI/Res_Component_public");

        GRoot.inst.SetContentScaleFactor(1920, 1080, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
        //各个系统，并且按照顺序
        if (currentUIManager == null)
        {
            currentUIManager = new UIManager();
            currentUIManager.OutGameMange();
        }
        currentUIManager.UIPanelDict[UIPanelType.Loading].Show();      
    }


        // Update is called once per frame
        void Update()
    {
        if(i ==100)
        {
            i = 0;
        }
        i += 1;
        currentUIManager.changeProgressBarValue(CommonGComp.LoadingProgressBar, i);
    }
}
