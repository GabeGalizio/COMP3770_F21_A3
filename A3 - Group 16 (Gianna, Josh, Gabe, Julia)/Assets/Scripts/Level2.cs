//Basic file to simulate game economy. Except it's Level 2.

/* How it works:
	1. Players get their turns first.
	2. Health/mana caps at their initial values, and cannot go below 0.
	3. Heals can and will be performed, even if a party member is at max HP.
	   The mana will still be expended, even if it is to no effect.
	4. The healer will try to cast Small Heal before it tries to cast Big Heal.
*/

using System.IO;
using UnityEngine;

public class Level2 : MonoBehaviour {
	
	private string filename = "\\LEVEL2_HEALTH_STATS.csv";
	private string filename2 = "\\LEVEL2_DMG.txt";
	
	//Stats
	int _bossHealth = 5000;
	int[] _squishiesHealth = {1000, 1250, 1500, 900}; //Health of mage, druid, rogue, priest
	int _tankHealth = 3000;
	int _priestMana = 1000;
	int _random = 0;
	int _bossDmg = 0;
	int[] _partyDmg = {0,0,0,0,0}; //Party dmg order: Warrior, Rogue, Mage, Druid, Priest
	int _timesteps = 0;
	
	public bool dead = false;
	
	void Start()
	{
		TextWriter wr = new StreamWriter(Application.dataPath + filename,false);
		wr.WriteLine("Timestep, Boss, Warrior, Rogue, Mage, Druid, Priest");
		wr.Close();
		
	}
	
	//Step every frame
	void Update()
	{

		//Might beef this up later.
		if (dead) return;
		_timesteps++;
			
		//Warrior: Deal 5-10 dmg to boss
		_random = Random.Range(5, 11);
		_partyDmg[0] += _random;
		_bossHealth -= _random;
		
		//Rogue: Deal 15-25 dmg to boss
		_random = Random.Range(15, 26);
		_partyDmg[1] += _random;
		_bossHealth -= _random;
		
		//Mage: Deal 5-30 dmg to boss
		_random = Random.Range(5, 31);
		_partyDmg[2] += _random;
		_bossHealth -= _random;
		
		//Druid: Deal 5-15 dmg to boss
		_random = Random.Range(5, 16);
		_partyDmg[3] += _random;
		_bossHealth -= _random;
		
		//TODO: Record score
		if(_bossHealth <= 0) {
			_bossHealth = 0;
			dead = true;
			return; //Stop simulation for this frame
		}
		
		//Increment priest mana
		if(_priestMana < 998) {
			_priestMana += 3;
		} else {
			_priestMana = 1000;
		}
		
		//Cast Small Heal
		if(_priestMana > 4) { //Ensure mana is at least 5
			_priestMana -= 5; //Reduce mana
			//Randomize target
			_random = Random.Range(0, 2);
			//2/3 chance of healing the healer (twice as likely as damage dealers)
			if(_random != 0) {
				//Heal +15, for max of 900.
				if(_squishiesHealth[3] < 886) {
					_squishiesHealth[3] += 15;
				} else {
					_squishiesHealth[3] = 900;
				}
			} else { //1/3 chance to heal damage dealer
				_random = Random.Range(0, 3);
				//Heal +15 for max of dps' max health... Featuring some array shenanigans
				var maxHealth = 1000 + (_random * 250);
				if(_squishiesHealth[_random] < (maxHealth - 14)) {
					_squishiesHealth[_random] += 15;
				} else {
					_squishiesHealth[_random] = maxHealth;
				}
			}
		}
			
		//Cast Big Heal
		if(_priestMana > 9) {
			_priestMana -= 10; //Reduce mana
			//Heal tank for +25, capping at 3000
			if(_tankHealth <= 2976) {
				_tankHealth += 25;
			} else {
				_tankHealth = 3000;
			}
		}
			
		//Boss attacks TODO
			
		//Squishies
		for(var i = 0; i < 4; i++) {
			_random = Random.Range(5, 21);
			_bossDmg += _random;
			_squishiesHealth[i] -= _random;
			if (_squishiesHealth[i] >= 1) continue;
			_squishiesHealth[i] = 0;
			dead = true;
			return; //Stop simulation for this frame
		}
			
		//Tank
		_random = Random.Range(40, 51);
		_bossDmg += _random;
		_tankHealth -= _random;

		if (_tankHealth <= 1500) {
			//Randomize heal spell.
			_random = Random.Range(0, 1);
			if (_random == 0) { //Cast Small Heal.
				//Randomize target
				_random = Random.Range(0, 2);
				//2/3 chance of healing the healer (twice as likely as damage dealers)
				if(_random != 0) {
					//Heal +15, for max of 900.
					if(_squishiesHealth[3] < 886) {
						_squishiesHealth[3] += 15;
					} else {
						_squishiesHealth[3] = 900;
					}
				} else { //1/3 chance to heal damage dealer
					_random = Random.Range(0, 3);
					//Heal +15 for max of dps' max health... Featuring some array shenanigans
					var maxHealth = 1000 + (_random * 250);
					if(_squishiesHealth[_random] < (maxHealth - 14)) {
						_squishiesHealth[_random] += 15;
					} else {
						_squishiesHealth[_random] = maxHealth;
					}
				}
			} else { //Cast Big Heal.
				//Heal tank for +25, capping at 3000
				if(_tankHealth <= 2976) {
					_tankHealth += 25;
				} else {
					_tankHealth = 3000;
				}
			}

		}

		TextWriter hw = new StreamWriter(Application.dataPath + filename, true);
		hw.WriteLine(_timesteps + "," + _bossHealth + "," + _tankHealth + "," + _squishiesHealth[2] + "," + _squishiesHealth[0] + "," + _squishiesHealth[1] + "," + _squishiesHealth[3]);
		hw.Close();
		//end of csv
		TextWriter wr2 = new StreamWriter(Application.dataPath + filename2,false);
		wr2.WriteLine(_bossDmg);
		wr2.Close();
		
		if (_tankHealth >= 1) return; //Stop simulation for this frame
		_tankHealth = 0;
		dead = true;
		//TODO WRITING HEALTH TO CSV AND CALCULATING SCORE MUCH LOVE

	}
	
	//GUI for the able sighted.
	
	int rectWidth = 300;
	int rectHeight = 50;
	
	void OnGUI() {
		
		//Draw healths
		GUI.Label(new Rect(10, 10, rectWidth, rectHeight), "Boss Health: " + _bossHealth.ToString());
		GUI.Label(new Rect(10, 20, rectWidth, rectHeight), "Warrior Health: " + _tankHealth.ToString());
		GUI.Label(new Rect(10, 30, rectWidth, rectHeight), "Rogue Health: " + _squishiesHealth[2].ToString());
		GUI.Label(new Rect(10, 40, rectWidth, rectHeight), "Mage Health: " + _squishiesHealth[0].ToString());
		GUI.Label(new Rect(10, 50, rectWidth, rectHeight), "Druid Health: " + _squishiesHealth[1].ToString());
		GUI.Label(new Rect(10, 60, rectWidth, rectHeight), "Priest Health: " + _squishiesHealth[3].ToString());
		GUI.Label(new Rect(10, 70, rectWidth, rectHeight), "Priest Mana: " + _priestMana.ToString());
		
		//Draw damages
		GUI.Label(new Rect(150, 10, rectWidth, rectHeight), "Boss Damage: " + _bossDmg.ToString());
		GUI.Label(new Rect(150, 20, rectWidth, rectHeight), "Warrior Damage: " + _partyDmg[0].ToString());
		GUI.Label(new Rect(150, 30, rectWidth, rectHeight), "Rogue Damage: " + _partyDmg[1].ToString());
		GUI.Label(new Rect(150, 40, rectWidth, rectHeight), "Mage Damage: " + _partyDmg[2].ToString());
		GUI.Label(new Rect(150, 50, rectWidth, rectHeight), "Druid Damage: " + _partyDmg[3].ToString());
		GUI.Label(new Rect(150, 60, rectWidth, rectHeight), "Priest Damage: N/A");
		
		//Time-steps
		GUI.Label(new Rect(300, 10, rectWidth, rectHeight), "Time-steps: " + _timesteps.ToString());
		
		if(dead) {
			//TODO make button not label
			GUI.Label(new Rect(10, 80, rectWidth, rectHeight), "DEATH");
		}
	}
}
