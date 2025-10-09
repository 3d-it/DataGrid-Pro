using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows;

namespace DataGrid_Pro
{
    public partial class MainWindow : Window
    {
        private string connString;

        public MainWindow()
        {
            InitializeComponent();

            // Full path to your database file
            string dbPath = @"C:\Users\denni\source\repos\DataGrid Pro\Database\WideWorldImporters.sqlite";
            DbInitializer.EnsureDatabase(dbPath);

            connString = $"Data Source={dbPath};Version=3;";
        }

        private void RunReport(string query)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    ResultsGrids.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Report1_Click(object sender, RoutedEventArgs e)
        {
            RunReport("SELECT CustomerID, CustomerName, CustEmail FROM Customers;");
        }

        private void Report2_Click(object sender, RoutedEventArgs e)
        {
            RunReport(@"SELECT o.OrderID, c.CustomerName, o.OrderDate, o.TotalAmount
                FROM Orders o
                JOIN Customers c ON o.CustomerID = c.CustomerID
                ORDER BY o.OrderDate DESC;");
        }

        private void Report3_Click(object sender, RoutedEventArgs e)
        {
            RunReport("SELECT PurchaseOrderID, SupplierName, OrderDate, TotalAmount FROM PurchaseOrders ORDER BY OrderDate DESC;");
        }

        private void Report4_Click(object sender, RoutedEventArgs e)
        {
            RunReport("SELECT CityName AS 'City', LatestRecordedPopulation AS 'Population' FROM Cities ORDER BY CityName;");
        }

        private void Report5_Click(object sender, RoutedEventArgs e)
        {
            RunReport(@"SELECT i.InvoiceID, c.CustomerName, i.InvoiceDate, i.TotalDue
                FROM Invoices i
                JOIN Customers c ON i.CustomerID = c.CustomerID
                ORDER BY i.InvoiceDate DESC;");
        }

        private void Report6_Click(object sender, RoutedEventArgs e)
        {
            RunReport("SELECT ColorID, ColorName FROM Colors ORDER BY ColorName;");
        }

        private void Report7_Click(object sender, RoutedEventArgs e)
        {
            RunReport("SELECT StockItemID, StockItemName AS 'Item Name', UnitPrice AS 'Price' FROM StockItems ORDER BY StockItemName;");
        }

        private void Report8_Click(object sender, RoutedEventArgs e)
        {
            RunReport(@"SELECT pol.PurchaseOrderLineID, po.SupplierName, si.StockItemName, 
                       pol.ExpectedUnitPricePerOuter, pol.Quantity
                FROM PurchaseOrderLines pol
                JOIN PurchaseOrders po ON pol.PurchaseOrderID = po.PurchaseOrderID
                JOIN StockItems si ON pol.StockItemID = si.StockItemID
                ORDER BY pol.ExpectedUnitPricePerOuter DESC;");
        }

        private void Report9_Click(object sender, RoutedEventArgs e)
        {
            RunReport("SELECT CityName, LatestRecordedPopulation FROM Cities ORDER BY LatestRecordedPopulation DESC;");
        }

        private void Report10_Click(object sender, RoutedEventArgs e)
        {
            RunReport("SELECT CountryName AS 'Country', FormalName AS 'Official Name' FROM Countries ORDER BY CountryName;");
        }

    }
}
