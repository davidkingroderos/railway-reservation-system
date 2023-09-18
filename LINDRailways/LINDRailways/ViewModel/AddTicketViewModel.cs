﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LINDRailways.Model;
using LINDRailways.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINDRailways.ViewModel
{
    [QueryProperty("TrainSchedule", "TrainSchedule")]
    public partial class AddTicketViewModel : BaseViewModel
    {
        public AddTicketViewModel()
        {
        }

        [ObservableProperty]
        TrainSchedule trainSchedule;

        [RelayCommand]
        private async Task BookTicketAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Book Ticket", "Are you sure you want to book?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketService.AddTicket("David", 
                    DateOnly.FromDateTime(DateTime.Now), true, true, 
                    TrainSchedule);

                await Shell.Current.CurrentPage.DisplayAlert("Success!", 
                    "Ticket Booked", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!", 
                    $"Unable to book ticket: {ex.Message}", "OK");
            }
        }
        [RelayCommand]
        private async Task ReserveTicketAsync()
        {
            bool isConfirmed = await Shell.Current.CurrentPage.DisplayAlert(
                "Reserve Ticket", "Are you sure you want to reserve?", "Yes", "No");

            if (!isConfirmed)
                return;

            try
            {
                await TicketService.AddTicket("David", 
                    DateOnly.FromDateTime(DateTime.Now), true, false, TrainSchedule);

                await Shell.Current.CurrentPage.DisplayAlert("Success!", 
                    "Ticket Reserved", "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.CurrentPage.DisplayAlert("Error!", 
                    $"Unable to reserve ticket: {ex.Message}", "OK");
            }
        }
    }
}
