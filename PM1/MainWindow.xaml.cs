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

namespace PM1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Employee> employees = new List<Employee>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = NameTextBox.Text;
                DateTime birthDate = BirthDatePicker.SelectedDate ?? throw new Exception("Выберите дату рождения.");
                string gender = (GenderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string education = (EducationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string position = (PositionComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                decimal salary = decimal.Parse(SalaryTextBox.Text);
                decimal bonus = decimal.Parse(((BonusComboBox.SelectedItem as ComboBoxItem)?.Content.ToString().TrimEnd('%')) ?? "0");

                Employee employee = new Employee
                {
                    Name = name,
                    BirthDate = birthDate,
                    Gender = gender,
                    Education = education,
                    Position = position,
                    Salary = salary,
                    BonusPercentage = bonus
                };

                employees.Add(employee);
                MessageBox.Show("Сотрудник добавлен успешно!");
                ClearInputs();
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные значения для оклада и премии.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowEmployees_Click(object sender, RoutedEventArgs e)
        {
            EmployeeListBox.Items.Clear();
            foreach (var employee in employees)
            {
                EmployeeListBox.Items.Add(employee.ToString() + $" (Осталось до пенсии: {employee.YearsToRetirement()} лет)");
            }
        }

        private void RemoveEmployee_Click(object sender, RoutedEventArgs args)
        {
            if (EmployeeListBox.SelectedItem != null)
            {
                string selectedEmployee = EmployeeListBox.SelectedItem.ToString();
                var employeeToRemove = employees.FirstOrDefault(emp => emp.ToString() == selectedEmployee);
                if (employeeToRemove != null)
                {
                    employees.Remove(employeeToRemove);
                    MessageBox.Show("Сотрудник удален.");
                    ShowEmployees_Click(sender, args);
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.");
            }
        }

        private void SearchEmployee_Click(object sender, RoutedEventArgs args)
        {
            string nameToSearch = NameTextBox.Text.ToLower();
            var foundEmployees = employees.Where(emp => emp.Name.ToLower().Contains(nameToSearch)).ToList();

            EmployeeListBox.Items.Clear();
            foreach (var employee in foundEmployees)
            {
                EmployeeListBox.Items.Add(employee.ToString() + $" (Осталось до пенсии: {employee.YearsToRetirement()} лет)");
            }

            if (foundEmployees.Count == 0)
            {
                MessageBox.Show("Сотрудники не найдены.");
            }
        }


        private void FilterByEducation_Click(object sender, RoutedEventArgs args)
        {
            string selectedEducation = (EducationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var filteredEmployees = employees.Where(e => e.Education == selectedEducation).ToList();

            EmployeeListBox.Items.Clear();
            foreach (var employee in filteredEmployees)
            {
                EmployeeListBox.Items.Add(employee.ToString() + $" (Осталось до пенсии: {employee.YearsToRetirement()} лет)");
            }

            if (filteredEmployees.Count == 0)
            {
                MessageBox.Show("Сотрудники с таким уровнем образования не найдены.");
            }
        }

        private void ClearInputs()
        {
            NameTextBox.Clear();
            BirthDatePicker.SelectedDate = null;
            GenderComboBox.SelectedIndex = -1;
            EducationComboBox.SelectedIndex = -1;
            PositionComboBox.SelectedIndex = -1;
            SalaryTextBox.Clear();
            BonusComboBox.SelectedIndex = -1;
        }
    }
}
