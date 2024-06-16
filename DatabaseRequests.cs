using System.ComponentModel;
using Npgsql;

namespace GarageConsoleApp;


public static class DatabaseRequests
{
    
    static string formattedDate = "";
    public static void GetAllUserTask(int Userid)
    {
        var querySql = $"SELECT id, nametask, description, date_of_task FROM task WHERE userid = {Userid}; ";


        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());


        using var reader = cmd.ExecuteReader();


        while (reader.Read())
        {
            Console.WriteLine($"Дата: {reader[0]} \nЗадание: {reader[1]} \nЗавершено: {reader[2]} \nfdsafkjadsl: {reader[3]}");
        }
    }

    public static void Registration()
    {
        Console.Write($"Введите имя нового пользователя: ");
        string login = Console.ReadLine();
        Console.Write($"Введите пароль:");
        string pass = Console.ReadLine();
        var querySql = $"INSERT INTO \"user\" (loginUser, password) VALUES('{login}', '{pass}');";


        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());


        using var reader = cmd.ExecuteReader();

    }


    public static int Authentication()
    {
        Console.Write("Введите имя пользователя: ");
        string login = Console.ReadLine();
        Console.Write("Введите пароль: ");
        string pass = Console.ReadLine();
        
        var querySql = $"SELECT iduser FROM \"user\" where loginUser = '{login}' and password = '{pass}'";


        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());


        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            int Userid = reader.GetInt32(0);
            Console.WriteLine("Вход выполнен успешно");
            return Userid;
        }
        else        {
            Console.WriteLine("Неверный логин или пароль");
            return -1;
        }
    }




   

    public static void AddNewTask( int Userid)
    {
        Console.WriteLine("Введите дату выполнения задания в формате (гггг-мм-дд чч:мм):");
        DateTime dateOfCompletion;
        string formattedDate;
        try        {
            dateOfCompletion = DateTime.Parse(Console.ReadLine());
            formattedDate = dateOfCompletion.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch        {
            Console.WriteLine("Неверный формат даты");
            return;
        }
        Console.Write($"Впишите новое название задания:");
        string taskname = Console.ReadLine();
        
        Console.Write($"Впишите новое описание задания:");
        string description = Console.ReadLine();

        var querySql =
            $"INSERT INTO task(date_of_task, nametask, description, userid) VALUES ('{formattedDate}', '{taskname}', {description}, {Userid})";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }
    
    public static void UpdateTaskOfUser(int Userid)
    {
        
        
        Console.Write("Выберете id Задачи, которую хотите Изменить:");
        int idt = int.Parse(Console.ReadLine());
        Console.Write($"Впишите новое название задания:");
        string taskname = Console.ReadLine();
        
        Console.Write($"Впишите новое описание задания:");
        string description = Console.ReadLine();
        
        Console.WriteLine("Введите дату выполнения задания в формате (гггг-мм-дд чч:мм):");
        DateTime dateOfCompletion;
        string formattedDate;
        try        {
            dateOfCompletion = DateTime.Parse(Console.ReadLine());
            formattedDate = dateOfCompletion.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch        {
            Console.WriteLine("Неверный формат даты");
            return;
        }
            
        var querySql2 = $"UPDATE task SET nametask = '{taskname}'," +
                        $"description = '{description}',"+
                        $"date_of_task = '{formattedDate}'" +
                        $"WHERE userid = {Userid} AND idtask = {idt};";
        using var cmd1 = new NpgsqlCommand(querySql2, DatabaseService.GetSqlConnection());
        cmd1.ExecuteNonQuery();
    }
      
      public static void DeleteTask(int Userid)
      {
          Console.Write("Выберете id Задачи, которую хотите удалить:");
          int idt = int.Parse(Console.ReadLine());
        
          var querySql1 = $"DELETE FROM task WHERE idtask = {idt} AND userid = {Userid};";
          using var cmd1 = new NpgsqlCommand(querySql1, DatabaseService.GetSqlConnection());
          cmd1.ExecuteNonQuery();
      }
      
      
      public static void TodayTask(int Userid)
      {
          formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            
          var querySql = $"SELECT * FROM task where date_of_task = '{formattedDate}' and userid = {Userid}";
          using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              Console.WriteLine(
                  $"Название: {reader[1]} \nЗадача: {reader[2]} \nДата выполнения: {reader[3]}");
          }
      }
      public static void TommorowTask(int Userid)
      {
          formattedDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
        
          var querySql = $"SELECT * FROM task where date_of_task = '{formattedDate}' and userid = {Userid}";
          using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              Console.WriteLine(
                  $"Название: {reader[1]} \nЗадача: {reader[2]} \nДата выполнения: {reader[3]}");
          }
      }

      public static void WeekTask(int Userid)
      {
          int weekConvert = (int)DateTime.Today.DayOfWeek;
          if (weekConvert == 0)
          {
              weekConvert = 7;
          }

          formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
          string formattedDateWeekConvert = DateTime.Today.AddDays(7 - weekConvert).ToString("yyyy-MM-dd HH:mm:ss");
        
          var querySql =
              $"SELECT * FROM sidequests where (datequest >= '{formattedDate}' AND datequest <= '{formattedDateWeekConvert}') and id_user ={Userid}";
          using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
          using var reader = cmd.ExecuteReader();
          while (reader.Read())
          {
              Console.WriteLine(
                  $"Название: {reader[1]} \nЗадача: {reader[2]} \nДата выполнения: {reader[3]}");
          }
      }

    
}
