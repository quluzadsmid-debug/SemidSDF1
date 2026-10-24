using System.Collections.Generic;
using System.Linq;

namespace SemidSDF
{
    // 1. Tələb olunan Abstract Sinif (Şablon)
    public abstract class EmployeeServiceBase
    {
        public abstract bool AddEmployee(Employee employee);
        public abstract Employee FindEmployee(string code);
        public abstract bool AddPayment(string employeeCode, string title, double amount);
        public abstract List<Payment> GetEmployeePayments(string employeeCode);
    }

    // 2. Abstract sinifdən törəyən əsas işlək sinif
    public class EmployeeService : EmployeeServiceBase
    {
        // Bütün işçilərin saxlandığı RAM yaddaşı (static List)
        public static List<Employee> Employees = new List<Employee>();

        // İşçi əlavə etmək (Eyni kod varsa false qaytarır)
        public override bool AddEmployee(Employee employee)
        {
            if (Employees.Any(e => e.Code == employee.Code)) return false;
            Employees.Add(employee);
            return true;
        }

        // Koda görə işçi axtarmaq
        public override Employee FindEmployee(string code)
        {
            return Employees.FirstOrDefault(e => e.Code == code);
        }

        // İşçiyə ödəniş əlavə etmək
        public override bool AddPayment(string employeeCode, string title, double amount)
        {
            if (amount <= 0) return false;

            Employee emp = FindEmployee(employeeCode);
            if (emp == null) return false;

            emp.Payments.Add(new Payment { Title = title, Amount = amount });
            return true;
        }

        // İşçinin ödənişlərini əldə etmək
        public override List<Payment> GetEmployeePayments(string employeeCode)
        {
            Employee emp = FindEmployee(employeeCode);
            if (emp == null) return null;
            return emp.Payments;
        }
    }
}