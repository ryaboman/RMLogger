using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;
//using System.Xml.Linq;

namespace RMLogging {

    public enum Level {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }

    public class Logger {
        //Нужно записать дату запуска ПО 

        public static bool logAllow = false;

        //структура файла: уровень логироваания: [message]
        string name;
        Dictionary<string, string> attributes;

        List<Target> targets;
        List<Rule> rules;

        public Logger() {
            attributes = new Dictionary<string, string>();
            attributes.Add("color", "red");

            DateTime dateTime = DateTime.Today;
            string shortdate = dateTime.ToShortDateString().Replace(".", "-");
            string basedir = Directory.GetCurrentDirectory();
            string path = basedir + @"\logs\" + shortdate + ".log";
            attributes.Add("fileName", path);
        }


        private void write(string message){
            
        }


        public void Debug(string message, string className) {            
            if (logAllow) {
                var frame = new StackFrame(1);
                string method = frame.GetMethod().ToString();
                string classs = frame.GetMethod().DeclaringType.Name.ToString();
                Console.WriteLine("Метод: " + method);
                Console.WriteLine("Класс: " + classs);

                string logLevel = "debug";
                Type type = typeof(Target);

                Target target = GetTypeTarget("ColoredConsole");
                target.attributes = attributes;
                target.Write(message);
            }
        }

        public void Info(string message) { 
            //Проверить правила
            //Если есть правила для Info, то получаем источники
            //В полученные источники проводим запись
        
        }

        public void Warning(string message, string className) {
            if (logAllow) {
                Target targetFile = GetTypeTarget("File");
                targetFile.attributes = attributes;
                targetFile.Write(message);
            }
        }

        public void Error(string message) {

        }

        /// <summary>
        /// Получить класс соответсвующий запрашиваемому типу источника
        /// </summary>
        /// <param name="typeTarget">Тип источника</param>
        /// <returns>Возвращает тип класса источника</returns>
        private Target GetTypeTarget(string typeTarget) {
            Type type = typeof(Target);
            IEnumerable<Type> type_list = Assembly.GetAssembly(type).GetTypes().Where(x => type.IsAssignableFrom(x));
            foreach (Type item in type_list) {
                var att = item.GetCustomAttributes(typeof(TargetAttribute), true).FirstOrDefault() as TargetAttribute;
                if (att == null) {
                    continue;
                }
                if(att.typeTarget == typeTarget){
                    return (Target)Activator.CreateInstance(item);
                }
            }
            return null;
        }

    }
}
