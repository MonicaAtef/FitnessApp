﻿using FitnessApp.SignUpPages;
using FitnessApp.SQLdatabase;
using MaterialDesignThemes.Wpf;
using System.Windows;

namespace FitnessApp
{
    /// <summary>
    /// Interaction logic for SigningWindow.xaml
    /// </summary>
    public partial class SigningWindow : Window
    {
        public static SigningWindow    SigningWindowObject;
        public static SignUpFirstPage  SignUpFirstPageObject;
        public static SignUpSecondPage SignUpSecondPageObject;
        public static SetUpProfilePage SetUpProfilePageObject;
        

        // Create object from database class
        SQLqueries SQLqueriesObject = new SQLqueries();

        public SigningWindow()
        {
            InitializeComponent();
            SigningWindowObject = this;

            // Initialize UserMainWindowPages Objects
            SignUpFirstPageObject  = new SignUpFirstPage();
            SignUpSecondPageObject = new SignUpSecondPage();
            SetUpProfilePageObject = new SetUpProfilePage();

            // Intialize ErrorMessagesQueue and Assign it to ErrorsSnackbar's MessageQueue
            var ErrorMessagesQueue = new SnackbarMessageQueue(System.TimeSpan.FromMilliseconds(2000));
            ErrorsSnackbar.MessageQueue = ErrorMessagesQueue;
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {

            bool isAccountFound = SQLqueriesObject.SignIn(EmailSignInTextBox.Text, PasswordSignInTextBox.Password);

            if (isAccountFound == true)
            {
                if (SQLqueriesObject.accountType == "User")
                {
                    // Open User Main Window
                    UserMainWindow UserMainWindowTemp = new UserMainWindow(SQLqueriesObject.accountID);
                    UserMainWindowTemp.Show();
                }
                else
                {
                    // Open Admin Main Window
                    AdminMainWindow AdminMainWindowTemp = new AdminMainWindow();
                    AdminMainWindowTemp.Show();
                }

                // Close Signing Window
                Close();
            }
            else
            {
                // Display error when the user is not found
                ErrorsSnackbar.MessageQueue.Enqueue("Incorrect Email Or Password");
            }

        }


        private void CreateAnAccountButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpPagesFrame.NavigationService.Navigate(SigningWindow.SignUpFirstPageObject);
        }
    }
}
