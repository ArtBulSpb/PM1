using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM1
{
    public class Employee
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public decimal BonusPercentage { get; set; }

        public int YearsToRetirement()
        {
            int retirementAge = Gender == "Мужской" ? 65 : 60;
            int age = DateTime.Now.Year - BirthDate.Year;
            if (DateTime.Now < BirthDate.AddYears(age)) age--;

            return retirementAge - age;
        }

        public override string ToString()
        {
            return $"{Name}, {BirthDate.ToShortDateString()}, {Gender}, {Education}, {Position}, " +
                   $"Оклад: {Salary}, Премия: {BonusPercentage}%";
        }
    }

}
