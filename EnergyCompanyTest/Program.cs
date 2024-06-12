// See https://aka.ms/new-console-template for more information
using EnergyCompanyExercise;
using System.Diagnostics.CodeAnalysis;

internal class Program

{
    private static void Main(string[] args)
    {
        int input;
        bool confirmExit = false;
        EnergyCompany energyCompany = new();

        do
        {
            PromptUserOptions();
            try
            {
                input = Convert.ToInt32(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        InsertEndPoint(energyCompany);
                        break;
                    case 2:
                        EditEndPoint(energyCompany);
                        break;
                    case 3:
                        DeleteEndPoint(energyCompany);
                        break;
                    case 4:
                        ListEndPoints(energyCompany);
                        break;
                    case 5:
                        FindEndPoint(energyCompany);
                        break;
                    case 6:
                        confirmExit = ConfirmUserExit();
                        break;
                }
            } catch (FormatException e)
            {
                ConsoleMessage(e.Message);
            }            
        } while (!confirmExit);

        Console.WriteLine("Goodbye");
    }
    [return: MaybeNull]
    private static EndPoint ReadEndPointInfo(string serialNumber)
    {
        int modelId = ReadModelID();
        if (modelId == -1)
        {
            ConsoleMessage("Invalid model ID.");
            return null;
        }

        Console.Write("Please type the model number: ");
        int modelNumber = Convert.ToInt32(Console.ReadLine());

        Console.Write("Please type the firmware version: ");
        string? firmwareVersion = Console.ReadLine();
        if (String.IsNullOrEmpty(firmwareVersion))
        {
            ConsoleMessage("Firmware version cannot be empty.");
            return null;
        }

        int state = ReadState();
        if (state == -1)
        {
            ConsoleMessage("Invalid state.");
            return null;
        }
        return new EndPoint(serialNumber, modelId, modelNumber, firmwareVersion, state);
    }
    [return: MaybeNull]
    private static string ReadSerialNumber()
    {
        Console.Write("Please type the serial number: ");
        string? serialNumber = Console.ReadLine();
        if (String.IsNullOrEmpty(serialNumber))
        {
            return null;
        }
        return serialNumber;
    }
    private static int ReadState()
    {
        Console.Write("Please type the switch state: ");
        int state = Convert.ToInt32(Console.ReadLine());
        if (state < 0 || state > 2)
        {
            return -1;
        }
        return state;
    }
    private static int ReadModelID()
    {
        Console.Write("Please type the model ID: ");
        int modelID = Convert.ToInt32(Console.ReadLine());
        if (modelID < 16 || modelID > 19)
        {
            return -1;
        }
        return modelID;
    }
    private static void ListEndPoints(EnergyCompany energyCompany)
    {
        var endpoints = energyCompany.EndPoints;
        if (endpoints.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Listing {endpoints.Count} endpoint(s)");
            Console.WriteLine("==========================");
            endpoints.ForEach(x => {
                Console.WriteLine(x);
                Console.WriteLine();
            });
        }
        else
        {
            ConsoleMessage("There are no endpoints.");
        }
    }
    private static void InsertEndPoint(EnergyCompany energyCompany)
    {
        var serialNumber = ReadSerialNumber();
        if (serialNumber is null)
        {
            ConsoleMessage("Serial number cannot be empty.");
            return;
        }
        Boolean endPointExists = energyCompany.EndPointExists(serialNumber);
        if (!endPointExists)
        {
            EndPoint? endPoint = ReadEndPointInfo(serialNumber);
            if (endPoint is not null)
            {
                energyCompany.AddEndPoint(endPoint);
                ConsoleMessage($"EndPoint \"{serialNumber}\" successfully added.");
            }
        }
        else
        {
            ConsoleMessage($"EndPoint \"{serialNumber}\" already exists.");
        }
    }
    private static void EditEndPoint(EnergyCompany energyCompany)
    {
        var serialNumber = ReadSerialNumber();
        if (serialNumber is null)
        {
            ConsoleMessage("Serial number cannot be empty.");
            return;
        }
        Boolean endPointExists = energyCompany.EndPointExists(serialNumber);
        if (endPointExists)
        {
            EndPoint endPoint = energyCompany.GetEndPoint(serialNumber);
            int nextState = ReadState();
            if (nextState == -1)
            {
                ConsoleMessage("Invalid state.");
                return;
            }
            endPoint.State = nextState;
            ConsoleMessage($"EndPoint \"{serialNumber}\" successfully edited.");
        }
        else
        {
            ConsoleMessage($"EndPoint \"{serialNumber}\" not found.");
        }
    }
    private static void DeleteEndPoint(EnergyCompany energyCompany)
    {
        var serialNumber = ReadSerialNumber();
        if (serialNumber is null)
        {
            ConsoleMessage("Serial number cannot be empty.");
            return;
        }
        Boolean endPointExists = energyCompany.EndPointExists(serialNumber);
        if (endPointExists)
        {
            Console.Write($"Are you sure you want delete the endpoint \"{serialNumber}\"? (Y/N): ");
            var confirmDelete = Console.ReadLine();
            if (String.Equals(confirmDelete, "Y", StringComparison.OrdinalIgnoreCase))
            {
                energyCompany.DeleteEndPoint(serialNumber);
                ConsoleMessage($"EndPoint \"{serialNumber}\" successfully deleted.");
            }
        }
        else
        {
            ConsoleMessage($"EndPoint \"{serialNumber}\" not found.");
        }
    }
    private static void FindEndPoint(EnergyCompany energyCompany)
    {
        var serialNumber = ReadSerialNumber();
        if (serialNumber is null)
        {
            ConsoleMessage("Serial number cannot be empty.");
            return;
        }
        Boolean endPointExists = energyCompany.EndPointExists(serialNumber);
        if (endPointExists)
        {
            EndPoint endPoint = energyCompany.GetEndPoint(serialNumber);
            ConsoleMessage(endPoint.ToString());
        }
        else
        {
            ConsoleMessage($"EndPoint \"{serialNumber}\" not found.");
        }
    }
    private static void ConsoleMessage(string message)
    {
        Console.WriteLine();
        Console.WriteLine(message);
        Console.WriteLine();
    }
    private static Boolean ConfirmUserExit()
    {
        Console.Write("Are you sure you want to exit? (Y/N): ");
        var confirm = Console.ReadLine();
        return String.Equals(confirm, "Y", StringComparison.OrdinalIgnoreCase);
    }
    private static void PromptUserOptions()
    {
        Console.WriteLine("1) Insert a new endpoint");
        Console.WriteLine("2) Edit an existing endpoint");
        Console.WriteLine("3) Delete an existing endpoint");
        Console.WriteLine("4) List all endpoints");
        Console.WriteLine("5) Find an endpoint by serial number");
        Console.WriteLine("6) Exit");
        Console.WriteLine();
        Console.Write("Select an option: ");
    }
}