using System;
using System.Collections.Generic;
using static System.Console;


namespace House
{
    class Program
    {
        static void Main()
        {
            House house = new House();
            Team team = new Team();

            WriteLine(team.Name);

            Random r = new Random();

            for (int i = 0; i < 6; i++)
            {
                team.w[r.Next(0, 3)].Build(house, team.t);
            }

            foreach (var a in team.t.report)
            {
                WriteLine(a);
            }

            team.t.Report();
            WriteLine();
            for (int i = 0; i < 5; i++)
            {
                team.w[r.Next(0, 3)].Build(house, team.t);
            }

            foreach (var a in team.t.report)
            {
                WriteLine(a);
            }
            team.t.Report();

            house.Paint(team.t);
            ReadKey();
        }
    }

    interface IWorker
    {
        string Name { get; }
    }
    interface IPart
    {
        void Do(House house);
    }

    class Basement : IPart
    {
        public void Do(House house)
        {
            house.basement = new Basement();
        }
    }

    class Walls : IPart
    {
        public void Do(House house)
        {
            house.walls.Add(new Walls());
        }
    }

    class Door : IPart
    {
        public void Do(House house)
        {
            house.door = new Door();
        }
    }

    class Window : IPart
    {
        public void Do(House house)
        {
            house.window.Add(new Window());
        }
    }

    class Roof : IPart
    {
        public void Do(House house)
        {
            house.roof = new Roof();
        }
    }

    class House
    {
        public Basement basement;
        public List<Walls> walls;
        public List<Window> window;
        public Door door;
        public Roof roof;

        public void Paint(TeamLeader t)
        {
            if (t.report.Count == 11)
            {

                string domik = @"
                           
                              
                               
                          (    )
                            )  )
                           (  (                 
                            (_)                 
                    ________[_]________      
                   /\        ______    \      
                  //_\       \    /\    \  
                 //___\       \__/  \    \
                //_____\       \ |[]|     \
               //_______\       \|__|      \
              /XXXXXXXXXX\                  \
             /_I_II  I__I_\__________________\
               I_I|  I__I_____[]_|_[]_____I
               I_II  I__I_____[]_|_[]_____I
               I II__I  I     XXXXXXX     I
            ~~~~~'   '~~~~~~~~~~~~~~~~~~~~~~~~";

                Console.WriteLine(domik);
            }
            else WriteLine("Дом еще не готов");
        }
    }

    class Team : IWorker
    {
        public TeamLeader t;
        public List<Worker> w;
        public string Name { get => "СТройтрестдом"; }

        public Team()
        {
            t = new TeamLeader("Степан");
            w = new List<Worker> { new Worker("Виталий"), new Worker("Петр"), new Worker("Андрей"), new Worker("Иван") };
        }


    }

    class Worker : IWorker
    {
        string Name { get; set; }

        string IWorker.Name => Name;

        public Worker(string name)
        { Name = name; }

        public void Build(House house, TeamLeader t)
        {
            if (house.basement == null)
            {
                Basement basement = new Basement();
                basement.Do(house);
                t.report.Add($"Worker {Name} построил фундамент!");
            }
            else if (house.walls == null || house.walls.Count < 4)
            {
                if (house.walls == null) house.walls = new List<Walls>();
                Walls wall = new Walls();
                wall.Do(house);
                t.report.Add($"Worker {Name} построил стену {house.walls.Count}!");
            }
            else if (house.door == null)
            {
                Door door = new Door();
                door.Do(house);
                t.report.Add($"Worker {Name} построил дверь!");

            }

            else if (house.window == null || house.window.Count < 4)
            {
                if (house.window == null) house.window = new List<Window>();
                Window window = new Window();
                window.Do(house);
                t.report.Add($"Worker {Name} сделал окно {house.window.Count}!");

            }

            else if (house.roof == null)
            {
                Roof roof = new Roof();
                roof.Do(house);
                t.report.Add($"Worker {Name} сделал крышу!");

            }

        }



    }

    class TeamLeader : IWorker
    {
        string Name { get; set; }
        public List<string> report = new List<string>();
        string IWorker.Name => Name;

        public TeamLeader(string name)
        { Name = name; }
        public void Report()
        {
            double d = (report.Count / 11.0) * 100;
            WriteLine($"{(int)d} % работы выполнено!");
        }
    }

}