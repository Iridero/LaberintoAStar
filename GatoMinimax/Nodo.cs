using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatoMinimax
{
    public class Nodo
    {
        //Estado
        public String Estado { get; set; } = "O X   X O";

        public List<Nodo> Hijos { get; set; } = new List<Nodo>();
        public int Valor { get; set; }

        /// <summary>
        /// Obtiene o establece el caracter con el que juega la computadora
        /// (X u O)
        /// </summary>
        public static char XuO { get; set; }

        private static readonly int[,] lineas =
            new int[,] {
                         { 0,1,2},
                         {3,4,5},
                         {6,7,8},
                         {0,3,6 },
                         {1,4,7},
                         {2,5,8},
                         {0,4,8 },
                         {2,4,6 }
            };
        public bool GanaXO(char XO)
        {
            for (int i = 0; i < 8; i++)
            {
                if (Estado[Nodo.lineas[i, 0]] == XO &&
                    Estado[Nodo.lineas[i, 1]] == XO &&
                        Estado[Nodo.lineas[i, 2]] == XO)
                {
                    return true;
                }
            }
            return false;
        }
        public bool EsTerminal
        {
            get { return this.GanaXO('X')||this.GanaXO('O'); }
        }
        public void GenerarHijos(char turno, int profundidad)
        {

            for (int i = 0; i < 9; i++)
            {
                if (Estado[i]==' ')
                {
                    var arrEstado = this.Estado.ToCharArray();
                    arrEstado[i] = turno;
                    Nodo hijo = new Nodo()
                    {
                        Estado = new string(arrEstado)
                    };
                    if (profundidad>1&& !hijo.GanaXO(turno))
                    {
                        hijo.GenerarHijos((turno == 'X') ? 'O' : 'X', profundidad - 1);
                    }
                    Hijos.Add(hijo);
                }
            }
        }


        public int Minimax(int profundidad, bool maximizandoParaJugador)
        {
            if (profundidad==0 || EsTerminal)
            {
                CalcularValor(Nodo.XuO);
                return this.Valor;
            }
            else
            {
                if (maximizandoParaJugador)
                {
                    int valor = int.MinValue;
                    foreach (var hijo in this.Hijos)
                    {
                        valor = Math.Max(valor, hijo.Minimax(profundidad - 1, !maximizandoParaJugador));
                    }
                    Valor = valor;
                    return valor;
                }
                else
                {
                    int valor = int.MaxValue;
                    foreach (var hijo in this.Hijos)
                    {
                        valor=Math.Min(valor, hijo.Minimax(profundidad-1,!maximizandoParaJugador));
                    }
                    Valor= valor;
                    return valor;
                }
            }
        }
        public void CalcularValor(char XO)
        {
            if (EsTerminal)
            {
                Valor = (GanaXO(XO)) ? int.MaxValue : int.MinValue;
            }
            else
            {
                int abiertasX = 0;
                int abiertasO = 0;
                for (int i = 0; i < 8; i++)
                {
                    abiertasX += (Estado[Nodo.lineas[i, 0]] == 'O' ||
                        Estado[Nodo.lineas[i, 1]] == 'O' ||
                            Estado[Nodo.lineas[i, 2]] == 'O') ? 0 : 1;
                    abiertasO += (Estado[Nodo.lineas[i, 0]] == 'X' ||
                        Estado[Nodo.lineas[i, 1]] == 'X' ||
                            Estado[Nodo.lineas[i, 2]] == 'X') ? 0 : 1;
                }
                Valor = (XO == 'X') ? abiertasX - abiertasO : abiertasO - abiertasX;
            }
        }
        public override string ToString()
        {
            return Estado;
        }
    }
}
