using AvitoXml.Wpf.Data;
using AvitoXml.Wpf.Enums;
using AvitoXml.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AvitoXml.Wpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProfileWindow.xaml
    /// </summary>
    public partial class ProfileWindow : Window
    {
        public bool IsReadOnly => EditMode == EditMode.View;
        public Profile Profile { get; set; } = new Profile();
        EditMode EditMode;
        ApplicationContext context = new ApplicationContext();
        public ProfileWindow()
        {
            InitializeComponent();
            EditMode = EditMode.Create;
            DataContext = Profile;

        }
        public ProfileWindow(int id,EditMode mode)
        {
            InitializeComponent();
            Profile = context.Profiles.SingleOrDefault(x => x.Id == id);
            EditMode = mode;
            DataContext = Profile;
            if (mode == EditMode.View)
                Name.IsReadOnly = 
                    Manager.IsReadOnly = 
                    Phone.IsReadOnly = 
                    Adress.IsReadOnly =
                    Prefix.IsReadOnly = true;
        }
        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (EditMode)
            {
                case EditMode.Create:
                    context.Profiles.Add(Profile);
                    context.SaveChanges();
                    break;
                case EditMode.Edit:
                    context.Profiles.Update(Profile);
                    context.SaveChanges();
                    break;
            }
            DialogResult = false;
        }
    }
}
