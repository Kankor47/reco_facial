using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ControlRF {

        public static string generarNombreArchivo()
        {
            string nombre = "";
            string letras = "abcdefghijklmnopqrstuvwxyz";
            Random rnd = new Random();
            for (int i = 0; i < 50; i++){
                int lon = rnd.Next(1, 3); // 1-> letra; 2-> número
                if (lon == 1) {
                    int indLetra = rnd.Next(0, 24);
                    int mon = rnd.Next(2);// 0-> Mayúscula; 1-> minúscula
                    string c = (letras[indLetra])+"";
                    if (mon == 0){
                        c = c.ToLower();
                    }
                    nombre += c;
                }else {
                    int numero = rnd.Next(10); // número
                    nombre += numero;
                }
            }
            return nombre;
        }
    }
}
