using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_0406_3977
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            List<Bus> busses = new List<Bus>();


            Console.WriteLine("Enter 0 to add buss to the busses list\n" +
                              "Enter 1 to choose bus for drive\n" +
                              "Enter 3 to fuel or to take care of the bus\n" +
                              "Enter 4 to display the mileage of all the busses from the last care\n" +
                              "Enter 5 to exit\n");

            Menu choice = (Menu)int.Parse(Console.ReadLine());
            do
            {
                switch (choice)
                {
                    case Menu.Add://add buss to the busses list
                        Console.WriteLine("Enter a license number and the date of begining bus activity" );
                        string licenseNumber = Console.ReadLine();
                        Date dateBegin = new Date();
                        dateBegin.day = int.Parse(Console.ReadLine());
                        dateBegin.month = int.Parse(Console.ReadLine());
                        dateBegin.year = int.Parse(Console.ReadLine());
                        Bus b = new Bus(licenseNumber, dateBegin);

                        Console.WriteLine("Do you want to enter mileage? enter 0 to NO, enter 1 to YES");
                        int temp1 = int.Parse(Console.ReadLine());
                        if (temp1 == 1)
                        {
                            double mileage = double.Parse(Console.ReadLine());
                            b.Mileage = mileage;
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
                        int temp = -1;
                        for(int i=0; i<busses.Count; i++)
                        {
                            if (busses[i].LicenseNumber == licenseNumber)
                            {
                                temp = i;
                                break;
                            }
                        }
                        if (temp == -1)// The licenseNumber doesnt found
                            throw new ArgumentException("The bus doesn't exsist");
                        int km = rnd.Next();
                        if((km+busses[temp].Mileage) > 20000)//The sum of mileage more 20000
                            throw new ArgumentException("The bus cant perform this travel");
                        if(busses[temp].FuelAfterKm-km <= 0)
                            throw new ArgumentException("The fuel isnt enough for travel, you should fuel");
                        
                        else//The bus is exsist and can perform this travel
                        {
                            busses[temp].Mileage += km;
                            busses[temp].FuelAfterKm -= km;
                        }
                        
                        break;
                    case Menu.Care://Fuel or take care of the bus
                        Console.WriteLine("Enter a license number");
                        licenseNumber = Console.ReadLine();
                        temp = -1;
                        for (int i = 0; i < busses.Count; i++)
                        {
                            if (busses[i].LicenseNumber == licenseNumber)
                            {
                                temp = i;
                                break;
                            }
                        }
                        if (temp == -1)// The licenseNumber doesnt found
                            throw new ArgumentException("The bus doesn't exsist");
                        Console.WriteLine("Do you want to care the bus or fuel it? enter 0 to care, enter 1 to fuel" );
                        int careOrFuel = int.Parse(Console.ReadLine());
                        if (careOrFuel == 1)
                        {
                            busses[temp].FuelAfterKm = 1200;
                        }
                        else if (careOrFuel == 0)
                        {
                            DateTime currentDate = DateTime.Now;
                           // busses[temp].LastCare.day = currentDate.

                        }

                        break;
                    case Menu.Km://Display the mileage of all the busses from the last care
                        break;
                    case Menu.Exit:
                        break;
                    default:
                        break;
                }

            }
            while (choice != Menu.Exit);
        }
    }
}
