using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintFunction : MonoBehaviour
{
    public GameObject brushPrefab;
    private float grayLevel;
    private SpriteRenderer brushSprite;
    public GameObject planer;
    private GameObject hint;

    public bool isPen; //检测是否是笔
    public Camera screenShootCamera;

    public RenderTexture capturedTexture;

    private string path;

    //UIManager
    private UIManager uIManager;

    //记录所有brush的位置，用于撤回
    private Stack<GameObject> HistoryRecord;
   // 当撤销时，就把这个点隐藏，并且放到反撤销组，这个组只要一进行主动操作则清空
   private Stack<GameObject> redoGroup;

    public PaintFunction(UIManager uIManager){
        this.uIManager = uIManager;

    }

    void Start()
    {
        Instantiate(planer,new Vector3(-4,-3,0),planer.transform.rotation);
        path = Application.persistentDataPath + "/CustomPicture";
        brushSprite = brushPrefab.GetComponent<SpriteRenderer>();
        grayLevel = 55f;
        //0 代表0 ， 1代表255，因此需要做除法
        brushSprite.color = new Color(grayLevel/255f,grayLevel/255f,grayLevel/255f); 
        HistoryRecord = new Stack<GameObject>(); 
        redoGroup = new Stack<GameObject>();  
        isPen = true;    

    }

    // Update is called once per frame
    void Update()
    {
        //获取mouse碰撞到painter的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //摄像机向鼠标为发射一条射线
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,1000)){ //1000码距离之内碰到的物体，判断是否有碰到物体 
            //碰到了物体，但是要判断是否在UI上，注意这里碰撞是碰到了场景内的物体，单纯的碰到了UI并不会触发上面的判断
            if(uIManager.isOnFGUI() == false){

                if(hit.collider.tag == "PaintPlaner" | hit.collider.tag == "PaintDot" ){
                    //按下左键就画画
                    if(Input.GetMouseButtonDown(0) && isPen == true){
                        GameObject tempBrush;
                        tempBrush=Instantiate(brushPrefab,hit.transform.position + new Vector3(0,0,-1),transform.rotation);
                        HistoryRecord.Push(tempBrush);
                        //并且清空redo的栈
                        redoGroup.Clear();
                    }
                    //移到哪儿哪儿就有提示
                    //首先判断提示是否已经存在
                    if(!hint){
                        hint = (GameObject)Resources.Load("Prefabs/Painter/Grid_hint");
                        hint = Instantiate(hint,hit.transform.position,transform.rotation);
                    }else{
                        hint.transform.position = hit.transform.position;
                    }
                    

                }
                // =============================橡皮擦模式======================================
                if(hit.collider.tag == "PaintDot"){
                    if(Input.GetMouseButtonDown(0) && isPen == false){
                        //得到这个物体
                        GameObject temp = hit.collider.gameObject;
                        temp.SetActive(false);
                        //注意这个物体还是存在于HistorRecord中的
                        HistoryRecord.Push(temp);
                    }
                }
               
            } 
            else
            {
                print("点在UI上了");
            }
        }

        if(Input.GetKeyDown(KeyCode.S)){
            Save();
        }
        
    }

    public void Save(){
        StartCoroutine(CoSave());
    }

    private IEnumerator CoSave(){
        yield return new WaitForEndOfFrame();
        //激活渲染
        Debug.Log(path);
        RenderTexture.active = capturedTexture;
        var texture2D = new Texture2D(capturedTexture.width,capturedTexture.height);
        texture2D.ReadPixels(new Rect(0,0,capturedTexture.width,capturedTexture.height),0,0);
        texture2D.Apply();
        var data = texture2D.EncodeToPNG();
        if(!Directory.Exists(path)){
            Directory.CreateDirectory(path);
        }
         File.WriteAllBytes(path+"/custom.png",data);
    }

    //拿到UIManager
    public void getUIManager(UIManager uIManager){
        this.uIManager = uIManager;
    }

    public void changeGrayLevel(float level){
        grayLevel = level;
        brushSprite.color = new Color(level/255,level/255,level/255);
    }

    //撤销操作
    public void undoOper(){
        GameObject brushTemp = HistoryRecord.Pop();
        //gameObject.SetActive(false) 隐藏
        //对于有子物体的 gameObject.SetActiveRecursively(false)
        if(brushTemp.activeSelf == true){
            redoGroup.Push(brushTemp);
            brushTemp.SetActive(false);
        }
        else if(brushTemp.activeSelf == false){
            redoGroup.Push(brushTemp);
            brushTemp.SetActive(true);
        }
    }
    //反撤销
    public void redoOper(){
        try{
            GameObject temp = redoGroup.Pop();
            if(temp.activeSelf == false){
                temp.SetActive(true);
                HistoryRecord.Push(temp);
            }
            else if(temp.activeSelf == true){
                temp.SetActive(false);
                HistoryRecord.Push(temp);
            }
            
        }catch{
            print("没有步骤了");
        }
    }
}
