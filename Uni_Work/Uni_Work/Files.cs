using System;
using System.IO;
using YamlDotNet.RepresentationModel;
using System.Text.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Uni_Work
{
    public class Files
    {

   

      

        public static void start() {

        

            Files.Load();





        
        }



        public static void Load()
        {

           

            var reader = new StreamReader("config.yml");
            var yaml = new YamlStream();
            yaml.Load(reader);

            var map = (YamlMappingNode) yaml.Documents[0].RootNode;

            foreach (var entry in map.Children)
            {
                Data.settingsMap.Add(((YamlScalarNode)entry.Key).Value, ((YamlScalarNode)entry.Value).Value);
                Console.WriteLine(((YamlScalarNode)entry.Value).Value);
            }




            //JSOn loading k


            

            Console.WriteLine(File.ReadAllText("users.json"));

          
            
            using (StreamReader r = new StreamReader("users.json"))
            {
                string json = r.ReadToEnd();
       
                Dictionary<String, User> usersMap =  JsonConvert.DeserializeObject<Dictionary<string, User>>(json);
                Data.Accounts = usersMap;
                Program.log("Loaded accounts map");
            }



           





























        }

        public static void save()
        {
            
        }

        public static void resetMap()
        {
            Data.settingsMap.Add("Username", "");
            Data.settingsMap.Add("Password", "");
            Data.settingsMap.Add("AllowedSignInAttempts", "3");
            Data.settingsMap.Add("LastLoginAttempt", "0");
            Data.settingsMap.Add("FailLoginCooldown", "180000");
            
        }

        public static void createFile()
        {
            
            if (!File.Exists("config.yml"))
            {
                
                using (StreamWriter sw = File.CreateText("config.yml"))
                {
                    resetMap();
                    foreach (var entry in Data.settingsMap ) {
                        sw.WriteLine("{0}: {1}", entry.Key, entry.Value); 
                        
                    }
                    sw.Close();
                    Console.Out.WriteLine("Created config.yml");
                }
            }

            if (!File.Exists("users.yml")) File.CreateText("users.yml");
        }

    }

   
}