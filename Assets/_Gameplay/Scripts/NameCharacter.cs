using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameCharacter
{
    private static List<string> names = new List<string>() {
        "Townsend",
        "George",
        "Doyle",
        "Hunter",
        "Hopkins",
        "Riley",
        "Ford",
        "Jenkins",
        "Duncan",
        "Mcgee",
        "Scott",
        "Burke",
        "Vega",
        "Bowman",
        "Conner",
        "Cooper",
        "Andrews",
        "Holloway",
        "Frazier",
        "Mills",
        "Parker",
        "Wagner",
        "Watts",
        "Fleming",
        "Waters",
        "Fletcher",
        "Reed",
        "Aguilar",
        "Foster",
        "Moss",
        "Jimenez",
        "Watson",
        "Ross",
        "Stokes",
        "Hamilton",
        "Bates",
        "Medina",
        "Perry",
        "Obrien",
        "Snyder",
        "Valdez",
        "Parks",
        "Gregory",
        "Schwartz",
        "Huff",
        "Harmon",
        "Norris",
        "Bowers",
        "May",
        "Brown"
    };

    public static List<string> GetNames(int amount)
    {
        var list = names.OrderBy(d => System.Guid.NewGuid());
        return list.Take(amount).ToList();
    }

    public static string GetRandomName()
    {
        return names[Random.Range(0, names.Count)];
    }
}