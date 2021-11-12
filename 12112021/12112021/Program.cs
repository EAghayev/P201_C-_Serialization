using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace _12112021
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            string path = @"C:\Users\Eagha\Desktop\CodeLessons\P201";

            DirectoryInfo dirInfo = new DirectoryInfo(path);

            Console.WriteLine(dirInfo.Exists);
            Console.WriteLine(dirInfo.Name);
            Console.WriteLine(dirInfo.FullName);
            Console.WriteLine(dirInfo.CreationTime);
            Console.WriteLine(dirInfo.LastAccessTime);
            Console.WriteLine(dirInfo.LastWriteTime);

            Console.WriteLine("sub files:");
            foreach (var item in dirInfo.GetFiles())
            {
                if (item.LastWriteTime.Date == DateTime.Now.Date)
                {
                    item.Delete();
                }
                Console.WriteLine($"{item.Name} - {item.CreationTime} - {item.LastAccessTime} - {item.LastWriteTime}");
            }

            Console.WriteLine("sub folders:");
            foreach (var item in dirInfo.GetDirectories())
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("\n===========================================\n");

            Console.WriteLine("all sub directories:");
            foreach (var item in dirInfo.GetDirectories())
            {
                Console.WriteLine(item.Name);
                Console.WriteLine("\t files:");
                foreach (var file in item.GetFiles())
                {
                    Console.WriteLine("\t "+file.Name + " "+file.Length/1024);
                }
            }

            Console.WriteLine("\n===========================================\n");

            FileInfo newFileInfo = new FileInfo(path + @"\test.txt");

            if(!newFileInfo.Exists)
            {
                var newFileStream = newFileInfo.Create();
                newFileStream.Close();
            }


            string[] content = new string[]
            {
                "Hello",
                "Salam",
                "Merhaba"
            };

            File.WriteAllLines(newFileInfo.FullName, content);

            foreach (var file in dirInfo.GetFiles())
            {
                Console.WriteLine("lines: ");
                foreach (var line in File.ReadAllLines(file.FullName))
                {
                    Console.WriteLine("\t " + line);
                }
            }


            Console.WriteLine("\n===========================================\n");

            //User user = new User
            //{
            //    Name = "Hikmet",
            //    Surname = "Abbasov",
            //    Age = 90
            //};

            //SerializeBinary(user);

            User existUser = DeserializeBinary();
            Console.WriteLine(existUser);
        }

        public static void SerializeBinary(User user)
        {
            Stream stream = new FileStream(@"C:\Users\Eagha\Desktop\User.dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(stream,user);
        }

        public static User DeserializeBinary()
        {
            Stream stream = new FileStream(@"C:\Users\Eagha\Desktop\User.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();

            var data = (User)bf.Deserialize(stream);
            return data;
        }
    }
}
