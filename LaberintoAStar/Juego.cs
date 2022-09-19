using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaberintoAStar
{
    public class Juego
    {
        public Nodo Final { get; set; }
        public Nodo Inicial { get; set; }
        public bool[,] Tablero { get; set; }

        public Juego(Nodo final, Nodo inicial, bool[,] tablero)
        {
            Final = final;
            Inicial = inicial;
            Tablero = tablero;
        }
        List<Nodo> abiertos;
        List<Nodo> cerrados;
        Nodo mejorNodo;
        public IEnumerable<Nodo> ObtenerRuta()
        {
            abiertos = new List<Nodo>();
            cerrados = new List<Nodo>();
            Inicial.calcularH(Final);
            abiertos.Add(Inicial);
            bool fallo = false;
            bool solucionEncontrada = false;
            do
            {

                if (abiertos.Count == 0)
                {
                    fallo = true;
                }
                else
                {
                    mejorNodo = abiertos.OrderBy(n => n.F).First();
                    abiertos.Remove(mejorNodo);
                    cerrados.Add(mejorNodo);
                    solucionEncontrada = mejorNodo.esIgual(Final);
                    if (!solucionEncontrada)
                    {
                        var sucesores = mejorNodo.generarSucesores();
                        foreach (var sucesor in sucesores)
                        {
                            TratarSucesor(sucesor);
                        }
                    }
                }
            } while (!fallo && !solucionEncontrada);
            if (fallo)
            {
                return null;
            }
            Nodo actual = mejorNodo;
            List<Nodo> ruta = new List<Nodo>();
            do
            {
                ruta.Add(actual);
                actual = actual.Anterior;
            } while (actual!=null);
            //ruta.Add(actual);
            ruta.Reverse();
            return ruta;
        }
        public void TratarSucesor(Nodo sucesor)
        {
            if (!Tablero[sucesor.Col, sucesor.Ren])
            {
                Nodo viejo;
                if (cerrados.Any(n => n.esIgual(sucesor)))
                {

                    viejo = cerrados.First(n => n.esIgual(sucesor));
                    if (viejo.G > sucesor.G)
                    {
                        viejo.Anterior = mejorNodo;
                        propagarG(viejo, sucesor.G);
                    }
                } else if (abiertos.Any(n => n.esIgual(sucesor)))
                {
                    viejo = abiertos.First(n => n.esIgual(sucesor));

                    if (sucesor.G<viejo.G)
                    {
                        viejo.Anterior = mejorNodo;
                        viejo.G = sucesor.G;
                    }
                }
                else
                {
                    abiertos.Add(sucesor);
                    sucesor.Anterior = mejorNodo;
                    sucesor.calcularH(Final);
                }
            }
        }
        void propagarG(Nodo nodo, int G)
        {
            nodo.G = G;
            IEnumerable<Nodo> hijos;
            hijos = abiertos.Where(n => n.Anterior == nodo);
            foreach (var hijo in hijos)
            {
                hijo.G = nodo.G + 1;
            }
            hijos = cerrados.Where(n => n.Anterior == nodo);
            foreach (var hijo in hijos)
            {
                propagarG(hijo, nodo.G + 1);
            }
        }
    }
}
