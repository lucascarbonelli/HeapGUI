using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

namespace Heap
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Elemento> elementosLista = new ObservableCollection<Elemento>();

        public MainWindow()
        {
            InitializeComponent();

            //elementosLista.Add(new Elemento() { Id = 1, Valor = 25, Accion = "Insertar", EnHeap = "Si" });
            //elementosLista.Add(new Elemento() { Id = 2, Valor = 1234, Accion = "Insertar", EnHeap = "Si" });
            elementos.ItemsSource = elementosLista;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComandoInsertar_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Insertar_CanExecute();
        }

        private void ComandoInsertar_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Insertar_Executed();
        }

        private void ComandoEliminar_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Eliminar_CanExecute();
        }

        private void ComandoEliminar_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Eliminar_Executed();
        }

        private void ComandoDesencolar_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Desencolar_CanExecute();
        }

        private void ComandoDesencolar_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Desencolar_Execute();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (Eliminar_CanExecute())
                {
                    Eliminar_Executed();
                }
                else if (Insertar_CanExecute())
                {
                    Insertar_Executed();
                }
            }
        }

        private bool Insertar_CanExecute()
        {
            int i = 0;
            if (int.TryParse(textBox.Text.ToString(), out i))
            {
                foreach (Elemento elem in elementosLista)
                {
                    if (elem.Valor == i && elem.EnHeap == EstaEnHeap.Si)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private void Insertar_Executed()
        {
            elementosLista.Add(new Elemento(elementosLista.Count() + 1, int.Parse(textBox.Text), Acciones.Insertar));
        }

        private bool Eliminar_CanExecute()
        {
            int i = 0;
            if (int.TryParse(textBox.Text.ToString(), out i))
            {
                foreach (Elemento elem in elementosLista)
                {
                    if (elem.Valor == i && elem.EnHeap == EstaEnHeap.Si)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void Eliminar_Executed()
        {
            int i = 0;
            int.TryParse(textBox.Text.ToString(), out i);

            foreach (Elemento elem in elementosLista)
            {
                if (elem.Valor == i && elem.EnHeap == EstaEnHeap.Si)
                {
                    elem.EnHeap = EstaEnHeap.No;
                }
            }

            elementosLista.Add(new Elemento(elementosLista.Count() + 1, i, Acciones.Eliminar));
        }

        private bool Desencolar_CanExecute()
        {
            foreach (Elemento elem in elementosLista)
            {
                if (elem.EnHeap == EstaEnHeap.Si)
                {
                    return true;
                }
            }
            return false;
        }
    
        private void Desencolar_Execute()
        {
            foreach (Elemento elem in elementosLista)
            {
                if (elem.EnHeap == EstaEnHeap.Si)
                {
                    elem.EnHeap = EstaEnHeap.No;
                    elementosLista.Add(new Elemento(elementosLista.Count() + 1, elem.Valor, Acciones.Desencolar));
                    break;
                }
            }
        }
    }


    public enum Acciones { Insertar, Eliminar, Desencolar }

    public enum EstaEnHeap { Si, No}


    public class Elemento : INotifyPropertyChanged
    {
        private EstaEnHeap enHeap;
        public int Id { get; set; }
        public int Valor { get; set; }
        public Acciones Accion { get; set; }
        public EstaEnHeap EnHeap {
            get { return this.enHeap; }
            set
            {
                if (this.enHeap != value)
                {
                    this.enHeap = value;
                    this.NotifyPropertyChanged("EnHeap");
                }
            }
        }

        public Elemento(int id, int val, Acciones accion)
        {
            Id = id;
            Valor = val;
            Accion = accion;

            if(accion == Acciones.Insertar)
            {
                EnHeap = EstaEnHeap.Si;
            }
            else
            {
                EnHeap = EstaEnHeap.No;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }

    public static class CustomCommands
    {
        public static readonly RoutedCommand Insertar = new RoutedUICommand
            (
            "Insertar",
            "Insertar",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                //new KeyGesture(Key.Enter)
            }
            );

        public static readonly RoutedCommand Eliminar = new RoutedUICommand
            (
            "Eliminar",
            "Eliminar",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                //new KeyGesture(Key.Enter)
            }
            );

                public static readonly RoutedCommand Desencolar = new RoutedUICommand
            (
            "Desencolar",
            "Desencolar",
            typeof(CustomCommands),
            new InputGestureCollection()
            {
                //new KeyGesture(Key.Enter)
            }
            );
    }

    public class isStringNumber : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            int i = 0;
            return int.TryParse(value.ToString(), out i);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
}
