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
    public enum RegexType
    {
        NUMBER, INTEGER, POSITIVE_INTEGER
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Dictionary<RegexType, Regex> regexes = new()
        {
            { RegexType.NUMBER, new Regex("[^0-9.-]+") },
            { RegexType.INTEGER, new Regex("[^0-9-]+") },
            { RegexType.POSITIVE_INTEGER, new Regex("[^0-9]+") }
        };

        private Settings settings = new()
        {
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
            e.Handled = regexes[RegexType.NUMBER].IsMatch(e.Text);
        }

        private void OnlyIntegerInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regexes[RegexType.INTEGER].IsMatch(e.Text);
        }

        private void OnlyPositiveIntegerInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regexes[RegexType.POSITIVE_INTEGER].IsMatch(e.Text);
        }
    }
}
