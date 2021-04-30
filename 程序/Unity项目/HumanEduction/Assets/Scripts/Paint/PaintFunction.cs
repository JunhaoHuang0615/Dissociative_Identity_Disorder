using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintFunction : MonoBehaviour
{
    public GameObject brushPrefab;
    public GameObject planer;
    private GameObject hint;

    public Camera screenShootCamera;

    public RenderTexture capturedTexture;

    private string path;


    void Start()
    {
        Instantiate(planer,new Vector3(-4,-3,0),planer.transform.rotation);
        path = Application.persistentDataPath + "/CustomPicture";
    }

    // Update is called once per frame
    void Update()
    {
        //获取mouse碰撞到painter的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //摄像机向鼠标为发射一条射线
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit,1000)){ //1000码距离之内碰到的物体，判断是否有碰到物体 
            if(hit.collider.tag == "PaintPlaner"){
                //按下左键就画画
                if(Input.GetMouseButtonDown(0)){
                    Instantiate(brushPrefab,hit.transform.position + new Vector3(0,0,-1),transform.rotation);

                }
                //移到哪儿哪儿就有提示
                //首先判断提示是否已经存在
                if(!hint){
                    hint = (GameObject)Resources.Load("Prefabs/Painter/Grid_hint");
                    Instantiate(hint,hit.transform.position + new Vector3(0,0,-0.5f),transform.rotation);
                }else{
                    print(hit.transform.position);
                    hint.transform.position = hit.transform.position;
                }
                

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

}
