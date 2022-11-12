
var anotherService = new AnotherService();
anotherService.OnApplicationStartUp();

public class AnotherService
{
    public void OnApplicationStartUp()
    {
        INotification notification;

        Console.WriteLine("Adding now new employee. How would you like to " +
            "send the notification. Enter S if SMS or E if email");

        var modeOfNotification = Console.ReadLine();

        var employeeService = new EmployeeService(NotificationFactory
            .GetNotification(modeOfNotification!));

        employeeService.AddEmployee("Brent");

        Console.ReadLine();
    }
}

public class EmployeeService
{
    private INotification _notification;

    public EmployeeService(INotification notification)
    {
        _notification = notification;
    }

    public void AddEmployee(string employeeName)
    {
        Console.WriteLine("Adding Employee");
        _notification.Send(employeeName + " is added to the system.");
    }
}

public class SMSNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Sending tru sms: " + message);
    }
}

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Sending tru email: " + message);
    }
}

public class SystemNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Sending tru built in system: " + message);
    }
}

public interface INotification
{
    public void Send(string message);
}

public static class NotificationFactory
{
    public static INotification GetNotification(string typeOfNotification)
    {
        if (typeOfNotification.ToUpper() == "S")
        {
            return new SMSNotification();
        }
        else if (typeOfNotification.ToUpper() == "E")
        {
            return new EmailNotification();
        }
        else
        {
            return new SystemNotification();
        }
    }
}