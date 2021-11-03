using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    //used for all scores
    private string temp;
    private int tempDMG;
    private int tempH;
    private string[] values;

    //level 1
    private string filename1 ="\\LEVEL1_HEALTH_STATS.csv";
    private string filename11 ="\\LEVEL1_DMG.txt";
    private string filename111 ="\\Level1_Scores.txt";
    private int bossHlvl1;
    private int bossDamagelvl1;

    
    //private string filename2 ="\\LEVEL2_HEALTH_STATS.csv";
    //private string filename3 ="\\LEVEL3_HEALTH_STATS.csv";
    
    // Start is called before the first frame update
    void Start()
    {
        //Level 1 stuff 
        TextReader trlvl1 = new StreamReader(Application.dataPath + filename1,false);
        TextReader trlvl1DMG = new StreamReader(Application.dataPath + filename11);
        TextReader trlvlScores = new StreamReader(Application.dataPath + filename111);
        
        temp = trlvl1.ReadLine();
        temp = trlvl1.ReadToEnd();
        values = temp.Split(',');
        
        bossHlvl1 = 5000 - int.Parse(values[values.Length-6]);
        bossDamagelvl1 = int.Parse(trlvl1DMG.ReadLine());
        
        
        temp = trlvlScores.ReadLine();
        values = temp.Split(',');
        tempH = int.Parse(values[0]);
        tempDMG = int.Parse(values[1]);
        
        if (tempH < bossHlvl1) {
            bossHlvl1 = tempH;
        }
        
        if (tempDMG > bossDamagelvl1) {
            bossDamagelvl1 = tempDMG;
        }
        
    

        //TextReader trlvl2 = new StreamReader(Application.dataPath + filename2,false);
        //TextReader trlvl3 = new StreamReader(Application.dataPath + filename3,false);

        trlvl1.Close();
        trlvl1DMG.Close();
        trlvlScores.Close();
       
    }

    // Update is called once per frame
    void Update()
    {
        TextWriter twlvl1 = new StreamWriter(Application.dataPath + filename111,false);
        twlvl1.WriteLine(bossHlvl1 + "," + bossDamagelvl1);
        twlvl1.Close();
    }

    private int rectWidth = 400;
    private int rectHeight = 100;
    private void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(155, 100, rectWidth, rectHeight), "Level 1: Damage to boss: " + bossHlvl1.ToString() + " Damage done by Boss: " + bossDamagelvl1);
    }
}
