using System;
using System.Collections.Generic;

public interface IUser
{
    void Login();
    void Logout();
}

public abstract class AUser : IUser
{
    public bool IsBlocked { get; set; }

    public void Login()
    {
        Console.WriteLine($"Пользователь {GetType().Name} вошел в систему.");
    }

    public void Logout()
    {
        Console.WriteLine($"Пользователь {GetType().Name} вышел из системы.");
    }

    public virtual bool SendMessage(IUser user, string text)
    {
        return true;
    }

    public static Type[] GetAllKindOfUsers()
    {
        return new Type[] { typeof(Admin), typeof(Customer) };
    }
}

public class Order
{
    private static int orderCounter = 0;
    public int OrderNumber { get; }

    public Order()
    {
        OrderNumber = ++orderCounter;
    }
}

public class Admin : AUser
{
    public bool BlockUser(IUser user)
    {
        if (user is AUser aUser)
        {
            aUser.IsBlocked = true;
            return true;
        }
        return false;
    }
}

public class Customer : AUser
{
    public Order CreateOrder()
    {
        if (IsBlocked)
        {
            Console.WriteLine("Ваш аккаунт заблокирован. Невозможно создать новый заказ.");
            return null;
        }

        return new Order();
    }

    public override bool SendMessage(IUser user, string text)
    {
        if (IsBlocked || (user is Customer customer && customer.IsBlocked))
        {
            return false;
        }

        return true;
    }
}

class Program
{
    static AUser currentUser = null;
    static List<Order> allOrders = new List<Order>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите пользователя для входа (admin, customer1, customer2):");
            string userType = Console.ReadLine();

            if (currentUser != null)
            {
                currentUser.Logout();
            }

            if (userType.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                currentUser = new Admin();
            }
            else if (userType.Equals("customer1", StringComparison.OrdinalIgnoreCase))
            {
                currentUser = new Customer();
            }
            else if (userType.Equals("customer2", StringComparison.OrdinalIgnoreCase))
            {
                currentUser = new Customer();
            }
            else
            {
                Console.WriteLine("Неверный тип пользователя. Повторите попытку.");
                continue;
            }

            currentUser.Login();

            while (true)
            {
                Console.WriteLine("Введите команду (SendMessage, BlockUser, CreateOrder, Logout, Exit):");
                string command = Console.ReadLine();

                if (command.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                switch (command)
                {
                    case "SendMessage":
                        Console.WriteLine("Введите получателя сообщения:");
                        string recipientType = Console.ReadLine();
                        IUser recipient = null;

                        if (recipientType.Equals("admin", StringComparison.OrdinalIgnoreCase))
                        {
                            recipient = new Admin();
                        }
                        else if (recipientType.Equals("customer1", StringComparison.OrdinalIgnoreCase))
                        {
                            recipient = new Customer();
                        }
                        else if (recipientType.Equals("customer2", StringComparison.OrdinalIgnoreCase))
                        {
                            recipient = new Customer();
                        }
                        else
                        {
                            Console.WriteLine("Неверный тип получателя. Повторите попытку.");
                            continue;
                        }

                        Console.WriteLine("Введите текст сообщения:");
                        string messageText = Console.ReadLine();
                        bool messageSent = currentUser.SendMessage(recipient, messageText);
                        Console.WriteLine(messageSent ? "Сообщение успешно отправлено." : "Ошибка при отправке сообщения.");
                        break;

                    case "BlockUser":
                        if (currentUser is Admin adminUser)
                        {
                            Console.WriteLine("Введите пользователя для блокировки:");
                            string userToBlockType = Console.ReadLine();
                            IUser userToBlock = null;

                            if (userToBlockType.Equals("admin", StringComparison.OrdinalIgnoreCase))
                            {
                                userToBlock = new Admin();
                            }
                            else if (userToBlockType.Equals("customer1", StringComparison.OrdinalIgnoreCase))
                            {
                                userToBlock = new Customer();
                            }
                            else if (userToBlockType.Equals("customer2", StringComparison.OrdinalIgnoreCase))
                            {
                                userToBlock = new Customer();
                            }
                            else
                            {
                                Console.WriteLine("Неверный тип пользователя для блокировки. Повторите попытку.");
                                continue;
                            }

                            bool userBlocked = adminUser.BlockUser(userToBlock);
                            Console.WriteLine(userBlocked ? "Пользователь успешно заблокирован." : "Ошибка при блокировке пользователя.");
                        }
                        else
                        {
                            Console.WriteLine("У вас нет прав для выполнения этой команды.");
                        }
                        break;

                    case "CreateOrder":
                        if (currentUser is Customer customerUser)
                        {
                            Order order = customerUser.CreateOrder();
                            if (order != null)
                            {
                                allOrders.Add(order);
                                Console.WriteLine($"Заказ успешно создан! Всего создано заказов: {allOrders.Count}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("У вас нет прав для выполнения этой команды.");
                        }
                        break;

                    case "Logout":
                        currentUser.Logout();
                        currentUser = null;
                        Console.WriteLine("Выберите пользователя для входа (admin, customer1, customer2):");
                        userType = Console.ReadLine();

                        if (currentUser != null)
                        {
                            currentUser.Logout();
                        }

                        if (userType.Equals("admin", StringComparison.OrdinalIgnoreCase))
                        {
                            currentUser = new Admin();
                        }
                        else if (userType.Equals("customer1", StringComparison.OrdinalIgnoreCase))
                        {
                            currentUser = new Customer();
                        }
                        else if (userType.Equals("customer2", StringComparison.OrdinalIgnoreCase))
                        {
                            currentUser = new Customer();
                        }
                        else
                        {
                            Console.WriteLine("Неверный тип пользователя. Повторите попытку.");
                            continue;
                        }

                        currentUser.Login();
                        break;

                    default:
                        Console.WriteLine("Неверная команда. Повторите попытку.");
                        break;
                }
            }

            if (currentUser != null)
            {
                currentUser.Logout();
                currentUser = null;
            }

            Console.WriteLine("Выход из программы.");
            break;
        }
    }
}
