using System;

namespace OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            //kaip sausainį gamini naudodamas sausainio formelę, 
            //taip objektą gamini naudodamas klasę. Klasė - tai objekto formelė. 
            //čia Person yra klasė, o Kaimynas - objektas, sukurtas naudojant klasę
            Person Kaimynas = new Person();
            Kaimynas.Name = "Jonas";
            Kaimynas.Age = 45;
            Kaimynas.FavoriteMeal = "Kugelis";

            //objektą galima sukurti ir kitokiu būdu
            Person Kaimyne = new Person()
            {
                Name = "Morta",
                Age = 43,
                FavoriteMeal = "Varškės apkepas"
            };

            //trečias būdas sukurti objektą - naudojant konstruktorių
            Person Kaimyniukas = new Person("Lukutis", 15, "Kepta vištiena");

            Console.WriteLine("Kitoj gatvės pusėj gyvena kaimynai " + Kaimynas.Name + " ir " + Kaimyne.Name);
            Console.WriteLine(Kaimynas.ToString());
            Console.WriteLine(Kaimyne.ToString());
            Console.WriteLine("Jų mėgstamiausiai valgiai yra " + Kaimynas.FavoriteMeal + " ir " + Kaimyne.FavoriteMeal);
            Console.WriteLine(Kaimynas.Name + " gimė " + Kaimynas.GetBirthYear());
            Console.WriteLine(Kaimyne.Name + " gimė " + Kaimyne.GetBirthYear());
            Console.WriteLine("Jie turi sūnų, kurio vardas " + Kaimyniukas.Name);
            Console.WriteLine();

            //STATIC

            //negalima daryti Kaimynas.secret arba Kaimyne.secret, bet:
            Console.WriteLine("Pirma paslaptis: " + Person.secret);
            Person.secret = "abc";
            Console.WriteLine("Antra paslaptis: " + Person.secret);

            Console.ReadKey();
        }
    }
}
