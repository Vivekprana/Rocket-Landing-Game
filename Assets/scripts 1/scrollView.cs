using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

 public class scrollView:MonoBehaviour
 {
 Vector2 scrollPosition;

 void Awake()
 {
     scrollPosition = new Vector2(this.transform.position.x, this.transform.position.y);
 }
 void OnGUI() // simply an example of a long ScrollView
    
     /*scrollPos = GUI.BeginScrollView(
      new Rect(110,50,130,150),scrollPos,
       new Rect(110,50,130,560),
       GUIStyle.none,GUIStyle.none); 
       // HOORAY THOSE TWO ARGUMENTS ELIMINATE
       // THE STUPID RIDICULOUS UNITY SCROLL BARS
     for(int i = 0;i < 20; i++)
        GUI.Box(new Rect(110,50+i*28,100,25),"xxxx_"+i);
     GUI.EndScrollView ();*/

 
    {
        // An absolute-positioned example: We make a scrollview that has a really large client
        // rect and put it in a small rect on the screen.
        using (var scrollScope = new GUI.ScrollViewScope(new Rect(0,0, 100, 100), scrollPosition, new Rect(0, 0, 220, 200)))
        {
            scrollPosition = scrollScope.scrollPosition;

            // Make four buttons - one in each corner. The coordinate system is defined
            // by the last parameter to the ScrollScope constructor.
            GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
            GUI.Button(new Rect(120, 0, 100, 20), "Top-right");
            GUI.Button(new Rect(0, 180, 100, 20), "Bottom-left");
            GUI.Button(new Rect(120, 180, 100, 20), "Bottom-right");
        }
        // Now the scroll view is ended.
    }


 
 void Update()  // so, make it scroll with the user's finger
    {
    if(Input.touchCount == 0) return;
    Touch touch = Input.touches[0];
    if (touch.phase == TouchPhase.Moved)
       {
       // simplistically, scrollPos.y += touch.deltaPosition.y;
       // but that doesn't actually work
       
       // don't forget you need the actual delta "on the glass"
       // fortunately it's easy to do this...
       
       float dt = Time.deltaTime / touch.deltaTime;
       if (dt == 0 || float.IsNaN(dt) || float.IsInfinity(dt))
          dt = 1.0f;
       Vector2 glassDelta = touch.deltaPosition * dt;
       
       scrollPosition.y += glassDelta.y;
       }
    }
 }