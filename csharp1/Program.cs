// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using System.Linq.Expressions;


class Program
{
  
   static void Main(string[] args)
  {
    
  

    Console.WriteLine("Hello, World! This is the Journal application select an option to proceed.");
    int choice = 0;
    Journal journal = new Journal();
    while (choice != 5)
    {
    Console.WriteLine("1. Add an Entry");
    Console.WriteLine("2. Display Entry");
    Console.WriteLine("3. Remove Entry");
    Console.WriteLine("4. Total Entry Count");
    Console.Write("Select An Option: ");
      if(int.TryParse(Console.ReadLine(),out choice)){}
      else
          {
          Console.WriteLine("Enter a valid number");
          }
      if(choice ==  1)
      {
          DateTime date = DateTime.Now;
        string presentDate = date.ToShortDateString();
      Console.WriteLine("What's on your mind....");
        Console.Write("> ");
        string text = Console.ReadLine();
          while(string.IsNullOrWhiteSpace(text))
          {
            Console.WriteLine("Share your thoughts");
            Console.Write("> ");
            text = Console.ReadLine();
          }
        Entry entry = new Entry(presentDate,text);
        journal.AddEntry(entry);
      }
      else if (choice == 2)
    {
      try
      {
        journal.DisplayEntry();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      } 
    }
    else if (choice == 3)
      {
      
        journal.DisplayEntry();
      Console.WriteLine("Select An Entry To Delete");
      if(int.TryParse(Console.ReadLine(), out int num)){}
      else
        {
          Console.WriteLine("Pleease Enter a valid number");
        }
      try
      {
          journal.RemoveEntry(num);
          Console.WriteLine("....");
          Console.WriteLine("Removed");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}");
      }   
    }
    else if (choice == 4)
      {
        try{
        Console.WriteLine($"{journal.CountEntry()} entries in your journal");
        }
          catch(Exception ex)
          {
            Console.WriteLine($"Error: {ex.Message}");
          }
      }
        
    }

    // Employee employee1 = new Employee("Aliya",18,1000);
    // Console.WriteLine(employee1.EmployeeDetails());
    // try
    // {
      //Employee employee = new Employee("",-1,0);
      //Console.WriteLine(employee.EmployeeDetails());
    //}
    //catch (Exception ex)
    //{
      //Console.WriteLine($"Error: {ex.Message}");
    
    //}
  }
}