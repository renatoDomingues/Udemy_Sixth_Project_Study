using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO; // => to file manipulation 
using System.Data.Common;

namespace Sixth_Project
{
    internal class Program
    {
        //Create same variables
        static string startDelimiter;
        static string endDelimiter;
        static string tagName;
        static string tagBirth;
        static string tagStreetName;
        static string tagHouseNumber;

        //Create a struct with name registration data
        public struct dataRegistrationStruct
            {
                //varible below
                public string name;
                public DateTime birthName;
                public string streetName;
                public UInt32 numberHouse;
            }
            //Create enum
            public enum result_and
            {
                sucess = 0,
                exit = 1,
                excetion = 2
            }
            //First method with void below
            public static void showMessage(string message)
            {
                Console.WriteLine(message);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();
            }
            //Other method with mofifie 'ref' to be changed
            public static result_and takeString(ref string myString, string message)
            {
                result_and back;
                Console.WriteLine(message);
                string time = Console.ReadLine();

                //Condition
                if (time == "e" || time == "E")
                {
                    back = result_and.exit;
                }
                else
                {
                    myString = time;
                    back = result_and.sucess;
                }

                Console.Clear();
                return back;
            }
            //Other method, now is to date
            public static result_and takeDate(ref DateTime date, string message)
            {
                //create a variable with name back
                result_and back;

                //repetition
                do
                {

                    //excess/exception treatment
                    try
                    {
                        Console.WriteLine(message);
                        string time = Console.ReadLine();//=>take a temporary message

                        //Loop
                        if (time == "e" || time == "E")
                        {
                            back = result_and.exit;
                        }
                        else
                        {
                            date = Convert.ToDateTime(time);
                            back = result_and.sucess;
                        }
                    }
                    catch (Exception and)
                    {
                        //if there are any errors
                        Console.WriteLine("EXCEPTION: " + and.Message);
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        Console.Clear();
                        back = result_and.excetion;
                    }
                } while (back == result_and.excetion);

                Console.Clear();
                return back;
            }
            //Now method to number house with 'UInt32'
            public static result_and takeUInt32(ref UInt32 numberUInt32, string message)
            {
                //create a variable with name back
                result_and back;

                //repetition
                do
                {

                    //excess/exception treatment
                    try
                    {
                        Console.WriteLine(message);
                        string time = Console.ReadLine();//=>take a temporary message

                        //Loop
                        if (time == "e" || time == "E")
                        {
                            back = result_and.exit;
                        }
                        else
                        {
                            numberUInt32 = Convert.ToUInt32(time);
                            back = result_and.sucess;
                        }
                    }
                    catch (Exception and)
                    {
                        //if there are any errors
                        Console.WriteLine("EXCEPTION: " + and.Message);
                        Console.WriteLine("Press any key to continue ...");
                        Console.ReadKey();
                        Console.Clear();
                        back = result_and.excetion;
                    }
                } while (back == result_and.excetion);

                Console.Clear();
                return back;
            }
            //Finally method to registration users
            public static result_and registrationUser(ref List<dataRegistrationStruct> listUsers)
            {
                dataRegistrationStruct registrationUser;
                registrationUser.name = "";
                registrationUser.birthName = new DateTime();
                registrationUser.streetName = "";
                registrationUser.numberHouse = 0;

                //Loop
                if (takeString(ref registrationUser.name, "What is your full name or type ' E ' to exit: ") == result_and.exit)
                {
                    return result_and.exit;
                }
                if (takeDate(ref registrationUser.birthName, "How old are you, please type DD/MM/AAAA or type ' E ' to exit: ") == result_and.exit)
                {
                    return result_and.exit;
                }
                if (takeString(ref registrationUser.streetName, "What is the name of your street or type ' E ' to exit: ") == result_and.exit)
                {
                    return result_and.exit;
                }
                if (takeUInt32(ref registrationUser.numberHouse, "What is the number of your house or type ' E ' to exit: ") == result_and.exit)
                {
                    return result_and.exit;
                }
                //to salve
                listUsers.Add(registrationUser);

                //If you manage to register the users
                return result_and.sucess;
            }

            //methods below will go records
            public static void recordData(string way, List<dataRegistrationStruct> listUsers) 
            {
                //exception treatment
                try 
                {
                    //Create a variable below, that will to be treatment ' listUser ' abouve
                    string fileContent = "";

                    foreach (dataRegistrationStruct registration in listUsers)
                    {
                        fileContent += startDelimiter + "\r\n";
                        fileContent += tagName + registration.name + "\r\n";
                        fileContent += tagBirth + registration.birthName.ToString("dd/MM/yyyy") + "\r\n";
                        fileContent += tagStreetName + registration.streetName + "\r\n";
                        fileContent += tagHouseNumber + registration.numberHouse + "\r\n";
                        fileContent += endDelimiter + "\r\n";
                    }

                    //to salve datas abouve
                    File.WriteAllText(way, fileContent);
                }
                catch(Exception and) 
                {
                Console.WriteLine("EXCEPTION: " + and.Message);
                }
            }

            //Other method to go load datas with yours parameters
            public static void datasLoad(string way, List<dataRegistrationStruct> listUsers) 
            {
                //Exception treatment
                try 
                {
                    //condition
                    if(File.Exists(way)) 
                    {
                        //create some variables together a variable with array
                        string[] fileContent = File.ReadAllLines(way);
                        dataRegistrationStruct dataRegistration;
                        dataRegistration.name = ""; // => just so the compiler doen´t complain
                        dataRegistration.birthName = new DateTime();
                        dataRegistration.streetName = "";
                        dataRegistration.numberHouse = 0;

                        //call loop
                        foreach (string line in fileContent) 
                        {
                            //condition
                            if (line.Contains(startDelimiter)) 
                            {
                                continue; // => you don´t want to do anything, and go on to the next one.
                            }
                            if (line.Contains(endDelimiter))
                            {
                                listUsers.Add(dataRegistration);
                            }
                            if (line.Contains(tagName)) 
                            {
                                dataRegistration.name = line.Replace(tagName, "");
                            }
                            if (line.Contains(tagBirth))
                            {
                                dataRegistration.birthName = Convert.ToDateTime(line.Replace(tagBirth, ""));
                            }
                            if (line.Contains(tagStreetName))
                            {
                                dataRegistration.streetName = line.Replace(tagStreetName, "");
                            }
                            if (line.Contains(tagHouseNumber))
                            {
                                dataRegistration.numberHouse = Convert.ToUInt32(line.Replace(tagHouseNumber, ""));
                            }
                        }
                    }
                }
                catch (Exception and) 
                {
                    Console.WriteLine("EXCEPTION: " + and.Message);
                }
            }

            static void Main(string[] args) // => this is method ' Main '
            {

            //Sixth project
            List<dataRegistrationStruct> listUsers = new List<dataRegistrationStruct>();

            //variable
            string option = "";
            startDelimiter = " ##### START ##### ";
            endDelimiter = " ##### END ##### ";
            tagName = " Name: ";
            tagBirth = " Birth Date: ";
            tagStreetName = " Street Name: ";
            tagHouseNumber = " House Number: ";

            //Create a variable to records ours users registration
            string fileWay = @"fileBase.txt";

            //create method to load ours full datas
            datasLoad(fileWay, listUsers); // => 15:35 minutes errors ' ref listUsers '

            //Loop/Repetion
            do
            {
                Console.WriteLine("Type R to registration new user or E to exit: ");
                option = Console.ReadKey(true).KeyChar.ToString().ToUpper();

                //Condition
                if (option == "R")
                {
                    //Registration a new user
                    if(registrationUser(ref listUsers) == result_and.sucess) 
                    {
                        recordData(fileWay, listUsers); // => a errors
                    }

                }
                else if (option == "E")
                {
                    //Exit application
                    showMessage("Closing the program ...");

                }
                else
                {
                    //Option unknown
                    showMessage("Option unknown ...");

                }

            }
            while (option != "E");
            }
    }
}
