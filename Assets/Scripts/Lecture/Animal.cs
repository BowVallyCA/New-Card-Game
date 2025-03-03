using UnityEngine;

public class Animal
{
    public string name;
    public bool isAlive;
    public int ageInYears;

    /*
     * Modifier classname()
     * {
     * 
     * }
    */

    public Animal()
    {
        Debug.Log("I Am Alive!");
    }

    public Animal(string name, int age, bool alive)
    {
        this.name = name;
        ageInYears = age;
        isAlive = alive;

        Debug.Log($"{name} is born"); 
    }

    public void Eat()
    {

    }

    public void Move()
    {

    }
}
