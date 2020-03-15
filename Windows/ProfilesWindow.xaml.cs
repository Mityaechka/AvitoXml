using AvitoXml.Wpf.Commands;
using AvitoXml.Wpf.Data;
using AvitoXml.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Windows;
using System.Windows.Input;

namespace AvitoXml.Wpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfilesWindow.xaml
    /// </summary>
    public partial class ProfilesWindow : Window
    {
        public ObservableCollection<Profile> UserProfiles { get; set; } = new ObservableCollection<Profile>();

        ApplicationContext context = new ApplicationContext();
        public ProfilesWindow()
        {
            InitializeComponent();
            RefreshProfiles();
        }
        void RefreshProfiles()
        {
            context = new ApplicationContext();
            UserProfiles = new ObservableCollection<Profile>(context.Profiles.ToList());
            Profiles.ItemsSource = UserProfiles;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new ProfileWindow();
            window.ShowDialog();
            RefreshProfiles();
        }

        private void ViewClick(object sender, RoutedEventArgs e)
        {
            var profile = (Profile)Profiles.SelectedItem;
            var window = new ProfileWindow(profile.Id, Enums.EditMode.View);
            window.ShowDialog();
            RefreshProfiles();
        }
        private void EditClick(object sender, RoutedEventArgs e)
        {
            var profile = (Profile)Profiles.SelectedItem;
            var window = new ProfileWindow(profile.Id, Enums.EditMode.Edit);
            window.ShowDialog();
            RefreshProfiles();
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            var profile = (Profile)Profiles.SelectedItem;
            if(MessageBox.Show("Вы хотите удалить этот профиль?", "Удалить?", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                context.Profiles.Remove(context.Profiles.SingleOrDefault(x=>x.Id==profile.Id));
                context.SaveChanges();
                RefreshProfiles();
            }
        }
    }
}
