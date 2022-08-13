using DemoLibrary;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SQLiteDemo
{
    public partial class MainWindow : Window
    {
        List<PersonModel> people = new List<PersonModel>();
        PersonModel selectedPerson = new PersonModel();

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
        private void refreshUI()
        {
            LoadPeopleList();
            ResetUserInputUI();
        }
        private void ResetUserInputUI()
        {
            idText.Text = "";
            firstNameText.Text = "";
            lastNameText.Text = "";
            editBtn.IsEnabled = false;
        }
        private void SelectedPerson()
        {
            selectedPerson = people[listPeopleListBox.SelectedIndex];

            idText.Text = selectedPerson.Id.ToString();
            firstNameText.Text = selectedPerson.Firstname;
            lastNameText.Text = selectedPerson.Lastname;
        }
        private void addPersonBtn_Click(object sender, RoutedEventArgs e)
        {
            PersonModel p = new PersonModel();

            p.Firstname = firstNameText.Text;
            p.Lastname = lastNameText.Text;

            SqliteDataAccess.SavePerson(p);
            WireUpPeopleList();
            ResetUserInputUI();
            refreshUI();
        }
        private void refreshListBtn_Click(object sender, RoutedEventArgs e)
        {
            refreshUI();
        }
        private void ListBoxItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SelectedPerson();
            editBtn.IsEnabled = true;
            deleteBtn.IsEnabled = true;

        }
        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            selectedPerson.Firstname = firstNameText.Text;
            selectedPerson.Lastname = lastNameText.Text;
            SqliteDataAccess.EditPerson(selectedPerson);
            refreshUI();
        }
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            List<PersonModel> selectedPersons = new List<PersonModel>();

            if(listPeopleListBox.SelectedItems.Count > 0)
            {
                foreach (PersonModel selPerson in listPeopleListBox.SelectedItems)
                {
                    selectedPersons.Add(selPerson);
                }
            }
            else
            {
                var dialog = new SmallDialog("Nothing is selected!","Oopsie");
                dialog.ShowDialog();
                //MessageBox.Show("Nothing selected!","Oopsie", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            SqliteDataAccess.DeleteMultiplePerson(selectedPersons);
            refreshUI();
        }
        private void findBtn_Click(object sender, RoutedEventArgs e)
        {
            string firstname;
            string lastname;

            firstname = firstNameText.Text;
            lastname = lastNameText.Text;

            people = SqliteDataAccess.FindPeople(firstname, lastname);

            WireUpPeopleList();

        }
    }
}
