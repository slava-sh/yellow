using System;
using System.Collections.Generic;
using System.Text;
using Game;
using Game.Interaction;

namespace ConsoleApp
{
    internal class Program
    {
        private const int FramesPerSecond = 60;
        private const int FieldWidth = 10;
        private const int FieldHeight = 20;

        private Game.Game game;
        private Dictionary<ConsoleKey, Key> keyMap;
        private bool needsRender;

        private static void Main()
        {
            new Program().Run();
        }

        private void Run()
        {
            var randomSeed = new Random().Next();
            var tetrominoGenerator =
                new RandomBagTetrominoGenerator(randomSeed);
            game = new Game.Game(FieldWidth, FieldHeight,
                tetrominoGenerator);

            var scheduler = new SleepingScheduler(FramesPerSecond);
            var gameController = new GameController(game, scheduler);
            gameController.Update += RequestRender;
            gameController.GameOver += scheduler.Stop;

            var keyboard = new Keyboard(gameController, scheduler);
            keyMap = new Dictionary<ConsoleKey, Key>
            {
                [ConsoleKey.UpArrow] = keyboard.Rotate,
                [ConsoleKey.LeftArrow] = keyboard.ShiftLeft,
                [ConsoleKey.RightArrow] = keyboard.ShiftRight,
                [ConsoleKey.DownArrow] = keyboard.SoftDrop,
                [ConsoleKey.Spacebar] = keyboard.HardDrop,
                [ConsoleKey.W] = keyboard.Rotate,
                [ConsoleKey.A] = keyboard.ShiftLeft,
                [ConsoleKey.D] = keyboard.ShiftRight,
                [ConsoleKey.S] = keyboard.SoftDrop
            };

            scheduler.SetInterval(1, HandleInput);
            scheduler.SetInterval(1, MaybeRender);

            Console.Clear();
            RequestRender();
            scheduler.Run();
            Console.WriteLine(Pad("Game Over"));
            Console.WriteLine();
        }

        private void RequestRender()
        {
            needsRender = true;
        }

        private void HandleInput()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                if (keyMap.ContainsKey(key))
                {
                    keyMap[key].HandleKeyDown();
                    keyMap[key].HandleKeyUp();
                }
            }
        }

        private void MaybeRender()
        {
            if (!needsRender)
            {
                return;
            }
            needsRender = false;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine();
            var mask = game.GetFieldMask();
            for (var y = 0; y < FieldHeight; ++y)
            {
                var line = new StringBuilder();
                for (var x = 0; x < FieldWidth; ++x)
                {
                    line.Append(mask[x, y] ? "#" : "|");
                }
                Console.WriteLine(Pad(line.ToString()));
            }
            Console.WriteLine();
        }

        private static string Pad(string line)
        {
            return "   " + line;
        }
    }
}
