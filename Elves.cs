using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Citirea datelor din fisier
        string[] linii = File.ReadAllLines("input.txt");

        // Determinarea indexului liniei goale care separa diagrama de instructiuni
        int indexLinieGoala = Array.FindIndex(linii, string.IsNullOrWhiteSpace);

        // Extrageti numarul de stive din linia anterioara liniei goale (!!!!! DE VERIFICAT MAI TARZIU !!!!!!)
        int numarStive = int.Parse(linii[indexLinieGoala - 1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Last());

        // Initializarea stivelor
        Stack<char>[] stive = new Stack<char>[numarStive];

        // Popularea stivelor cu cutii din diagrama data
        for (int indexLinie = indexLinieGoala - 2; indexLinie >= 0; indexLinie--)
        {
            for (int idStiva = 0; idStiva < numarStive; idStiva++)
            {
                char cutie = linii[indexLinie][idStiva * 4 + 1];
                if (char.IsLetter(cutie))
                {
                    stive[idStiva] ??= new Stack<char>();
                    stive[idStiva].Push(cutie);
                }
            }
        }

        // Executarea instructiunilor pentru mutarea cutiilor intre stive
        for (int idInstructiune = indexLinieGoala + 1; idInstructiune < linii.Length; idInstructiune++)
        {
            string[] parti = linii[idInstructiune].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int numarCutii = int.Parse(parti[1]);
            int idStivaSursa = int.Parse(parti[3]) - 1;
            int idStivaDestinatie = int.Parse(parti[5]) - 1;

            for (int i = 0; i < numarCutii; i++)
            {
                if (stive[idStivaSursa].Count == 0)
                    break;

                char cutie = stive[idStivaSursa].Pop();
                stive[idStivaDestinatie].Push(cutie);
            }
        }

        // Afisarea rezultatului: cutiile de deasupra din fiecare stiva in parte
        foreach (var stiva in stive)
        {
            if (stiva != null && stiva.Count > 0)
                Console.Write(stiva.Peek());
        }
        Console.WriteLine();
    }
}
