using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClasses
{
    public partial class Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            // Create a Character class object .....
            Character hero = new Character(); 
            hero.Name = "Hero";
            hero.Health = 40;
            hero.DamageMax = 20;
            hero.AttackBonus = true;
  
            // Create a second Character class object
            Character monster = new Character();
            monster.Name = "Monster";
            monster.Health = 40;
            monster.DamageMax = 20;
            monster.AttackBonus = false;

            // Create a Dice object
            Dice gameDice = new Dice();

            // Battle 1: Hero attacks Monster, Monster attacks Hero
            // The attack method returns a value for the roll, or damage so calling that method to retrieve a value as the argument
            displayStatsAfterBattle(hero, monster);
            monster.Defend(hero.Attack(gameDice));     
            hero.Defend(monster.Attack(gameDice));
            displayStatsAfterBattle(hero, monster);

            // Battle 2: Bonus Attacks for those characters with AttackBonus property set to "true"
            if (hero.AttackBonus)
            {
                monster.Defend(hero.Attack(gameDice));
                displayStatsAfterBattle(hero, monster);
            }
            
           if (monster.AttackBonus)
           {
                hero.Defend(monster.Attack(gameDice));
                displayStatsAfterBattle(hero, monster);
            }

            // Turn 3:
            //Loop until one character's health is less than zero allowing the monster the first attack, then the hero.
             while (hero.Health > 0 & monster.Health > 0)
            {
                hero.Defend(monster.Attack(gameDice));
                displayStatsAfterBattle(hero, monster);
                monster.Defend(hero.Attack(gameDice));
                displayStatsAfterBattle(hero, monster);
            }

            // Display Results of the game ... The winner
            displayResults(hero, monster);

        }

        private void displayStatsAfterBattle(Character hero, Character monster)
        {
            displayStats(hero);
            displayStats(monster);
        }

        private void displayResults(Character hero, Character monster)
        {
            // THINK ABOUT THIS .... just using less than or equal to zero
            if (hero.Health <= 0 && monster.Health <= 0)
                resultLabel.Text += String.Format("<p>Both Hero {0} and Monster {1} die!<p/>", hero.Name, monster.Name);
            else if (hero.Health <= 0)
                resultLabel.Text += String.Format("<p>Monster {0} defeats the Hero {1} !<p/>", monster.Name, hero.Name);
            else
            resultLabel.Text +=String.Format( "<p>Hero {0} defeats the Monster {1} !<p/>", hero.Name, monster.Name);
 
        }

        private void displayStats(Character character)
        {
            resultLabel.Text += String.Format("<p>Name: {0} - Health: {1} - Damage Max: {2} - Attack Bonus: {3} <p/>", 
                                                            character.Name, character.Health.ToString(), 
                                                            character.DamageMax.ToString(), character.AttackBonus.ToString());
        }
    }

    // A custom class "Character"
    class Character
    {
        // A property within a class with a get and set. Can be written on multiple lines or just one
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMax { get; set; }
        public bool AttackBonus { get; set; }

        public int Attack(Dice dice)
        {
            dice.Sides = this.DamageMax;
            return dice.Roll(dice);
        }

        public void Defend(int damage)
        {
            this.Health -= damage;
        }
    }

    // A custom class "Dice"
    class Dice
    {
        
        public int Sides { get; set; }

        // Create a random roll object for the dice roll
        Random randomRoll = new Random();

        public int Roll(Dice dice)
        {
            return randomRoll.Next(1,this.Sides);
        }
     }
}
