using GeneticAlgorithmSimulator.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace GeneticAlgorithmSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Dictionary<TestFunction, Func<double, double, double>> testFunctionsImpl = new()
        {
            { 
                TestFunction.SCHWEFEL, 
                (double x1, double x2) 
                    => 837.9658 - ( x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2))) )
            }
        };

        private Settings settings = new()
        {
            TestFunction = TestFunction.SCHWEFEL,
            RangeStart = 0,
            RangeEnd = 10,
            NumOfBits = 40,
            PopulationSize = 100,
            EpochsAmount = 1000,
            PercentageToCross = 10,
            TournamentsAmount = 20,
            PercentageInElite = 10,
            CrossingProbabPerc = 60,
            MutationProbabPerc = 40,
            InversionProbabPerc = 10,
            SelectionMethod = SelectionMethod.BEST,
            CrossingMethod = CrossingMethod.ONE_POINT,
            MutationMethod = MutationMethod.ONE_POINT,
            OptimizationMethod = OptimizationMethod.MIN
        };

        public MainWindow()
        {
            InitializeComponent();
            DataContext = settings;
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxSelectionMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionMethod? val = (SelectionMethod)e.AddedItems[0];
            if (val == null)
                return;

            TextBoxPercentageToCross.IsEnabled = false;
            TextBoxTournamentsAmount.IsEnabled = false;

            switch (val)
            {
                case SelectionMethod.BEST:
                    TextBoxPercentageToCross.IsEnabled = true;
                    break;
                case SelectionMethod.TOURNAMENT:
                    TextBoxTournamentsAmount.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        private void OnlyNumberInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regexes.number.IsMatch(e.Text);
        }

        private void OnlyIntegerInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regexes.integer.IsMatch(e.Text);
        }

        private void OnlyPositiveIntegerInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regexes.positiveInteger.IsMatch(e.Text);
        }
    }
}
