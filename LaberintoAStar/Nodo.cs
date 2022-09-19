using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaberintoAStar
{
    public class Nodo
    {
        public int Ren { get; set; }
        public int Col { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F => H + G;
        public Nodo Anterior { get; set; }
        public void calcularH(Nodo final)
        {
            this.H= Math.Abs(this.Ren - final.Ren)
                    + Math.Abs(this.Col - final.Col);
        }

        public IEnumerable<Nodo> generarSucesores()
        {
            if (this.Col > 0)
            {
                yield return new Nodo { Ren = this.Ren, Col = this.Col - 1, G = this.G + 1 };
            }
            if (this.Col < 19)
            {
                yield return new Nodo { Ren = this.Ren, Col = this.Col + 1, G = this.G + 1 };
            }
            if (this.Ren>0)
            {
                yield return new Nodo { Ren=this.Ren-1,Col=this.Col, G=this.G + 1 };
            }
            if (this.Ren<19)
            {
                yield return new Nodo { Ren=this.Ren+1, Col=this.Col, G=this.G + 1 };    
            }
        }


        public bool esIgual(Nodo n) => this.Ren == n.Ren && this.Col == n.Col;
     
    }
}
