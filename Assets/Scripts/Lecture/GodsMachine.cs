using UnityEngine;
using System;

public class GodsMachine : MonoBehaviour
{
    void Start()
    {
        Human human = new Human("Bob", 25, false);
        human.name = "Bob";
        //Animal Cat = new Animal("cat", 10);
        Animal Dog = new Animal();

        /*
        Human.name = "Bob";
        Cat.name = "Gio";
        Dog.name = "Lucy";

        Human.ageInYears = 25;
        Cat.ageInYears = 140;
        Dog.ageInyears = 1;
        */

    }

}
