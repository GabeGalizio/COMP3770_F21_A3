//Basic file to simulate game economy.

/* Assumptions I have made for how this works:
	1. Players get their turns first.
	2. Health/mana caps at their initial values, and cannot go below 0.
	3. Heals can and will be performed, even if a party member is at max HP.
	   The mana will still be expended, even if it is to no effect.
	4. The healer will try to cast Small Heal before it tries to cast Big Heal.
*/

using System.IO;
using UnityEngine;

public class Level1 : MonoBehaviour
{
	private string filename = "\\LEVEL1_HEALTH_STATS.csv";
	private string filename2 = "\\LEVEL1_DMG.txt";

	void Start()
	{
		TextWriter wr = new StreamWriter(Application.dataPath + filename,false);
		wr.WriteLine("Timestep, Boss, Warrior, Rogue, Mage, Druid, Priest");
		wr.Close();
		
	}

	//Stats
	int BossHealth = 5000;
	int[] SquishiesHealth = {1000, 1250, 1500, 900}; //Health of mage, druid, rogue, priest
	int TankHealth = 3000;
	int PriestMana = 1000;
	int random = 0;
	int BossDMG = 0;
	int[] PartyDMG = {0,0,0,0,0}; //Party dmg order: Warrior, Rogue, Mage, Druid, Priest
	int timesteps = 0;
	
	public bool dead = false;
	
	//Step every frame
	void Update() {
		//Will only run if nobody has "beefed it"
		if(!dead) {
			timesteps++;
			
			//Warrior: Deal 5-10 dmg to boss
			random = Random.Range(5, 11);
			PartyDMG[0] += random;
			BossHealth -= random;
		
			//Rogue: Deal 15-25 dmg to boss
			random = Random.Range(15, 26);
			PartyDMG[1] += random;
			BossHealth -= random;
		
			//Mage: Deal 5-30 dmg to boss
			random = Random.Range(5, 31);
			PartyDMG[2] += random;
			BossHealth -= random;
		
			//Druid: Deal 5-15 dmg to boss
			random = Random.Range(5, 16);
			PartyDMG[3] += random;
			BossHealth -= random;
		
			//TODO: Record score
			if(BossHealth <= 0) {
				BossHealth = 0;
				dead = true;
				//Start of csv writing
				TextWriter hw1 = new StreamWriter(Application.dataPath + filename, true);
				hw1.WriteLine(timesteps + "," + BossHealth + "," + TankHealth + "," + SquishiesHealth[2] + "," + SquishiesHealth[0] + "," + SquishiesHealth[1] + "," + SquishiesHealth[3]);
				hw1.Close();
				//end of csv
				return; //Stop simulation for this frame
			}
		
			//Increment priest mana
			if(PriestMana < 998) {
				PriestMana += 3;
			} else {
				PriestMana = 1000;
			}
		
			//Cast Small Heal
			if(PriestMana > 4) { //Ensure mana is at least 5
				PriestMana -= 5; //Reduce mana
				//Randomize target
				random = Random.Range(0, 2);
				//2/3 chance of healing the healer (twice as likely as damage dealers)
				if(random != 0) {
					//Heal +15, for max of 900.
					if(SquishiesHealth[3] < 886) {
						SquishiesHealth[3] += 15;
					} else {
						SquishiesHealth[3] = 900;
					}
				} else { //1/3 chance to heal damage dealer
					random = Random.Range(0, 3);
					//Heal +15 for max of dps' max health... Featuring some array shenanigans
					int MaxHealth = 1000 + (random * 250);
					if(SquishiesHealth[random] < (MaxHealth - 14)) {
						SquishiesHealth[random] += 15;
					} else {
						SquishiesHealth[random] = MaxHealth;
					}
				}
			}
			
			//Cast Big Heal
			if(PriestMana > 9) {
				PriestMana -= 10; //Reduce mana
				//Heal tank for +25, capping at 3000
				if(TankHealth < 2976) {
					TankHealth += 25;
				} else {
					TankHealth = 3000;
				}
			}
			
			
			//Squishies
			for(int i = 0; i < 4; i++) {
				random = Random.Range(5, 21);
				BossDMG += random;
				SquishiesHealth[i] -= random;
				if(SquishiesHealth[i] < 1) {
					SquishiesHealth[i] = 0;
					dead = true;
					TextWriter hw1 = new StreamWriter(Application.dataPath + filename, true);
					hw1.WriteLine(timesteps + "," + BossHealth + "," + TankHealth + "," + SquishiesHealth[2] + "," + SquishiesHealth[0] + "," + SquishiesHealth[1] + "," + SquishiesHealth[3]);
					hw1.Close();
					return; //Stop simulation for this frame
				}
			}
			
			//Tank
			random = Random.Range(40, 51);
			BossDMG += random;
			TankHealth -= random;
			if(TankHealth < 1) {
				TankHealth = 0;
				dead = true;
				TextWriter hw1 = new StreamWriter(Application.dataPath + filename, true);
				hw1.WriteLine(timesteps + "," + BossHealth + "," + TankHealth + "," + SquishiesHealth[2] + "," + SquishiesHealth[0] + "," + SquishiesHealth[1] + "," + SquishiesHealth[3]);
				hw1.Close();
				return; //Stop simulation for this frame
			}
			//TODO WRITING HEALTH TO CSV AND CALCULATING SCORE MUCH LOVE
			
			//Start of csv writing
			TextWriter hw = new StreamWriter(Application.dataPath + filename, true);
			hw.WriteLine(timesteps + "," + BossHealth + "," + TankHealth + "," + SquishiesHealth[2] + "," + SquishiesHealth[0] + "," + SquishiesHealth[1] + "," + SquishiesHealth[3]);
			hw.Close();
			//end of csv
			TextWriter wr2 = new StreamWriter(Application.dataPath + filename2,false);
			wr2.WriteLine(BossDMG);
			wr2.Close();
		}
	}
	
	//GUI shit
	
	int rectWidth = 300;
	int rectHeight = 50;
	
	void OnGUI() {
		
		//Draw healths
		GUI.Label(new Rect(10, 10, rectWidth, rectHeight), "Boss Health: " + BossHealth.ToString());
		GUI.Label(new Rect(10, 20, rectWidth, rectHeight), "Warrior Health: " + TankHealth.ToString());
		GUI.Label(new Rect(10, 30, rectWidth, rectHeight), "Rogue Health: " + SquishiesHealth[2].ToString());
		GUI.Label(new Rect(10, 40, rectWidth, rectHeight), "Mage Health: " + SquishiesHealth[0].ToString());
		GUI.Label(new Rect(10, 50, rectWidth, rectHeight), "Druid Health: " + SquishiesHealth[1].ToString());
		GUI.Label(new Rect(10, 60, rectWidth, rectHeight), "Priest Health: " + SquishiesHealth[3].ToString());
		GUI.Label(new Rect(10, 70, rectWidth, rectHeight), "Priest Mana: " + PriestMana.ToString());
		
		//Draw damages
		GUI.Label(new Rect(150, 10, rectWidth, rectHeight), "Boss Damage: " + BossDMG.ToString());
		GUI.Label(new Rect(150, 20, rectWidth, rectHeight), "Warrior Damage: " + PartyDMG[0].ToString());
		GUI.Label(new Rect(150, 30, rectWidth, rectHeight), "Rogue Damage: " + PartyDMG[1].ToString());
		GUI.Label(new Rect(150, 40, rectWidth, rectHeight), "Mage Damage: " + PartyDMG[2].ToString());
		GUI.Label(new Rect(150, 50, rectWidth, rectHeight), "Druid Damage: " + PartyDMG[3].ToString());
		GUI.Label(new Rect(150, 60, rectWidth, rectHeight), "Priest Damage: 9999999999999999999999999999999");
		
		//And timesteps
		GUI.Label(new Rect(300, 10, rectWidth, rectHeight), "Timesteps: " + timesteps.ToString());
		
		if(dead) {
			//TODO make button not label
			GUI.Label(new Rect(10, 80, rectWidth, rectHeight), "DEATH");
		}
	}
}