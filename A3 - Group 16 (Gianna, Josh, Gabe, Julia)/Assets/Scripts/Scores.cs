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

    
    private string filename2 ="\\LEVEL2_HEALTH_STATS.csv";
    private string filename21 ="\\LEVEL1_DMG.txt";
    private string filename211 ="\\Level2_Scores.txt";
    private int bossHlvl2;
    private int bossDamagelvl2;
    
    
    private string filename3 ="\\LEVEL3_HEALTH_STATS.csv";
    private string filename31 ="\\LEVEL1_DMG.txt";
    private string filename311 ="\\Level3_Scores.txt";
    private int bossHlvl3;
    private int bossDamagelvl3;

    
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
        
        //level 2
        TextReader trlvl2 = new StreamReader(Application.dataPath + filename2,false);
        TextReader trlvl2DMG = new StreamReader(Application.dataPath + filename11);
        TextReader trlvlScores2 = new StreamReader(Application.dataPath + filename111);
        
        temp = trlvl2.ReadLine();
        temp = trlvl2.ReadToEnd();
        values = temp.Split(',');
        
        bossHlvl2 = 5000 - int.Parse(values[values.Length-6]);
        bossDamagelvl2 = int.Parse(trlvl2DMG.ReadLine());
        
        
        temp = trlvlScores2.ReadLine();
        values = temp.Split(',');
        tempH = int.Parse(values[0]);
        tempDMG = int.Parse(values[1]);
        
        if (tempH < bossHlvl2) {
            bossHlvl2 = tempH;
        }
        
        if (tempDMG > bossDamagelvl2) {
            bossDamagelvl2 = tempDMG;
        }
        TextReader trlvl3 = new StreamReader(Application.dataPath + filename3,false);
        TextReader trlvl3DMG = new StreamReader(Application.dataPath + filename11);
        TextReader trlvlScores3 = new StreamReader(Application.dataPath + filename111);
        
        temp = trlvl3.ReadLine();
        temp = trlvl3.ReadToEnd();
        values = temp.Split(',');
        
        bossHlvl2 = 5000 - int.Parse(values[values.Length-6]);
        bossDamagelvl2 = int.Parse(trlvl3DMG.ReadLine());
        
        
        temp = trlvlScores3.ReadLine();
        values = temp.Split(',');
        tempH = int.Parse(values[0]);
        tempDMG = int.Parse(values[1]);
        
        if (tempH < bossHlvl3) {
            bossHlvl3 = tempH;
        }
        
        if (tempDMG > bossDamagelvl3) {
            bossDamagelvl3 = tempDMG;
        }
        //level 1 closees 
        trlvl1.Close();
        trlvl1DMG.Close();
        trlvlScores.Close();
        
        //level 2 closes
        trlvl2.Close();
        trlvl2DMG.Close();
        trlvlScores2.Close();
        
        //level 3 closes
        trlvl3.Close();
        trlvl3DMG.Close();
        trlvlScores3.Close();
       
    }

    // Update is called once per frame
    void Update()
    {
        TextWriter twlvl1 = new StreamWriter(Application.dataPath + filename111,false);
        twlvl1.WriteLine(bossHlvl1 + "," + bossDamagelvl1);
        twlvl1.Close();
        
        TextWriter twlvl2 = new StreamWriter(Application.dataPath + filename111,false);
        twlvl2.WriteLine(bossHlvl2 + "," + bossDamagelvl2);
        twlvl2.Close();
        
        TextWriter twlvl3 = new StreamWriter(Application.dataPath + filename111,false);
        twlvl3.WriteLine(bossHlvl2 + "," + bossDamagelvl2);
        twlvl3.Close();
    }

    private int rectWidth = 400;
    private int rectHeight = 100;
    private void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(155, 100, rectWidth, rectHeight), "Level 1: Damage to boss: " + bossHlvl1.ToString() + " Damage done by Boss: " + bossDamagelvl1);
        GUI.Label(new Rect(175, 100, rectWidth, rectHeight), "Level 2: Damage to boss: " + bossHlvl2.ToString() + " Damage done by Boss: " + bossDamagelvl2);
        GUI.Label(new Rect(195, 100, rectWidth, rectHeight), "Level 3: Damage to boss: " + bossHlvl3.ToString() + " Damage done by Boss: " + bossDamagelvl3);
    }
}
