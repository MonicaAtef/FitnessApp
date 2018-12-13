﻿using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using FitnessApp.Models;
using FitnessApp.SQLdatabase;

namespace FitnessApp.UserMainWindowPages
{
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        // Create an object from dataBase class
        SQLqueries SQLqueriesObject = new SQLqueries();

        public SettingsPage()
        {
            InitializeComponent();
            UserMainWindow.SettingsPageObject = this;

            // Initialize Profile Expander to be expanded
            ProfileExpander.IsExpanded = true;

            // Initialize DataContext with signedInUser Model
            DataContext = UserMainWindow.signedInUser;
        }


        private void Expander_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Remove Expanded Event Handler from all Expanders
            // to prevent calling Expander_Expanded Event Handler 
            // recursively.
            ProfileExpander .Expanded -= Expander_Expanded;
            AccountExpander .Expanded -= Expander_Expanded;
            SecurityExpander.Expanded -= Expander_Expanded;
            AboutExpander   .Expanded -= Expander_Expanded;
            FeedbackExpander.Expanded -= Expander_Expanded;
            HelpExpander    .Expanded -= Expander_Expanded;

            // Close all Expanders.
            ProfileExpander .IsExpanded = false;
            AccountExpander .IsExpanded = false;
            SecurityExpander.IsExpanded = false;
            AboutExpander   .IsExpanded = false;
            FeedbackExpander.IsExpanded = false;
            HelpExpander    .IsExpanded = false;

            // Re-add Expanded Event Handler to all Expanders.
            ProfileExpander .Expanded += Expander_Expanded;
            AccountExpander .Expanded += Expander_Expanded;
            SecurityExpander.Expanded += Expander_Expanded;
            AboutExpander   .Expanded += Expander_Expanded;
            FeedbackExpander.Expanded += Expander_Expanded;
            HelpExpander    .Expanded += Expander_Expanded;


            // Get Current Expander object from sender.
            Expander currentExpander = sender as Expander;

            // Remove Expanded Event Handler from Current Expander.
            currentExpander.Expanded -= Expander_Expanded;

            // Open current Expander only.
            currentExpander.IsExpanded = true;

            // Re-add Expanded Event Handler to Current Expander.
            currentExpander.Expanded += Expander_Expanded;

        }

        private ImageModel currentProfilePhoto;

        private void UpdateUserProfilePhotoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog browsePhotoDialog = new OpenFileDialog();
            browsePhotoDialog.Title  = "Select your New Profile Photo";
            browsePhotoDialog.Filter = "All formats|*.jpg;*.jpeg;*.png|" +
                                       "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                       "PNG (*.png)|*.png";

            if (browsePhotoDialog.ShowDialog() == true)
            {
                currentProfilePhoto = new ImageModel { FilePath = browsePhotoDialog.FileName };
                UserProfilePhoto.ImageSource = currentProfilePhoto.Source;
            }

        }
        
        private void TextBoxesNumbersOnly_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void UpdateProfileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (WeightTextBox.Text == ""             || HeightTextBox.Text == ""          || TargetWeightTextBox.Text == ""     ||
                KilosToLosePerWeekTextBox.Text == "" || WorkoutsPerWeekTextBox.Text == "" || WorkoutHoursPerDayTextBox.Text == "")
            {
                if (WeightTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Weight Is Empty!");
                if (HeightTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Height Is Empty!");
                if (TargetWeightTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Target Weight Is Empty!");
                if (KilosToLosePerWeekTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Kilos To Lose Per Week Is Empty!");
                if (WorkoutsPerWeekTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workouts Per Week Is Empty!");
                if (WorkoutHoursPerDayTextBox.Text == "")
                    UserMainWindow.UserMainWindowObject.MessagesSnackbar.MessageQueue.Enqueue("Workout Hours Per Day Is Empty!");
            }
            else
            {

                // Update signedInUser User Model

                if (currentProfilePhoto != null) // Check if profile photo is updated
                    UserMainWindow.signedInUser.ProfilePhoto.ByteArray = currentProfilePhoto.ByteArray;

                UserMainWindow.signedInUser.Weight             = double.Parse(WeightTextBox            .Text);
                UserMainWindow.signedInUser.Height             = double.Parse(HeightTextBox            .Text);
                UserMainWindow.signedInUser.TargetWeight       = double.Parse(TargetWeightTextBox      .Text);
                UserMainWindow.signedInUser.KilosToLosePerWeek = double.Parse(KilosToLosePerWeekTextBox.Text);
                UserMainWindow.signedInUser.WorkoutsPerWeek    = double.Parse(WorkoutsPerWeekTextBox   .Text);
                UserMainWindow.signedInUser.WorkoutHoursPerDay = double.Parse(WorkoutHoursPerDayTextBox.Text);

                // Update User's Profile in database
                SQLqueriesObject.UpdateUserProfile(UserMainWindow.signedInUser);

                // Refresh UserMainWindow DataContext
                UserMainWindow.UserMainWindowObject.DataContext = null;
                UserMainWindow.UserMainWindowObject.DataContext = UserMainWindow.signedInUser;

                // Refresh CaloriesCalculatorPage DataContext
                UserMainWindow.CaloriesCalculatorPageObject.DataContext = null;
                UserMainWindow.CaloriesCalculatorPageObject.DataContext = UserMainWindow.signedInUser;
            }
        }

        private void UpdateAccountButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Update Account Code Here...
        }

        private void UpdatePasswordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Update Password Code Here...
        }

        private void SubmitFeedbackButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Submit Feedback Code Here...
        }

    }
}
