using AvitoXml.Wpf.Data;
using AvitoXml.Wpf.Models;
using AvitoXml.Wpf.Windows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AvitoXml.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext context = new ApplicationContext();
        public AdBlank AdBlank
        {
            get
            {
                var b = new AdBlank();
                b.Profile = (Profile)ProfileBox.SelectedItem;
                b.AdType = ((TextBlock)AdTypeBox.SelectedItem).Text;
                b.Type = (Models.Type)TypesBox.SelectedItem;
                b.Category = (Category)CategoriesBox.SelectedItem;
                b.Price = Price.Text;
                b.Condition = ((TextBlock)Condition.SelectedItem).Text;
                b.Description = Description.Text;
                b.Title = Title.Text;
                if (StartDateCheckBox.IsChecked.Value)
                    b.StartDate = StartDate.SelectedDate.Value;
                if (EndDateCheckBox.IsChecked.Value)
                    b.EndDate = EndDate.SelectedDate.Value;

                return b;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            CategoriesBox.DisplayMemberPath = "Name";
            TypesBox.DisplayMemberPath = "Name";
            ProfileBox.DisplayMemberPath = "Name";
            AdTypeBox.SelectedIndex = 0;
            
            
            Condition.SelectedIndex = 0;
            RefreshCategories();
            RefreshProfiles();
            StartDateCheckBox_Checked(null, null);
            EndDateCheckBox_Checked(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Windows.ProfilesWindow().ShowDialog();
        }
        public void RefreshCategories()
        {
            context = new ApplicationContext();
            CategoriesBox.ItemsSource = context.Categories.ToList();
            CategoriesBox.SelectedIndex = 0;
        }
        public void RefreshProfiles()
        {
            ProfileBox.ItemsSource = context.Profiles.ToArray();
            ProfileBox.SelectedIndex = 0;
        }
        private void CategoriesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var category = (Category)CategoriesBox.SelectedItem;
            if (category == null)
                return;
            context = new ApplicationContext();
            var types = context.Categories.Include(x => x.Types).FirstOrDefault(x => x.Id == category.Id).Types;
            TypesBox.ItemsSource = types;
            TypesBox.SelectedIndex = 0;
        }
        private void StartDateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (StartDateCheckBox.IsChecked.Value)
                StartDateLabel.Visibility = StartDate.Visibility = Visibility.Visible;
            else
                StartDateLabel.Visibility = StartDate.Visibility = Visibility.Hidden;
        }
        private void EndDateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (EndDateCheckBox.IsChecked.Value)
                EndDateLabel.Visibility = EndDate.Visibility = Visibility.Visible;
            else
                EndDateLabel.Visibility = EndDate.Visibility = Visibility.Hidden;
        }

        private void XlsImportBtn_Click(object sender, RoutedEventArgs e)
        {
            new XlsImportWindow(AdBlank).ShowDialog();
        }

        private void ProfilesBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProfilesWindow().ShowDialog();
            RefreshProfiles();
        }

    }
}
