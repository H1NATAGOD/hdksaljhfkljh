namespace GarageConsoleApp;

/// <summary>
/// Класс Program
/// здесь описываем логику приложения
/// вызываем нужные методы пишем обработчики и тд
/// </summary>
public class Program 
{
    public static void Main(string[] args)
    {
        
        int Userid = DatabaseRequests.Authentication();
        Console.WriteLine(Userid);
        int flag1 = int.Parse(Console.ReadLine());
        while (flag1 != 0)
        {
            switch (flag1)
            {


                case 1:
                    DatabaseRequests.GetAllUserTask(Userid);
                    break;

                case 2:
                    

                    DatabaseRequests.AddNewTask(Userid);
                    break;

                case 3:
                    DatabaseRequests.TodayTask(Userid);
                    break;
      
                case 4:
                    DatabaseRequests.TommorowTask(Userid);
                    break;
      
                case 5:
                    DatabaseRequests.WeekTask(Userid);
                    break;
      
                case 6:
                    DatabaseRequests.DeleteTask(Userid);
                    break;
                
                case 7:
                    DatabaseRequests.UpdateTaskOfUser(Userid);
                    break;


                default:
                    Console.WriteLine($"Извините, команды нет");
                    break;




            }

            flag1 = int.Parse(Console.ReadLine());
        }
        
        
        
    }
}