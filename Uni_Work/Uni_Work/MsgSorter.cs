using System;
using System.Collections.Generic;
using System.Timers;

namespace Uni_Work
{
    public class MsgSorter
    {
        public static void Sorter(String msg)
        {
            int loginAttempts = 0;

            switch (msg)
            {
                case "test": {
                    Program.log("Running Test");
                    break;
                }

                case "login":
                {
                    Program.log("Please enter your username");
                    String loginUsername = Console.ReadLine();
                        bool login = false;
                        User us = null;
                        long time = DateTimeOffset.Now.ToUnixTimeMilliseconds();


                        foreach (KeyValuePair<String, User> entry in Data.Accounts) {


                            string id = entry.Key;

                            string username = entry.Value.Username;
                            string password = entry.Value.Password;
                            long FailLoginTime = entry.Value.LastLoginAttempt;

                            Program.log(username);

                            if (loginUsername.Equals(username)) {


                                TimeSpan t = TimeSpan.FromMilliseconds(FailLoginTime - time);
                                string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                                        t.Hours,
                                                        t.Minutes,
                                                        t.Seconds,
                                                        t.Milliseconds);


                                if (time < FailLoginTime ) {
                                    Program.log("Failed to login-- Account currently locked for " + answer);

                                    return;
                                }


                                login = true;
                                us = entry.Value;
                            }


                            if (login)
                            {
                                Program.log("Hello, " + us.Username + " Please enter your password ");
                                String givenPassword = Console.ReadLine();

                                if (givenPassword.Equals(us.Password))
                                {
                                    us.LoginAttempt = 0;
                                    Program.log("You have sucessfully logged in");
                                }
                                else
                                {
                                    Program.log("You have entered the wrong pasword, Attempts: " + us.LoginAttempt++);

                                    if (us.LoginAttempt >= int.Parse(Data.settingsMap["AllowedSignInAttempts"])) us.LastLoginAttempt = time + long.Parse(Data.settingsMap["FailLoginCooldown"]);
                                }



                            }
                            else { Program.log("User not found"); }




                           


                        }




            break;

                }


            }
        
            
        }
    }
}