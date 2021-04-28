using GeneticAlgorithmSimulator.Models;
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

        private string _computationTimeMsg;
        public string ComputationTimeMsg
        {
            get => _computationTimeMsg;
            private set
            {
                _computationTimeMsg = value;
                OnPropertyChanged();
            }
        }

        private string _resultMsg;
        public string ResultMsg
        {
            get => _resultMsg;
            private set
            {
                _resultMsg = value;
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
            if (!IsValid(StackPanelSettings))
                return;
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
                    foreach (var item in PlotArguments.Axes)
                    {
                        item.Minimum = item.AbsoluteMinimum = manager.TestFunction.InputDomain.Item1;
                        item.Maximum = item.AbsoluteMaximum = manager.TestFunction.InputDomain.Item2;
                    }

                    var scatterIndividuals = new List<ScatterPoint>();
                    foreach (var item in results)
                    {
                        if (scatterIndividuals.FindIndex(x => x.X == item.x1 && x.Y == item.x2) == -1)
                        {
                            scatterIndividuals.Add(new ScatterPoint(item.x1, item.x2, size: 3));
                        }
                    }

                    var best = results.Last();
                    PlotResultsLine.ItemsSource = results.Select(x => new DataPoint(x.epochNumber, x.functionValue));
                    PlotArgumentsScatterIndividuals.ItemsSource = scatterIndividuals;
                    PlotArgumentsScatterMin.ItemsSource = new[] { new ScatterPoint(manager.TestFunction.MinValueArguments.Item1, manager.TestFunction.MinValueArguments.Item2, size: 5) };
                    PlotMeanLine.ItemsSource = results.Select(x => new DataPoint(x.epochNumber, x.mean));
                    PlotStdDevLine.ItemsSource = results.Select(x => new DataPoint(x.epochNumber, x.stdDev));
                    TestFunctionActualMinVal = manager.TestFunction.MinValue;
                    ComputationTimeMsg = string.Format("Last computation time: {0}ms", manager.LastComputationTimeInMs);
                    ResultMsg = string.Format("Best result: f({0:0.0000}, {1:0.0000}) = {2:0.0000}", best.x1, best.x2, best.functionValue);
                });
            });
        }

        private void DisableConditionalSelecionInputs(SelectionMethodEnum? currentValue)
        {
            currentValue = currentValue != null ? currentValue : (SelectionMethodEnum)ComboBoxSelectionMethod.SelectedItem;
            if (currentValue == null)
                return;

            TextBoxPercentageToSelect.IsEnabled = false;
            TextBoxNumOfIndivInGroup.IsEnabled = false;

            switch (currentValue)
            {
                case SelectionMethodEnum.BEST:
                    TextBoxPercentageToSelect.IsEnabled = true;
                    break;
                case SelectionMethodEnum.TOURNAMENT:
                    TextBoxNumOfIndivInGroup.IsEnabled = true;
                    break;
                default:
                    break;
            }
        }

        private bool IsValid(DependencyObject obj)
        {
            return !System.Windows.Controls.Validation.GetHasError(obj) &&
                LogicalTreeHelper.GetChildren(obj).OfType<DependencyObject>().All(IsValid);
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
