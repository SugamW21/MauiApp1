using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MauiApp1.Models;

namespace MauiApp1.Components.Pages
{
    public partial class Todo : ComponentBase
    {
        private string newTask = string.Empty;
        private List<TodoItem> todoList = new();
        private string message = string.Empty;

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(newTask))
            {
                todoList.Add(new TodoItem { Id = Guid.NewGuid(), TaskName = newTask, IsCompleted = false });
                newTask = string.Empty;
                message = "Task added successfully!";
            }
        }




    }
}