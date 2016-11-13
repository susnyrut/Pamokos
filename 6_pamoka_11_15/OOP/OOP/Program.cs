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

            //objektą galima sukurti dviem būdais
            Person Kaimyne = new Person()
            {
                Name = "Morta",
                Age = 43,
                FavoriteMeal = "Varškės apkepas"
            };

            Console.WriteLine("Kitoj gatvės pusėj gyvena kaimynai " + Kaimynas.Name + " ir " + Kaimyne.Name);
            Console.WriteLine(Kaimynas.ToString());
            Console.WriteLine(Kaimyne.ToString());
            Console.WriteLine("Jų mėgstamiausiai valgiai yra " + Kaimynas.FavoriteMeal + " ir " + Kaimyne.FavoriteMeal);
            Console.WriteLine(Kaimynas.Name + " gimė " + Kaimynas.GetBirthYear());
            Console.WriteLine(Kaimyne.Name + " gimė " + Kaimyne.GetBirthYear());
            Console.ReadKey();
        }
    }
}
