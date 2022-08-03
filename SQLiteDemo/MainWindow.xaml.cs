using DemoLibrary;
using System.Collections.Generic;
using System.Windows;

namespace SQLiteDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<PersonModel> people = new List<PersonModel>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadPeopleList()
        {
            people = SqliteDataAccess.LoadPeople();

            WireUpPeopleList();
        }

        private void WireUpPeopleList()
        {
            listPeopleListBox.ItemsSource = null;
            listPeopleListBox.Items.Clear();
            listPeopleListBox.ItemsSource = people;
        }

        private void addPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = new PersonModel();

            p.Firstname = firstNameText.Text;
            p.Lastname = lastNameText.Text;

            SqliteDataAccess.SavePerson(p);
            WireUpPeopleList();

            //reset textbox content
            firstNameText.Text = "";
            lastNameText.Text = "";
        }

        private void refreshListBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadPeopleList();
        }
    }
}
