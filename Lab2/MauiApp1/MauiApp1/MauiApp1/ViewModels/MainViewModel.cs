﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text.Json;
using System.Numerics;

namespace MauiApp1.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _currentDateTime;
        private string _currentDeviceInfo;

        public string Title
        {
            get => "Welcome to.NET MAUI";
        }


        public string CurrentDateTime
        {
            get => _currentDateTime;
            set
            {
                _currentDateTime = value;
                OnPropertyChanged();
            }
        }
        public ICommand UpdateTimeCommand { get; }
        public ICommand UpdateImageCommand { get; }

        public string CurrentDeviceinfo
        {
            get => new StringBuilder()
            .AppendLine($"Model: {DeviceInfo.Model}")
            .AppendLine($"Manufacturer: {DeviceInfo.Manufacturer}")
            .AppendLine($"Platform: {DeviceInfo.Platform}")
            .AppendLine($"OS Version: {DeviceInfo.VersionString}").ToString();
            set
            {
                _currentDeviceInfo = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            UpdateTimeCommand = new Command(UpdateTime);
            CurrentDateTime = DateTime.Now.ToString("F");
        }
        private void UpdateTime()
        {
            CurrentDateTime = DateTime.Now.ToString("F");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task FetchDataFromApiAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var toDoItem = JsonSerializer.Deserialize<ToDoItem>(json);
            }
        }
    }

    public class ToDoItem
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
