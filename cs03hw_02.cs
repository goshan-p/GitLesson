using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter your role (Admin/Customer/Seller): ");
        string role;
        while (!TryParseRole(Console.ReadLine(), out role))
        {
            Console.WriteLine("Invalid role. Please enter a valid role (Admin/Customer/Seller): ");
        }

        User user = CreateUser(role);

        Console.Write("Enter a command (DeleteOrder/CreateOrder/SendOrder): ");
        string command;
        while (!TryParseCommand(Console.ReadLine(), out command))
        {
            Console.WriteLine("Invalid command. Please enter a valid command (DeleteOrder/CreateOrder/SendOrder): ");
        }

        user.ExecuteCommand(command);
    }

    static bool TryParseRole(string inputRole, out string role)
    {
        role = inputRole;
        return IsValidRole(role);
    }

    static bool TryParseCommand(string inputCommand, out string command)
    {
        command = inputCommand;
        return IsValidCommand(command);
    }

    static bool IsValidRole(string role)
    {
        return role == "Admin" || role == "Customer" || role == "Seller";
    }

    static bool IsValidCommand(string command)
    {
        return command == "DeleteOrder" || command == "CreateOrder" || command == "SendOrder";
    }

    static User CreateUser(string role)
    {
        switch (role)
        {
            case "Admin":
                return new Admin();
            case "Customer":
                return new Customer();
            case "Seller":
                return new Seller();
            default:
                throw new ArgumentException("Invalid role");
        }
    }
}

class User
{
    protected Privilege privilege;

    public User(string role)
    {
        SetRole(role);
    }

    protected void SetRole(string role)
    {
        Role = role;
        privilege = PrivilegeMaster.GetPrivilege(role);
    }

    public string Role { get; private set; }

    public void Authorization()
    {
        Console.WriteLine("Authorization completed.");
    }

    public void ExecuteCommand(string command)
    {
        if (command == "SendOrder" && privilege.Code >= 2)
        {
            Console.WriteLine("Sending order...");
        }
        else if (command == "DeleteOrder" && privilege.Code >= 3)
        {
            Console.WriteLine("Deleting order...");
        }
        else if (command == "CreateOrder" && privilege.Code >= 1)
        {
            Console.WriteLine("Creating order...");
        }
        else
        {
            Console.WriteLine("NoAccess: You do not have permission for this command.");
        }
    }

    public Privilege GetPrivilege()
    {
        return privilege;
    }
}

class Admin : User
{
    public Admin() : base("Admin") { }
}

class Customer : User
{
    public Customer() : base("Customer") { }
}

class Seller : User
{
    public Seller() : base("Seller") { }
}

class Privilege
{
    public readonly int Code;

    public Privilege(int code)
    {
        Code = code;
    }
}

class PrivilegeMaster
{
    public static Privilege GetPrivilege(string role)
    {
        int code = 0;
        switch (role)
        {
            case "Admin":
                code = 3;
                break;
            case "Customer":
                code = 1;
                break;
            case "Seller":
                code = 2;
                break;
            default:
                throw new ArgumentException("Invalid role");
        }

        return new Privilege(code);
    }
}
