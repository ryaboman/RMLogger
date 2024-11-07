using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RMLogging {
    [Target("File")]
    class FileTarget : Target {
        
        public override bool Write(string message) {
            try {
                string pathToFile = attributes["fileName"];                                             

                //Если директория не существует создадим ее
                string directory = Path.GetDirectoryName(pathToFile);
                if (Directory.Exists(directory) == false) {
                    Directory.CreateDirectory(directory);
                }

                using (StreamWriter writer = new StreamWriter(pathToFile, true)) {
                    writer.WriteLine(message);
                }


                return true;
            }
            catch { 
                //Закрыть файл
                //Нет необходимого атрибута файла
                //Не корретный путь
                return false;
            }
        }


    }
}
