using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    //used for all scores
    private int tempDMG;
    private int tempH;

    //level 1
    private string filename1 ="\\LEVEL1_HEALTH_STATS.csv";
    private string filename11 ="\\LEVEL1_DMG.txt";
    private string filename111 ="\\Level1_Scores.txt";
    private int bossHlvl1;
    private int bossDamagelvl1;
    private string[] values;
    private string temp;

    //level 2
    private string filename2 ="\\LEVEL2_HEALTH_STATS.csv";
    private string filename21 ="\\LEVEL2_DMG.txt";
    private string filename211 ="\\Level2_Scores.txt";
    private int bossHlvl2;
    private int bossDamagelvl2;
    private string temp2;
    private string[] values2;
    
    //level 3
    private string filename3 ="\\LEVEL3_HEALTH_STATS.csv";
    private string filename31 ="\\LEVEL3_DMG.txt";
    private string filename311 ="\\Level3_Scores.txt";
    private int bossHlvl3;
    private int bossDamagelvl3;
    private string temp3;
    private string[] values3;
    private int tempDMG3;
    private int tempH3;

    
    // Start is called before the first frame update
    void Start()
    {
        //Level 1 
        TextReader trlvl1 = new StreamReader(Application.dataPath + filename1,false);
        TextReader trlvl1DMG = new StreamReader(Application.dataPath + filename11,false);
        TextReader trlvlScores = new StreamReader(Application.dataPath + filename111,false);
        
        temp = trlvl1.ReadLine();
        temp = trlvl1.ReadToEnd();
        values = temp.Split(',');
        
        bossHlvl1 = 5000 - int.Parse(values[values.Length-6]);
        bossDamagelvl1 = int.Parse(trlvl1DMG.ReadLine());

       
        temp = trlvlScores.ReadLine();
        values = temp.Split(',');
        tempH = int.Parse(values[0]); // value from the saved file
        tempDMG = int.Parse(values[1]); //value from the saved file
        
        if (tempH > bossHlvl1) {
            bossHlvl1 = tempH;
        }
        
        if (tempDMG > bossDamagelvl1) {
            bossDamagelvl1 = tempDMG;
        }
        
        //level 1 closes 
        trlvl1.Close();
        trlvl1DMG.Close();
        trlvlScores.Close();
        
        TextWriter twlvl1 = new StreamWriter(Application.dataPath + filename111,false);
        twlvl1.WriteLine(bossHlvl1 + "," + bossDamagelvl1);
        twlvl1.Close();
        
        //level 2
        TextReader trlvl2 = new StreamReader(Application.dataPath + filename2,false);
        TextReader trlvl2DMG = new StreamReader(Application.dataPath + filename21,false);
        TextReader trlvlScores2 = new StreamReader(Application.dataPath + filename211,false);
        
        temp2 = trlvl2.ReadLine();
        temp2 = trlvl2.ReadToEnd();
        values2 = temp2.Split(',');
        
        bossHlvl2 = 5000 - int.Parse(values2[values2.Length-6]);
        bossDamagelvl2 = int.Parse(trlvl2DMG.ReadLine());
        
        tempH = 0;
        tempDMG = 0;
        
        temp2 = trlvlScores2.ReadLine();
        values2 = temp2.Split(',');
        tempH = int.Parse(values2[0]);
        tempDMG = int.Parse(values2[1]);
        
        if (tempH > bossHlvl2) {
            bossHlvl2 = tempH;
        }
        
        if (tempDMG > bossDamagelvl2) {
            bossDamagelvl2 = tempDMG;
        }
        //level 2 closes
        trlvl2.Close();
        trlvl2DMG.Close();
        trlvlScores2.Close();
        
        TextWriter twlvl2 = new StreamWriter(Application.dataPath + filename211,false);
        twlvl2.WriteLine(bossHlvl2 + "," + bossDamagelvl2);
        twlvl2.Close();
        
        //level 3
        TextReader trlvl3 = new StreamReader(Application.dataPath + filename3,false);
        TextReader trlvl3DMG = new StreamReader(Application.dataPath + filename31,false);
        TextReader trlvlScores3 = new StreamReader(Application.dataPath + filename311,false);
        
        temp3 = trlvl3.ReadLine();
        temp3 = trlvl3.ReadToEnd();
        values3 = temp3.Split(',');
        
        bossHlvl3 = 5000 - int.Parse(values3[values3.Length-6]);
        bossDamagelvl3 = int.Parse(trlvl3DMG.ReadLine());
        
        temp3 = trlvlScores3.ReadLine();
        values3 = temp3.Split(',');
        tempH = int.Parse(values3[0]);
        tempDMG = int.Parse(values3[1]);
        
        if (tempH > bossHlvl3) {
            bossHlvl3 = tempH;
        }
        
        if (tempDMG > bossDamagelvl3) {
            bossDamagelvl3 = tempDMG;
        }
        
        trlvl3.Close();
        trlvl3DMG.Close();
        trlvlScores3.Close();
        
        TextWriter twlvl3 = new StreamWriter(Application.dataPath + filename311,false);
        twlvl3.WriteLine(bossHlvl3 + "," + bossDamagelvl3);
        twlvl3.Close();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private int rectWidth = 400;
    private int rectHeight = 100;
    private void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(155, 100, rectWidth, rectHeight), "Level 1: Damage to boss: " + bossHlvl1 + " Damage done by Boss: " + bossDamagelvl1);
        GUI.Label(new Rect(155, 120, rectWidth, rectHeight), "Level 2: Damage to boss: " + bossHlvl2 + " Damage done by Boss: " + bossDamagelvl2);
        GUI.Label(new Rect(155, 140, rectWidth, rectHeight), "Level 3: Damage to boss: " + bossHlvl3 + " Damage done by Boss: " + bossDamagelvl3);
    }
}
