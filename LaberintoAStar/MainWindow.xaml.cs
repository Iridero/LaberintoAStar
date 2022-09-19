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

namespace LaberintoAStar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private Rectangle[,] cuadros
            = new Rectangle[20, 20];
        bool[,] tablero;

        Nodo inicial = new Nodo { Col = 2, Ren = 2 };
        Nodo final = new Nodo { Col = 15, Ren = 18 };
        IEnumerable<Nodo> ruta;
        private void ActualizarTablero()
        {
            for (int r = 0; r < 20; r++)
            {
                for (int c = 0; c < 20; c++)
                {
                    if (tablero[c, r])
                    {
                        cuadros[c, r].Fill =
                            Brushes.SaddleBrown;
                        cuadros[c, r].Stroke = Brushes.Transparent;
                    }
                }
            }
            cuadros[inicial.Col, inicial.Ren].Stroke = Brushes.Red;
            cuadros[final.Col, final.Ren].Stroke = Brushes.Blue;
            if (ruta != null)
            {
                foreach (var nodo in ruta)
                {
                    cuadros[nodo.Col, nodo.Ren].Fill =
                        Brushes.DarkSeaGreen;
                }

            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int r = 0; r < 20; r++)
            {
                for (int c = 0; c < 20; c++)
                {
                    cuadros[c, r] = new Rectangle() { Fill = Brushes.LightSeaGreen };
                    gridTablero.Children.Add(cuadros[c, r]);
                    cuadros[c, r].MouseLeftButtonUp += cuadro_MouseLeftButtonUp;
                }
            }
            tablero = new bool[,]
            {
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,true,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,true,false,false,false,false,false,false,false,false },
                { false, false, true,false,false,false,false,true,true,true,true,true,true,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false },
                { false, false, false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false,false }
            };
            ActualizarTablero();

        }

        private void cuadro_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle cuadro = (Rectangle)sender;
            int index = gridTablero.Children.IndexOf(cuadro);
            int r = index / 20;
            int c = index % 20;
            tablero[c,r]= !tablero[c,r];
            ActualizarTablero();
        }

        private void btnResolver_Click(object sender, RoutedEventArgs e)
        {
            Juego j = new Juego(
                            inicial,
                            final,
                            tablero
                            );
            ruta = j.ObtenerRuta();
            ActualizarTablero(); 
        }
    }
}
