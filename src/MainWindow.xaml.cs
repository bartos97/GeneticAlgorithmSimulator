using GeneticAlgorithmSimulator.TestFunctions;
using GeneticAlgorithmSimulator.Validation;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double TestFunctionActualMinVal { get; private set; }

        private double _lastComputationTime;
        public double LastComputationTime
        {
            get => _lastComputationTime;
            private set
            {
                _lastComputationTime = value;
                OnPropertyChanged();
            }
        }

        private bool isWindowLoaded = false;
        private GeneticAlgorithmSettings settings = GeneticAlgorithmSettings.GetDefault();

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

        private void ComboBoxSelectionMethod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isWindowLoaded)
                return;
            DisableConditionalSelecionInputs((SelectionMethodEnum)e.AddedItems[0]);
        }


        private async void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            DisableUIForWork(true);
            await RunSimulation();
            DisableUIForWork(false);
        }

        private void DisableUIForWork(bool isDisabled)
        {
            StackPanelSettings.IsEnabled = !isDisabled;
            GridLoader.Visibility = isDisabled ? Visibility.Visible : Visibility.Hidden;
        }

        private async Task RunSimulation()
        {
            await Task.Run(() =>
            {
                var manager = new GeneticAlgorithmManager(settings);
                var results = manager.GetResults();

                Dispatcher.Invoke(() =>
                {
                    PlotResultsLine.ItemsSource = results.Select(x => new DataPoint(x.epochNumber, x.functionValue));
                    PlotArgumentsScatterIndividuals.ItemsSource = results.Select(x => new ScatterPoint(x.x1, x.x2, 2));
                    PlotArgumentsScatterMin.ItemsSource = new[] { new ScatterPoint(manager.TestFuncMinValueArg.Item1, manager.TestFuncMinValueArg.Item2, 5) };
                    PlotMeanLine.ItemsSource = results.Select(x => new DataPoint(x.epochNumber, x.mean));
                    PlotStdDevLine.ItemsSource = results.Select(x => new DataPoint(x.epochNumber, x.stdDev));
                    LastComputationTime = manager.GetLastComputationTime();
                    TestFunctionActualMinVal = manager.TestFuncMinValue;
                });
            });
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

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
