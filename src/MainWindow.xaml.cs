using GeneticAlgorithmSimulator.TestFunctions;
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
        public bool IsPlotLivePreview { get; set; } = true;

        private static readonly Dictionary<TestFunctionEnum, ITestFunction> testFunctions = new()
        {
            { TestFunctionEnum.SCHWEFEL, new SchwefelTestFunction() }
        };

        private GeneticAlgorithmSettings settings = new()
        {
            TestFunction = TestFunctionEnum.SCHWEFEL,
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
            SelectionMethod = SelectionMethodEnum.BEST,
            CrossingMethod = CrossingMethodEnum.ONE_POINT,
            MutationMethod = MutationMethodEnum.ONE_POINT
        };

        private bool isWindowLoaded = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            isWindowLoaded = true;
            StackPanelSettings.DataContext = settings;
            DisableConditionalSelecionInputs(null);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBoxSelectionMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isWindowLoaded)
                return;
            DisableConditionalSelecionInputs((SelectionMethodEnum)e.AddedItems[0]);
        }

        private void DisableConditionalSelecionInputs(SelectionMethodEnum? currentValue)
        {
            currentValue = currentValue != null ? currentValue : (SelectionMethodEnum)ComboBoxSelectionMethod.SelectedItem;
            if (currentValue == null)
                return;

            TextBoxPercentageToCross.IsEnabled = false;
            TextBoxTournamentsAmount.IsEnabled = false;

            switch (currentValue)
            {
                case SelectionMethodEnum.BEST:
                    TextBoxPercentageToCross.IsEnabled = true;
                    break;
                case SelectionMethodEnum.TOURNAMENT:
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
