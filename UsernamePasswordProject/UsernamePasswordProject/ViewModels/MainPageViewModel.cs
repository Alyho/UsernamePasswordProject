using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using UsernamePasswordProject.Models;

namespace UsernamePasswordProject.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public Command SaveCommand { get; }

        private ObservableCollection<Account> _userListView;

        public event PropertyChangedEventHandler PropertyChanged;

        private string Username_;
        private string Password_;

        private bool _userCreated;

        public bool UserCreated
        {
            get { return _userCreated; }
        }

        public MainPageViewModel()
        {

            _userListView = new ObservableCollection<Account>();

            SaveCommand = new Command(async () =>
            {

                var _user = new Account
                {
                    Username = Username_,
                    Password = Password_

                };

                //call the save to database

                await App.Database.SaveAccountAsync;
                _userListView = await App.Database.GetAccountAsync(_user.Username);

                //if the save returns an Account, then user already exists
                if (_userListView != null)
                {
                    _userCreated = false;
                };

                //if the save returns null, then the user doesn't exist

                if (_userListView == null)
                {
                    _userCreated = true;
                }

                var ar = new PropertyChangedEventArgs(nameof(UserCreated));
                PropertyChanged?.Invoke(this, ar);

                if (_userCreated)
                {
                    _userListView.Add(_user);
                    Username = string.Empty;
                    Password = string.Empty;
                }
                
                
            });

        }

        public ObservableCollection<Account> userListView
        {
            get { return _userListView; }
            set
            {
                if (_userListView != value)
                {
                    _userListView = value;
                    var args = new PropertyChangedEventArgs(nameof(userListView));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Username
        {
            get => Username_;
            set
            {
                if (Username_ != value)
                {
                    Username_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Username));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }

        public string Password
        {
            get => Password_;
            set
            {
                if (Password_ != value)
                {
                    Password_ = value;
                    var args = new PropertyChangedEventArgs(nameof(Password));
                    PropertyChanged?.Invoke(this, args);
                }
            }
        }


    }
}
