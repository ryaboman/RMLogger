using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMLogging {

    [Target("Console")]
    class ConsoleTarget : Target {

        public override bool Write(string message) {
            Console.WriteLine(message);
            return true;
        }
    }

    [Target("ColoredConsole")]
    class ColoredConsoleTarget : Target {

        public override bool Write(string message) {
            var defaultForegroundColor = Console.ForegroundColor;
            var defaultBackgroundColor = Console.BackgroundColor;

            switch (attributes["color"]) {
                case "red": {
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                case "yellow": {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }
                case "green": {
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                }                    
                default: {
                        Console.ForegroundColor = defaultForegroundColor;
                        break;
                }
            }
            
            Console.WriteLine(message);

            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;

            return true;
        }
    }
}
