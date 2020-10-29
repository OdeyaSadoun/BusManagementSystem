using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace dotNet5781_01_0406_3977
{
    class Program
    {


        static Bus FindBusInList(List<Bus> busses, string licenseNumber)
            //find the bus in the busses list.
        {
            int tempIndex = -1;
            string tempNumber = Bus.VisualShapeOfLicenseNumber(licenseNumber);
            for (int i = 0; i < busses.Count; i++)
            {
                if (busses[i].LicenseNumber == tempNumber)
                {
                    tempIndex = i;
                    break;
                }
            }
            if (tempIndex == -1)// The licenseNumber doesnt found
                throw new ArgumentException("The bus doesn't exsist");
            return busses[tempIndex];
        }

        static void Main(string[] args)
        {

            Random rnd = new Random(DateTime.Now.Millisecond);
            List<Bus> busses = new List<Bus>();

                Menu choice;
            do
            {
                  Console.WriteLine("\nEnter 1 to add buss to the busses list\n" +
                                      "Enter 2 to choose bus for drive\n" +
                                      "Enter 3 to fuel or to take care of the bus\n" +
                                      "Enter 4 to display the mileage of all the busses from the last care\n" +
                                      "Enter 5 to exit\n");
                choice = (Menu)int.Parse(Console.ReadLine());
                try
                {

                    switch (choice)
                    {
                        case Menu.Add://add buss to the busses list
                            Console.WriteLine("Enter a license number and the date of begining bus activity");
                            string licenseNumber = Console.ReadLine();
                            DateTime dateBegin = new DateTime();
                            DateTime.TryParse(Console.ReadLine(), out dateBegin);
                            int temp = -1;
                            for (int i = 0; i < busses.Count; i++) // check if the bus exist
                            {
                                if (busses[i].LicenseNumber == licenseNumber)
                                {
                                    temp = i;
                                    break;
                                }
                            }
                            if (temp != -1)
                                throw new ArgumentException("The bus exsist, could not add it to the list");
                            Bus b = new Bus(licenseNumber, dateBegin);

                            Console.WriteLine("Do you want to enter mileage? enter 0 to NO, enter 1 to YES");
                            int temp1 = int.Parse(Console.ReadLine());
                            if (temp1 == 1)
                            {
                                double mileage = double.Parse(Console.ReadLine());
                                b.SumMileage = mileage;
                            }
                            Console.WriteLine("Do you want to enter fuel? enter 0 to NO, enter 1 to YES");
                            int yesOrNo = int.Parse(Console.ReadLine());
                            if (yesOrNo == 1)
                            {
                                double fuel = double.Parse(Console.ReadLine());
                                b.Fuel = fuel;
                            }
                            busses.Add(b);//Add the new bus to busses list
                            break;

                        case Menu.Choose://Choose bus for drive
                            Console.WriteLine("Enter a license number");
                            licenseNumber = Console.ReadLine();
                            Bus b1 = FindBusInList(busses, licenseNumber); //busses[temp] == b1
                            int km = rnd.Next(1,20000); //maximum drive withot care
                            if ((km + b1.SumMileage) > 20000)//The sum of mileage more 20000
                                throw new ArgumentException("The bus cant perform this travel - need to care");
                            DateTime currentDate = DateTime.Now;
                            if (b1.YearPassed())
                                throw new ArgumentException("The bus cant perform this travel because a year passed from the last care- need to care");
                            if (b1.KmBeforeFuel - km <= 0)
                                throw new ArgumentException("The fuel isnt enough for travel, you should fuel");

                            else//The bus is exsist and can perform this travel
                            {
                                b1.SumMileage += km;
                                b1.currentMileage = km;
                                b1.KmBeforCare -= km;
                                b1.KmBeforeFuel -= km;
                            }
                            break;

                        case Menu.Care://Fuel or take care of the bus
                            Console.WriteLine("Enter a license number");
                            licenseNumber = Console.ReadLine();
                            b1 = FindBusInList(busses, licenseNumber); //busses[temp] == b1
                            Console.WriteLine("Do you want to care the bus or fuel it? enter 0 to care, enter 1 to fuel");
                            int careOrFuel = int.Parse(Console.ReadLine());
                            if (careOrFuel == 1)//fuel
                            {
                                b1.KmBeforeFuel = 1200;
                            }
                            else if (careOrFuel == 0) //care
                            {
                                currentDate = DateTime.Now;
                                b1.LastCare = currentDate;
                                b1.KmBeforCare = 20000;
                            }
                            break;

                        case Menu.Km://Display the mileage of all the busses from the last care
                            Console.WriteLine("The mileage of all the busses from the last care:\n");
                            for (int i = 0; i < busses.Count; i++)
                            {
                                Console.WriteLine("The License number is: {0}, The mileage is: {1}", busses[i].LicenseNumber, busses[i].SumMileage);
                            }
                            break;

                        case Menu.Exit:
                            break;

                        default:
                            Console.WriteLine("error number! please enter a number again");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            while (choice != Menu.Exit);

        }
    }
}
